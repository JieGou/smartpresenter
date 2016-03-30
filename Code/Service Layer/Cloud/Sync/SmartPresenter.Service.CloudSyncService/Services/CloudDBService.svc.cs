using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartPresenter.Service.CloudSyncService
{
    public class CloudDBService : ICloudDBService
    {
        private readonly string _connectionString;

        public CloudDBService()
        {
            if (ConfigurationManager.ConnectionStrings["CloudDB"] != null)
            {
                _connectionString = ConfigurationManager.ConnectionStrings["CloudDB"].ToString();
            }
        }
        public List<UserAccount> GetAllUsers()
        {
            List<UserAccount> items = new List<UserAccount>();

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM UserAccounts";
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        items.Add(new UserAccount
                        {
                            Serial = Convert.ToInt32(reader["Serial"].ToString()),
                            Id = Guid.Parse(reader["Id"].ToString()),
                            EMail = reader["EMail"].ToString(),
                            Password = reader["Password"].ToString(),
                            Type = reader["Type"].ToString(),
                        });
                    }
                }
            }

            return items;
        }


        public void AddUser(UserAccount userAccount)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = "INSERT INTO UserAccounts VALUES(" + "'" + userAccount.Id.ToString() + "','" + userAccount.EMail.ToString() + "','" + userAccount.Password.ToString() + "','" + userAccount.Type.ToString() + "'" + ")";
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(UserAccount userAccount)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = "UPDATE UserAccounts SET EMail='" + userAccount.EMail.ToString() + "', Password='" + userAccount.Password.ToString() + "', Type='" + userAccount.Type.ToString() + "'" + " WHERE Id = '" + userAccount.Id + "'";
                    int rows = command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(UserAccount userAccount)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = "DELETE FROM UserAccounts WHERE Id = " + "'" + userAccount.Id.ToString() + "'";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}