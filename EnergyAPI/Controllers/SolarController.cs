using EnergyAPI.Data;
using EnergyAPI.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace EnergyAPI.Controllers
{
    public class Solarresults
    {
        public DateOnly date { get; set;}
        public double output { get; set;}
        public Solarresults(DateOnly date,double output)
        {
            this.date = date;
            this.output = output;
        }
    }
  
    [Route("api/[controller]")]
    [ApiController]
    public class SolarController : ControllerBase
    {
        
        private IDataContext _dataContext;
        public SolarController(IDataContext data) 
        {
            _dataContext= data;
        }

        // GET: api/<SolarController>
        [HttpGet]
        public ActionResult<List<Solarresults>> Get()
        {
            List<Solarresults> list = new List<Solarresults>();
            MySqlConnection connection = Program.conn();
            MySqlCommand query = connection.CreateCommand();
            query.CommandText= "Select * from daily_solar";
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
                list.Add(new Solarresults(DateOnly.FromDateTime(Convert.ToDateTime(fetch_query["date"])), Math.Round(Convert.ToDouble(fetch_query["output"]),3)));
            }
            return Ok(list);
            
        }
        /*
        [HttpGet("{date}")]
        public ActionResult<Solar> Get(DateTime date)
        {
            Solar solar = _dataContext.GetOneSolarReading(date);
            if (solar == null) return NotFound("No data for date");
            return Ok(solar);
        }
        [HttpPost]
        public ActionResult<Solar> Post(Solar solar)
        {
            _dataContext.AddSolarReading(solar);
            return Ok("Added!");
        }

        [HttpDelete]
        public ActionResult DeleteSolarReading(DateTime date)
        {
            _dataContext.DeleteSolarReading(date);
            return Ok();
        }
        */
    }
}
