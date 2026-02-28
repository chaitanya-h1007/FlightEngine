using FlightSearchEngine.Models;
using Microsoft.Data.SqlClient;
using System.Data;
namespace FlightSearchEngine.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("FlightSearchEnigne");
        }

       
        public async Task<List<string>> GetSourcesAsync()
        {
            List<string> sources = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetSources", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        sources.Add(reader["Source"].ToString());
                    }
                }
            }

            return sources;
        }

     
        public async Task<List<string>> GetDestinationsAsync()
        {
            List<string> destinations = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetDestinations", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        destinations.Add(reader["Destination"].ToString());
                    }
                }
            }

            return destinations;
        }

       
        public async Task<List<FlightResult>> SearchFlightsAsync(string source, string destination, int persons)
        {
            List<FlightResult> flights = new List<FlightResult>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_SearchFlights", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Source", source);
                cmd.Parameters.AddWithValue("@Destination", destination);
                cmd.Parameters.AddWithValue("@Persons", persons);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        flights.Add(new FlightResult
                        {
                            FlightId = Convert.ToInt32(reader["FlightId"]),
                            FlightName = reader["FlightName"].ToString(),
                            FlightType = reader["FlightType"].ToString(),
                            Source = reader["Source"].ToString(),
                            Destination = reader["Destination"].ToString(),
                            TotalCost = Convert.ToDecimal(reader["TotalCost"])
                        });
                    }
                }
            }

            return flights;
        }

        public async Task<List<FlightHotelResult>> SearchFlightsWithHotelsAsync(string source, string destination, int persons)
        {
            List<FlightHotelResult> packages = new List<FlightHotelResult>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_SearchFlightsWithHotels", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Source", source);
                cmd.Parameters.AddWithValue("@Destination", destination);
                cmd.Parameters.AddWithValue("@Persons", persons);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        packages.Add(new FlightHotelResult
                        {
                            FlightId = Convert.ToInt32(reader["FlightId"]),
                            FlightName = reader["FlightName"].ToString(),
                            Source = reader["Source"].ToString(),
                            Destination = reader["Destination"].ToString(),
                            HotelName = reader["HotelName"].ToString(),
                            TotalCost = Convert.ToInt32(reader["TotalCost"])
                        });
                    }
                }
            }

            return packages;
        }


    }
}
