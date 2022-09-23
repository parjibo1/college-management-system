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
using System.Xml.Linq;


namespace college
{
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectModels;Integrated Security=True");
        private void fillDepartment()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Depname from DepartmentTbl ", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Depname", typeof(string));
            dt.Load(rdr);
            depS.ValueMember = "Depname";
            depS.DataSource = dt;


            con.Close();
        }

        private void populate()
        {
            con.Open();
            string query = "Select *from StudentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            studentGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (idS.Text == "" ||nameS.Text == "" ||phoneS.Text == "" ||feesS.Text == "")
                {
                    MessageBox.Show("Enter the deptm name");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into StudentTbl Values(" +idS.Text + ",'" +nameS.Text + "','" +GenderS.SelectedItem.ToString() + "','" +dopS.Text + "','" +phoneS.Text + "','" + depS.SelectedValue.ToString() + "','" +feesS.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student successfully Added");
                    con.Close();
                    populate();

                }
            }
            catch
            {
                MessageBox.Show("something went wrong");

            }
        }

        private void Student_Load(object sender, EventArgs e)
        {
            populate();
            fillDepartment();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void studentGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idS.Text =studentGV.SelectedRows[0].Cells[0].Value.ToString();
            nameS.Text =studentGV.SelectedRows[0].Cells[1].Value.ToString();
            GenderS.SelectedItem =studentGV.SelectedRows[0].Cells[2].Value.ToString();

            phoneS.Text = studentGV.SelectedRows[0].Cells[6].Value.ToString();
            feesS.Text =studentGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (idS.Text =="")
                {
                    MessageBox.Show("Enter The Student id");
                }
                else
                {
                    con.Open();
                    string query = "delete  from StudentTbl where Stdid=" +idS.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student deleted successfully");
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
                if (idS.Text == "" || nameS.Text == "" || phoneS.Text == "" || feesS.Text == "")
                {
                    MessageBox.Show("Missing data");
                }
                else
                {
                    con.Open();
                    string query = "update StudentTbl Set Stdname='" +nameS.Text + "',StdGender='" +GenderS.SelectedItem.ToString() + "',StdDOB='" +dopS.Text + "',Stdphone='" +phoneS.Text + "',StdDep='" +depS.SelectedValue.ToString() + "',StdFees='" +feesS.Text + "'  where Stdid='" +idS.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student updated successfully");
                    con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

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
