using HomeAssignment5.BL;

namespace HomeAssignment5.Repositories
{
    public class MockLegoRepository : ILegoRepository
    {
        // הרשימה בזיכרון
         List<LegoSet> sets = new List<LegoSet>();

        // מונה ל-ID
         int nextId = 1;

        public List<LegoSet> GetAll()
        {
            return sets;
        }

        public LegoSet Add(LegoSet set)
        {
            set.Id = nextId;
            nextId++;

            sets.Add(set);
            return set;
        }

        public bool Delete(int id)
        {
            LegoSet set = sets.FirstOrDefault(s => s.Id == id);
            if (set == null)
                return false;

            sets.Remove(set);
            return true;
        }
    }
}
