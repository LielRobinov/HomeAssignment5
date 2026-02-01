
using HomeAssignment5.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace HomeAssignment5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //כל פעם שמישהו יבקש ILegoRepository 
            //תן לו רק מופע אחד של MockLegoRepository תמיד
            builder.Services.AddSingleton<ILegoRepository, MockLegoRepository>();

            // הוספת Authentication ו-Authorization
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // לא חייב לבדוק מי הוציא את הטוקן
                    ValidateAudience = false, // לא חייב לבדוק מי הקהל
                    ValidateLifetime = true, // לבדוק אם הטוקן פג תוקף
                    ValidateIssuerSigningKey = true, // לבדוק אם המפתח חתום כראוי
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("THIS_IS_A_SUPER_SECRET_KEY_12345")) // חייב להיות אותו מפתח כמו ב-AuthController
                };
            });

            // גם מוסיפים Authorization
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
