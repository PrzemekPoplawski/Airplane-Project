using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Entry_Project
{
    public class SQLiteDataAccess
    {
        public static List<Airplane> LoadAirplanes()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Airplane>("Select * from Airplane", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveAirplanes(Airplane airplane)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO Airplane(AirplaneNumber,AirplaneSpec,AirplaneModel,AirplaneImage) VALUES(@AirplaneNumber,@AirplaneSpec, @AirplaneModel, @AirplaneImage)", airplane);
            };
        }       

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
