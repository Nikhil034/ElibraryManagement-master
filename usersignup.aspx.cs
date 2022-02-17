using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class usersignup : System.Web.UI.Page
{
    String constring;
    SqlConnection con;
    public void connect()
    {
        constring = WebConfigurationManager.ConnectionStrings["econ"].ConnectionString;
        con = new SqlConnection(constring);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
       
        if (!IsPostBack)
        {
            connect();
            con.Open();

            SqlCommand cmd2 = new SqlCommand("select member_id from Member_master_tbl order by member_id desc", con);
            SqlDataReader sdr = cmd2.ExecuteReader();
            if (sdr.Read())
            {
                String id = sdr["member_id"].ToString();
                int mid = Convert.ToInt16(id);
                mid = mid + 1;
                Response.Write(mid);
                TextBox8.Text = Convert.ToString(mid);
                TextBox8.ReadOnly = true;
            }
            else
            {
                TextBox8.Text = "1000";
                TextBox8.ReadOnly = true;
            }
        }
        
 
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

       
        connect();
        con.Open();
        SqlCommand cmd = new SqlCommand("insert_member", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@fnm",TextBox1.Text);
        cmd.Parameters.AddWithValue("@dob",TextBox2.Text);
        cmd.Parameters.AddWithValue("@no",TextBox3.Text);
        cmd.Parameters.AddWithValue("@em",TextBox4.Text);
        cmd.Parameters.AddWithValue("@stat", DropDownList1.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@ct", TextBox5.Text);
        cmd.Parameters.AddWithValue("@pin", TextBox6.Text);
        cmd.Parameters.AddWithValue("@add", TextBox7.Text);
        cmd.Parameters.AddWithValue("@id", TextBox8.Text);
        cmd.Parameters.AddWithValue("@pass", TextBox9.Text);
        cmd.Parameters.AddWithValue("@status","pending");
       int i=cmd.ExecuteNonQuery();
        if (i > 0)
        {
            Response.Write("<script>alert('Thanks you')</script>");
            
        }
        
    }
}