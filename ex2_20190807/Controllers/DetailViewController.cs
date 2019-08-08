using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using ex2_20190807.Models;

namespace ex2_20190807.Controllers
{
    public class DetailViewController : Controller
    {
        // GET: DetailView
        public ActionResult Detail(string id)
        {
            DataTable dt = new DataTable();
            string constr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["NorthwindConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))

            {
                string query = "SELECT [CustomerID],[CompanyName],[ContactName],[ContactTitle] FROM Customers Where [CustomerID]= @id";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@id", SqlDbType.NChar, 5);
                    cmd.Parameters["@id"].Value = id;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }

            var item = new Models.DetailViewModel();

            item.CompanyName = dt.Rows[0]["CompanyName"].ToString();
            item.CustomerID = dt.Rows[0]["CustomerID"].ToString();
            item.ContactName = dt.Rows[0]["ContactName"].ToString();
            item.ContactTitle = dt.Rows[0]["ContactTitle"].ToString();

            return View(item);
        }

        [HttpPost]
        public ActionResult Detail(DetailViewModel viewModle)
        {
            if (!this.ModelState.IsValid)

            {
                return View(viewModle);
            }
            DataTable dt = new DataTable();
            string constr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["NorthwindConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))

            {
                string query = "UPDATE [Customers] SET[ContactName]=@ContactName,[ContactTitle]=@ContactTitle   Where [CustomerID]= @id";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@id", SqlDbType.NChar, 5);
                    cmd.Parameters["@id"].Value = viewModle.CustomerID;
                    cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar, 30);
                    cmd.Parameters["@ContactName"].Value = viewModle.ContactName;
                    cmd.Parameters.Add("@ContactTitle", SqlDbType.NVarChar, 30);
                    cmd.Parameters["@ContactTitle"].Value = viewModle.ContactTitle;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }




            return this.View("Detail", viewModle);
        }
    }
}