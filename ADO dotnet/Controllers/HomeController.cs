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
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RLG Project ADO 1\RLG Project ADO 1\App_Data\Item.mdf;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from Items", con);
            cmd.ExecuteNonQuery();
            List<ItemModel> it = new List<ItemModel>();
            ItemModel details = new ItemModel();

            using (SqlDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    details = new ItemModel();
                    details.Id = int.Parse(read["Id"].ToString());
                    details.Name = (read["Name"].ToString());
                    details.Cost = int.Parse(read["Cost"].ToString());
                    it.Add(details);
                }
                con.Close();
            }
            return View(it);
        }

        [HttpPost]
        public ActionResult UpdateItem(ItemModel item)
        {
                string query = "update Items SET Name='" + item.Name + "',Cost=" + item.Cost + " where Id=" + item.Id;
                string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RLG Project ADO 1\RLG Project ADO 1\App_Data\Item.mdf;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;

                        cmd.ExecuteNonQuery();

                    }
                    con.Close();
                }

                return new EmptyResult();
            
        }


    }
}
