using Microsoft.AspNetCore.Mvc;
using Querystringproject.Models;
using System.Data.SqlClient;
using System.Data;

namespace Querystringproject.Controllers
{
    public class RecordController : Controller
    {
        List<Record> rec = new List<Record>();
        public void LoadData()
        {
            SqlConnection con = new SqlConnection("server=localhost\\SQLEXPRESS;database=institute;Trusted_Connection=true;");
            SqlDataAdapter da = new SqlDataAdapter("select* from student", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                Record st1 = new Record();
                st1.Name = row["name"].ToString();
                st1.Roll = Convert.ToInt32(row["roll"]);
                st1.Mobile = row["mobile"].ToString();
                rec.Add(st1);
            }
        }            
        public IActionResult AddData()
        {
            Record obj = new Record()
            {
                Roll = 2561,    
                Name = "Naveen",
                Mobile = "4512785412"
            };      
            return View(obj);
        }
        public IActionResult InsertData()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddedData()
        {
            int roll = Convert.ToInt32(Request.Form["roll"].ToString());
            string name = Request.Form["name"].ToString();
            string mobile = Request.Form["mobile"].ToString();
            SqlConnection con = new SqlConnection("server=localhost\\SQLEXPRESS;database=institute;Trusted_Connection=true;");
            SqlCommand cmd = new SqlCommand("insert into student (roll,name,mobile)values(@roll,@name,@mobile)", con);
            cmd.Parameters.AddWithValue("@roll", roll);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@mobile", mobile);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Index");
        }

        public IActionResult EditingDetails()
        {
            LoadData();
            string Roll = Request.Query["roll"];
             Record rec1 = rec.Where(x => x.Roll == Convert.ToInt32(Roll)).FirstOrDefault();
            //foreach (Record item in rec)
            //{
            //    if (item.Roll == Convert.ToInt32(Roll))
            //    {
            //        return View(item);
            //    }
            //}
            return View(rec1);
        }

        public IActionResult Index()
        {
            LoadData();
            return View(rec);
        }        

        [HttpPost]
        public IActionResult UpdateRecord()
        {   
            int roll = Convert.ToInt32(Request.Form["roll"]);
            string name = Request.Form["name"].ToString();
            string mobile = Request.Form["mobile"].ToString();
            SqlConnection con = new SqlConnection("server=localhost\\SQLEXPRESS;database=institute;Trusted_Connection=true;");
            SqlCommand cmd = new SqlCommand("update student set name=@name ,mobile=@mobile where roll=@roll", con);
            cmd.Parameters.AddWithValue("@roll", roll);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@mobile", mobile);    
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteDetails()   
        {
            LoadData();

            string Roll = Request.Query["roll"];
             Record rec2 = rec.Where(x => x.Roll == Convert.ToInt32(Roll)).FirstOrDefault();
            //foreach (Record item in rec)
            //{
            //    if (item.Roll == Convert.ToInt32(Roll))
            //    {
            //        return View(item);
            //    }
            //}
            return View(rec2);
        }

        public IActionResult Del()
        {
            return View(rec);
        }

        [HttpPost]
        public IActionResult DeleteData()
        {
            int roll = Convert.ToInt32(Request.Form["roll"]);
            SqlConnection con = new SqlConnection("server=localhost\\SQLEXPRESS;database=institute;Trusted_Connection=true;");
            SqlCommand cmd = new SqlCommand("delete from student where roll=@roll", con);
            cmd.Parameters.AddWithValue("@roll", roll);            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Index");
        }
    }
}
