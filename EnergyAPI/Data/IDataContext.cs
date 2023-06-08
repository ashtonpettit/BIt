using EnergyAPI.Models;

namespace EnergyAPI.Data
{
    public interface IDataContext
    {
        //Solar
        public void AddSolarReading(Solar solar);
        IEnumerable<Solar> GetAllSolarReadings();

        Solar GetOneSolarReading(DateTime date);
        void DeleteSolarReading(DateTime date);
        //Electric
        public void AddElectricReading(Electric electric);
        IEnumerable<Electric> GetAllElectricReadings();
        IEnumerable<Constant> GetLatestReading();
       
    }
}
