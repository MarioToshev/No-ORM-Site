using Dummyy.Models.DTOs;
using Dummyy.Models.UserViewModels;
using Dummyy.Services.Convertors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace Dummyy.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;
        private Convertor _convertor;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            _convertor = new Convertor();
        }


        public List<User> GetAll()
        {
            string query = @"
                            select * from tutorials_tbl";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DummyDb");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return _convertor.ConvertDataTable<User>(table);
        }
        public UserViewModel GetOne(string Id)
        {
            string query = @"
                            select * from tutorials_tbl
                             where Id = (@Id)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DummyDb");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@Id", Id);
                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return _convertor.ConvertDataTable<UserViewModel>(table)[0];
        }

        public JsonResult Add(CreateUserViewModel user)
        {
            string query = @"
                            INSERT INTO dummy.tutorials_tbl (Name,Password)
                             VALUES(@Name,@Password)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DummyDb");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@Name", user.Name);
                    mySqlCommand.Parameters.AddWithValue("@Password", user.Password);
                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Succesfuly");
        }
        public UserViewModel Update(UserViewModel user)
        {
            string query = @"
                            UPDATE dummy.tutorials_tbl SET Name = @Name , Password = @Password
                            WHERE Id = @Id;
";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DummyDb");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@Id", user.Id);
                    mySqlCommand.Parameters.AddWithValue("@Name", user.Name);
                    mySqlCommand.Parameters.AddWithValue("@Password", user.Password);
                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return user;
        }
        public void Delete(string Id)
        {
            string query = @"
                           DELETE FROM dummy.tutorials_tbl WHERE Id = @Id;
                          ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DummyDb");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@Id", Id);
                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
        }

    }
}
