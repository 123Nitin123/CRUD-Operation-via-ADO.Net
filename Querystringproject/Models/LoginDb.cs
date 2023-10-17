using System.Data.SqlClient;
using System.Data;

namespace Querystringproject.Models
{
    public class LoginDb
    {
        SqlConnection con = new SqlConnection("server=localhost\\SQLEXPRESS;database=master;Trusted_Connection=true;");
        public int LoginCheck(Login ad)
        {
            SqlCommand cmd = new SqlCommand("Sp_Login", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Admin_id", ad.Admin_id);
            cmd.Parameters.AddWithValue("@Password", ad.Ad_Password);
            SqlParameter obj = new SqlParameter();
            obj.ParameterName = "@Isvalid";
            obj.SqlDbType = SqlDbType.Bit;
            obj.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(obj);
            con.Open();
            cmd.ExecuteNonQuery();
            int res = Convert.ToInt32(obj.Value);
            con.Close();
            return res;
        }

    }
}
