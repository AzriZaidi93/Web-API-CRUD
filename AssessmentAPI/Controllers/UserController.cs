using AssessmentAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetUser()
        {
            string query = @"select userid,username,email,phoneNo,skillsets,hobby from User";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserDBConn");
            MySqlDataReader myReader;

            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mycommand = new MySqlCommand(query,myconn))
                {
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myconn.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult AddUser(User user)
        {
            string query = @"insert into User (username,email,phoneNo,skillsets,hobby) values (@username,@email,@phoneNo,@skillsets,@hobby)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserDBConn");
            MySqlDataReader myReader;

            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mycommand = new MySqlCommand(query, myconn))
                {
                    mycommand.Parameters.AddWithValue("@username", user.username.ToUpper());
                    mycommand.Parameters.AddWithValue("@email", user.email.ToLower());
                    mycommand.Parameters.AddWithValue("@phoneNo", user.phoneNo);
                    mycommand.Parameters.AddWithValue("@skillsets", user.skillset.ToUpper());
                    mycommand.Parameters.AddWithValue("@hobby", user.hobby.ToUpper());

                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myconn.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult UpdateUser(User user)
        {
            string query = @"update user set username = @username, email = @email, phoneNo = @phoneNo, skillsets = @skillsets, hobby = @hobby 
                             where userid = @userid";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserDBConn");
            MySqlDataReader myReader;

            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mycommand = new MySqlCommand(query, myconn))
                {
                    mycommand.Parameters.AddWithValue("@userid", user.userid);
                    mycommand.Parameters.AddWithValue("@username", user.username.ToUpper());
                    mycommand.Parameters.AddWithValue("@email", user.email.ToLower());
                    mycommand.Parameters.AddWithValue("@phoneNo", user.phoneNo);
                    mycommand.Parameters.AddWithValue("@skillsets", user.skillset.ToUpper());
                    mycommand.Parameters.AddWithValue("@hobby", user.hobby.ToUpper());

                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myconn.Close();
                }
            }

            return new JsonResult("Update Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult DeleteUser(int id)
        {
            string query = @"delete from user
                             where userid = @userid";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserDBConn");
            MySqlDataReader myReader;

            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mycommand = new MySqlCommand(query, myconn))
                {
                    mycommand.Parameters.AddWithValue("@userid", id);


                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myconn.Close();
                }
            }

            return new JsonResult("Delete Successfully");
        }
    }
}
