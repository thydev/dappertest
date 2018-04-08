using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using dappertest.Models;
 
namespace dappertest.Factory
{
    public class NinjaFactory : IFactory<Ninja>
    {
        private string connectionString;
        public NinjaFactory()
        {
            connectionString = "server=localhost;userid=root;password=admin1;port=3306;database=dojoleague;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(Ninja item)
        {
            using (IDbConnection dbConnection = Connection) {
                
                string query;
                if(item.Dojo != null) {
                    query =  $"INSERT INTO Ninjas(name, level, description, dojo_id) VALUES(@Name, @Level, @Description, {@item.Dojo.Id})";
                } else {
                    query =  "INSERT INTO Ninjas(name, level, description, dojo_id) VALUES(@Name, @Level, @Description, 0)";
                }
                System.Console.WriteLine(query);
                dbConnection.Open();
                int outInt;
                outInt = dbConnection.Execute(query, item);
                System.Console.WriteLine();
                System.Console.WriteLine(outInt);
                System.Console.WriteLine();
            }
        }

        public void UpdateDojo(int ninja_id, int dojo_id)
        {
            using (IDbConnection dbConnection = Connection) {
                
                string query = $"Update Ninjas Set dojo_id={dojo_id} where id={ninja_id}" ;
                
                dbConnection.Open();
                dbConnection.Execute(query);

            }
        }
        public IEnumerable<Ninja> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM ninjas");
            }
        }

        public IEnumerable<Ninja> RogueNinja()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM ninjas Where dojo_id=0");
            }
        }

        public Ninja FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM Ninjas WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<Ninja> NinjasForDojoById(int dojo_id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var query = $"SELECT * FROM ninjas JOIN dojos ON ninjas.dojo_id = dojos.id WHERE dojos.id = {dojo_id}";
                dbConnection.Open();
        
                var myNinjas = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => { ninja.Dojo = dojo; return ninja; });
                return myNinjas;
            }
        }

    }
}