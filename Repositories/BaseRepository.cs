using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration; 


namespace H_Sports.Repositories
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            //constructor that accepts an IConfiguration instance to access configuration settings 
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        protected SqlConnection Connection
        {
            get
            {
                //A protected property to provide a SqlConnection instance to derived classes 
                return new SqlConnection(_connectionString);
            }
        }
    }
}
