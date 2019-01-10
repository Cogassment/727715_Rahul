using RLG_Project_ADO_1.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace RLG_Project_ADO_1.Controllers
{
    public class HomeController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
        /// Summary
        /// The default class which will be used to extract the database data and will be redirect to the Index View.
        /// Summary
        public ActionResult Index()
        {
            using (SqlConnection sqlConnection = new SqlConnection(constr))
            {

                SqlCommand sqlCommand = new SqlCommand("Select * From Items", sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand.CommandText, sqlConnection);
                sqlConnection.Open();
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();

                List<ItemModel> itemModels = new List<ItemModel>();
                foreach (DataRow item in dataTable.Rows.Cast<DataRow>().ToList())
                {
                    ItemModel details = new ItemModel();
                    details.Id = int.Parse(item[0].ToString());
                    details.Name = (item[1].ToString());
                    details.Cost = int.Parse(item[2].ToString());
                    itemModels.Add(details);
                }

                return View(itemModels);
            }
        }
        /// Summary
        /// // On Click Update Button, the following ActionResult will update the entered valid value in the database.
        /// Summary
        [HttpPost]
        public ActionResult UpdateItem(ItemModel item)
        {
            string sqlquery = "Update Items Set Name='" + item.Name + "',Cost=" + item.Cost + " where Id=" + item.Id;
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
