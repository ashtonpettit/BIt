using EnergyAPI.Data;
using EnergyAPI.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EnergyAPI.Controllers
{
    public class Electricresults
    {
        public DateOnly date { get; set; }
        public string register { get; set; }
        public Double value { get; set; }
        public Electricresults(DateOnly date, string register, double value)
        {
            this.date = date;
            this.register = register;
            this.value = value;
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricController : ControllerBase
    {
        private IDataContext _dataContext;
        public ElectricController(IDataContext data)
        {
            _dataContext = data;
        }
        // GET: api/<SolarController>
        [HttpGet]
        public ActionResult<List<Electricresults>> Get()
        {
            List<Electricresults> list = new List<Electricresults>();
            MySqlConnection connection = Program.conn();
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "Select * from daily_useage";
            try
            {
                connection.Open();

            }
            catch (Exception ex)
            {
                return NotFound("No connection");
            }
            MySqlDataReader fetch_query = query.ExecuteReader();
            while (fetch_query.Read())
            {
                list.Add(new Electricresults(DateOnly.FromDateTime(Convert.ToDateTime(fetch_query["date"])),fetch_query["register"].ToString(), Math.Round(Convert.ToDouble(fetch_query["value"]), 3)));
            }
            return Ok(list);

        }
        /*
        [HttpPost]
        public ActionResult<Electric> Post(Electric electric)
        {
            _dataContext.AddElectricReading(electric);
            return Ok("Added!");
        }
        */
    }
}
