using H_Sports.Interfaces;
using H_Sports.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;



namespace H_Sports.Repositories
{
    public class SportsRepository : BaseRepository, ISportsRepository

    {
        public SportsRepository(IConfiguration configuration) : base(configuration) { }
        public List<Sport> GetSports()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, SportName FROM [Sport] ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var sports = new List<Sport>();
                        while (reader.Read())
                        {
                            Sport sport = new Sport
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                SportName = reader.GetString(reader.GetOrdinal("SportName"))
                            };
                            sports.Add(sport);
                        }
                        return sports;
                    }
                }
            }
        }

        Sport ISportsRepository.GetSportBySportName(string sportName)
        {
            throw new NotImplementedException();
        }
    }
}