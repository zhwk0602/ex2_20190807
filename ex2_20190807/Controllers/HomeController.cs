using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ex2_20190807.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            string constr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["NorthwindConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT [CustomerID],[CompanyName] FROM Customers";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            var result = new List<Models.SqlViewModel>();
            foreach (DataRow x in dt.Rows)
            {
                var item = new Models.SqlViewModel();

                item.CompanyName = x["CompanyName"].ToString();
                item.CustomerID = x["CustomerID"].ToString();
                result.Add(item);
            }

            return View(result);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}