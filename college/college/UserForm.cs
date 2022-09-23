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

namespace college
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectModels;Integrated Security=True");
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            populate();
        }
        private void populate()
        {
            con.Open();
            string query = "Select *from userTbll";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            userGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (uidtb.Text == "" || usernametb.Text == "" || upassword.Text == "")
                {
                    MessageBox.Show("missing imformation");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into userTbll Values(" + uidtb.Text + ",'" + usernametb.Text + "','" + upassword.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("user successfully Added");
                    con.Close();
                    populate();

                }
            }
            catch
            {
                MessageBox.Show("something went wrong");

            }
        }

        private void userGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            uidtb.Text = userGV.SelectedRows[0].Cells[0].Value.ToString();
            usernametb.Text = userGV.SelectedRows[0].Cells[1].Value.ToString();
            upassword.Text = userGV.SelectedRows[0].Cells[2].Value.ToString();


        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (uidtb.Text == "")
                {
                    MessageBox.Show("Enter The User Id");
                }
                else
                {
                    con.Open();
                    string query = "delete  from userTbll where userid=" + uidtb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("user deleted successfully");
                    con.Close();
                    populate();

                }
            }
            catch
            {
                MessageBox.Show("something went wrong");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(uidtb.Text == "" || usernametb.Text == "" || upassword.Text == "")
                {
                    MessageBox.Show("Missing data");
                }
                else
                {
                    con.Open();
                    string query = "update userTbll Set username='" + usernametb.Text + "',password='" + upassword.Text + "'where userid=" + uidtb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("user updated successfully");
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
