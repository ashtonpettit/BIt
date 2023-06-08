using EnergyAPI.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EnergyAPI.Controllers
{
    public class Constantresults
    {
        public DateTime datetime { get; set; }
        public double totaldayconsumption { get; set; }
        public double totalnighconsumption { get; set; }
        public Constantresults(DateTime datetime, double totaldayconsumption,double totalnightconsumption)
        {
            this.datetime=datetime;
            this.totaldayconsumption=totaldayconsumption;
            this.totalnighconsumption=totalnightconsumption;

        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class ConstantController : ControllerBase
    {
        private IDataContext _dataContext;
        public ConstantController(IDataContext data)
        {
            _dataContext = data;
        }

        // GET: api/<SolarController>
        [HttpGet]
        public ActionResult<List<Constantresults>> Get()
        {
            List<Constantresults> list = new List<Constantresults>();
            MySqlConnection connection = Program.conn();
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "Select * from constant_reading";
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
                list.Add(new Constantresults(Convert.ToDateTime(fetch_query["datetime"]), Math.Round(Convert.ToDouble(fetch_query["total_day_consumption"]), 3), Math.Round(Convert.ToDouble(fetch_query["total_night_consumption"]), 3)));
            }
            return Ok(list);

        }
        [HttpGet("latest")]
        public ActionResult<List<Constantresults>> GetLatest()
        {
            List<Constantresults> list = new List<Constantresults>();
            MySqlConnection connection = Program.conn();
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "SELECT * FROM constant_reading order by datetime desc limit 1";
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
                list.Add(new Constantresults(Convert.ToDateTime(fetch_query["datetime"]), Math.Round(Convert.ToDouble(fetch_query["total_day_consumption"]), 3), Math.Round(Convert.ToDouble(fetch_query["total_night_consumption"]), 3)));
            }
            return Ok(list);

        }
    }
}
