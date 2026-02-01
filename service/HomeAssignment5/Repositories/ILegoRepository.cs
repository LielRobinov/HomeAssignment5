using HomeAssignment5.BL;

namespace HomeAssignment5.Repositories
{
    public interface ILegoRepository
    {
        List<LegoSet> GetAll();
        LegoSet Add(LegoSet set);
        bool Delete(int id);
    }
}
