using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Hotel.Models;

namespace Hotel._Repositories
{
    public class HotelRepository : BaseRepository, IHotelRepository
    {
        //Constructor
        public HotelRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //Methods
        public void Add(HotelModel hotelModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Hotel values (@name)";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = hotelModel.Name;
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "delete from Hotel where Hotel_Id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }
        public void Edit(HotelModel hotelModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update Hotel 
                                        set Hotel_Name=@name 
                                        where Hotel_Id=@id";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = hotelModel.Name;
                command.Parameters.Add("@id", SqlDbType.Int).Value = hotelModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<HotelModel> GetAll()
        {
            var hotelList = new List<HotelModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select *from Hotel order by Hotel_Id desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hotelModel = new HotelModel();
                        hotelModel.Id = (int)reader[0];
                        hotelModel.Name = reader[1].ToString();
                        hotelList.Add(hotelModel);
                    }
                }
            }
            return hotelList;
        }

        public IEnumerable<HotelModel> GetByValue(string value)
        {
            var hotelList = new List<HotelModel>();
            int hotelId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string hotelName = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Select *from Hotel
                                        where Hotel_Id=@id or Hotel_Name like @name+'%' 
                                        order by Hotel_Id desc";
                command.Parameters.Add("@id", SqlDbType.Int).Value = hotelId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = hotelName;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hotelModel = new HotelModel();
                        hotelModel.Id = (int)reader[0];
                        hotelModel.Name = reader[1].ToString();
                        hotelList.Add(hotelModel);
                    }
                }
            }
            return hotelList;
        }
    }
}