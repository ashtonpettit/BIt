using EnergyAPI.Data;
using MySql.Data.MySqlClient;

namespace EnergyAPI
{
    public class Program
    {
        public static MySqlConnection conn()
        {
            string server = "ID328605_energy.db.webhosting.be";
            string database = "ID328605_energy";
            string username = "ID328605_energy";
            string password = "energy123";
            string constring = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={password};";
            MySqlConnection connection = new MySqlConnection(constring);
            return connection;
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDateOnlyTimeOnlyStringConverters();
            builder.Services.AddSingleton(typeof(IDataContext), typeof(Database));
            builder.Services.AddSwaggerGen(c=>c.UseDateOnlyTimeOnlyStringConverters());
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}