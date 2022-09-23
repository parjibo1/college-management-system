using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace college
{
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectModels;Integrated Security=True");
        private void populate()
        {
            con.Open();
            string query = "Select *from DepartmentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            depGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepnameTbl.Text == "" ||Depdescr.Text == "" ||Depdura.Text == "")
                {
                    MessageBox.Show("Enter the deptm name");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DepartmentTbl Values('" +DepnameTbl.Text + "','" +Depdescr.Text + "','" +Depdura.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department successfully Added");
                    con.Close();
                    populate();

                }
            }
            catch
            {
                MessageBox.Show("something went wrong");

            }
        }

        private void Department_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepnameTbl.Text == "")
                {
                    MessageBox.Show("Enter The Dpeartment name");
                }
                else
                {
                    con.Open();
                    string query = "delete  from DepartmentTbl where Depname='" +DepnameTbl.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department deleted successfully");
                    con.Close();
                    populate();

                }
            }
            catch
            {
                MessageBox.Show("something went wrong");
            }
        }

        private void depGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DepnameTbl.Text = depGV.SelectedRows[0].Cells[0].Value.ToString();
            Depdescr.Text = depGV.SelectedRows[0].Cells[1].Value.ToString();
            Depdura.Text = depGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepnameTbl.Text == "" ||Depdescr.Text == "" ||Depdura.Text == "")
                {
                    MessageBox.Show("Missing data");
                }
                else
                {
                    con.Open();
                    string query = "update DepartmentTbl Set DepDesc='" +Depdescr.Text + "',DepDuration=" +Depdura.Text + "where Depname='" +DepnameTbl.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deparetment updated successfully");
                    con.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("something went wrong");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm ss = new MainForm();
            ss.Show();
        }
    }
}
