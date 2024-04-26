using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace ADODbConnection
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // step1 - write query
                string qry = "insert into employee values(@name, @city, @salary)";
                // create object of command and assign the query
                cmd = new SqlCommand(qry, con);
                // assign values to parameters
                cmd.Parameters.AddWithValue("@name",txtEmpName.Text);
                cmd.Parameters.AddWithValue("@city", txtCity.Text);
                cmd.Parameters.AddWithValue("@salary", txtSalary.Text);
                //fire the query
                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                con.Close();
            }

        }
    }
}
