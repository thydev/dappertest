using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using dappertest.Models;
 
namespace dappertest.Factory
{
    public class DojoFactory : IFactory<Dojo>
    {
        private string connectionString;
        public DojoFactory()
        {
            connectionString = "server=localhost;userid=root;password=admin1;port=3306;database=dojoleague;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(Dojo item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO Dojos(name, location, description) VALUES(@Name, @Location, @Description)";
                System.Console.WriteLine(query);
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<Dojo> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                
                return dbConnection.Query<Dojo>("SELECT * FROM Dojos");
            }
        }
        public Dojo FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var query =
                @"
                SELECT * FROM dojos WHERE id = @Id;
                SELECT * FROM ninjas WHERE dojo_id = @Id;
                ";
        
                using (var multi = dbConnection.QueryMultiple(query, new {Id = id}))
                {
                    var dojo = multi.Read<Dojo>().Single();
                    dojo.Ninjas = multi.Read<Ninja>().ToList();
                    return dojo;
                }
                // return dbConnection.Query<Dojo>("SELECT * FROM Dojos WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }
    }
}