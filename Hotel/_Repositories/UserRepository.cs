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
    public class UserRepository : BaseRepository, IUserRepository
    {
        //Constructor
        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //Methods
        public void Add(UserModel userModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into User values (@name, @password, @role)";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@role", SqlDbType.NVarChar).Value = userModel.Role;
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
                command.CommandText = "delete from User where User_Id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }
        public void Edit(UserModel userModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update User
                                        set User_Name=@name,User_Password= @password,Pet_Role= @role
                                        where Pet_Id=@id";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@role", SqlDbType.NVarChar).Value = userModel.Role;
                command.Parameters.Add("@id", SqlDbType.Int).Value = userModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<UserModel> GetAll()
        {
            var userList = new List<UserModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select *from User order by User_Id desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var userModel = new UserModel();
                        userModel.Id = (int)reader[0];
                        userModel.Name = reader[1].ToString();
                        userModel.Password = reader[2].ToString();
                        userModel.Role = reader[3].ToString();
                        userList.Add(userModel);
                    }
                }
            }
            return userList;
        }

        public IEnumerable<UserModel> GetByValue(string value)
        {
            var userList = new List<UserModel>();
            int userId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string userName = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Select *from User
                                        where Pet_Id=@id or Pet_Name like @name+'%' 
                                        order by Pet_Id desc";
                command.Parameters.Add("@id", SqlDbType.Int).Value = userId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = userName;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var userModel = new UserModel();
                        userModel.Id = (int)reader[0];
                        userModel.Name = reader[1].ToString();
                        userModel.Password = reader[2].ToString();
                        userModel.Role = reader[3].ToString();
                        userList.Add(userModel);
                    }
                }
            }
            return userList;
        }
    }
}