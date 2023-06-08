using EnergyAPI.Models;
using LiteDB;
using System.Drawing;
using System.Xml.Linq;

namespace EnergyAPI.Data
{
    public class Database :IDataContext
    {
        LiteDatabase _db = new LiteDatabase("data.db");

        public void AddElectricReading(Electric electric)
        {
            _db.GetCollection<Electric>("Electric").Insert(electric);
        }

        public void AddSolarReading(Solar solar)
        {
            _db.GetCollection<Solar>("Solar").Insert(solar);
        }

        public void DeleteSolarReading(DateTime date)
        {
            _db.GetCollection<Solar>("Solar").Delete(date);
        }



        public IEnumerable<Electric> GetAllElectricReadings()
        {
            return _db.GetCollection<Electric>("Electric").FindAll();
        }

        public IEnumerable<Solar> GetAllSolarReadings()
        {
            return _db.GetCollection<Solar>("Solar").FindAll();
        }

        public IEnumerable<Constant> GetLatestReading()
        {
            throw new NotImplementedException();
        }

        public Solar GetOneSolarReading(DateTime date)
        {
            return _db.GetCollection<Solar>("Solar").FindById(date);
        }
    }
}
