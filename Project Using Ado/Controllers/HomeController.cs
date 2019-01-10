using RLG_Project_ADO_1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace RLG_Project_ADO_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RLG Project ADO 1\RLG Project ADO 1\App_Data\Item.mdf;Integrated Security=True");
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * from Items",sqlConnection);
            sqlCommand.ExecuteNonQuery();
            List<ItemModel> it = new List<ItemModel>();
            ItemModel details = new ItemModel();
            using (SqlDataReader read = sqlCommand.ExecuteReader())
            {
                while (read.Read())
                {
                    details = new ItemModel();
                    details.Id = int.Parse(read["Id"].ToString());
                    details.Name = (read["Name"].ToString());
                    details.Cost = int.Parse(read["Cost"].ToString());
                    it.Add(details);
                }
                sqlConnection.Close();
            }
            return View(it);
        }
        // On Click Update
        [HttpPost]
        public ActionResult UpdateItem(ItemModel item)
        {
                string sqlquery = "update Items SET Name='" + item.Name + "',Cost=" + item.Cost + " where Id=" + item.Id;
                string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RLG Project ADO 1\RLG Project ADO 1\App_Data\Item.mdf;Integrated Security=True";
                using (SqlConnection sqlConnection = new SqlConnection(constr))
                {
                sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(sqlquery, sqlConnection))
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }
                return new EmptyResult();            
        }
    }
}
