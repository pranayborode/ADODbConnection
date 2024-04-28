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

       private void ClearFields()
        {
            txtEmpId.Clear();
            txtEmpName.Clear();
            txtCity.Clear();
            txtSalary.Clear();
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
                    ClearFields();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // step1 - write query
                string qry = "update employee set name=@name, city=@city, salary=@salary where id=@id";
                // create object of command and assign the query
                cmd = new SqlCommand(qry, con);
                // assign values to parameters

                cmd.Parameters.AddWithValue("@id", txtEmpId.Text);
                cmd.Parameters.AddWithValue("@name", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@city", txtCity.Text);
                cmd.Parameters.AddWithValue("@salary", txtSalary.Text);
               

                //fire the query
                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ClearFields();

            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from employee where id = @id";
        
                cmd = new SqlCommand(qry, con);

                cmd.Parameters.AddWithValue("@id", txtEmpId.Text);

                con.Open();

                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtEmpName.Text = dr["name"].ToString();
                        txtCity.Text = dr["city"].ToString();
                        txtSalary.Text = dr["salary"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record Not Found..");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from employee where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", txtEmpId.Text);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record Deleted...");
                    ClearFields();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            { 
                con.Close(); 
            }
        }

        private void btnShowAllEmp_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from employee";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(dr);

                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
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
