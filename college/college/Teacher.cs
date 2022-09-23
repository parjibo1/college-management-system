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
    public partial class Teacher : Form
    {
        public Teacher()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectModels;Integrated Security=True");

        private void fillDepartment()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Depname from DepartmentTbl ", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Depname",typeof(string));
            dt.Load(rdr);
            DepCb.ValueMember = "Depname";
            DepCb.DataSource = dt;

            
                con.Close();
        }
        private void populate()
        {
            con.Open();
            string query = "Select *from TeacherTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TeacherGV.DataSource = ds.Tables[0];
            con.Close();
        }


        private void Teacher_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (idC.Text == "" ||nameC.Text == "" ||phoneC.Text == ""||adressC.Text=="")
                {
                    MessageBox.Show("Enter the deptm name");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TeacherTbl Values(" +idC.Text + ",'" +nameC.Text + "','" +genderC.SelectedItem.ToString() + "','"+dopC.Text+"','"+phoneC.Text+"','"+DepCb.SelectedValue.ToString()+"','"+adressC.Text+"')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher successfully Added");
                    con.Close();
                      populate();

                }
            }
            catch
            {
                MessageBox.Show("something went wrong");

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           idC.Text =TeacherGV.SelectedRows[0].Cells[0].Value.ToString();
            nameC.Text =TeacherGV.SelectedRows[0].Cells[1].Value.ToString();
            genderC.SelectedItem= TeacherGV.SelectedRows[0].Cells[2].Value.ToString();

            phoneC.Text =TeacherGV.SelectedRows[0].Cells[6].Value.ToString();
            adressC.Text = TeacherGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (idC.Text == "")
                {
                    MessageBox.Show("Enter The Teacher id");
                }
                else
                {
                    con.Open();
                    string query = "delete  from TeacherTbl where Teacherid='" +idC.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher deleted successfully");
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
                if (idC.Text == "" ||nameC.Text == "" ||adressC.Text == "" || phoneC.Text=="")
                {
                    MessageBox.Show("Missing data");
                }
                else
                {
                    con.Open();
                    string query = "update TeacherTbl Set Teachername='" +nameC.Text + "',Teachergender='" +genderC.SelectedItem.ToString() +"',TeacherDOB='"+dopC.Text+"',Teacherphone='"+phoneC.Text+"',TeacherDep='"+DepCb.SelectedValue.ToString()+"',TeacherAdd='"+adressC.Text+"'  where Teacherid='" +idC.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher updated successfully");
                    con.Close();
                    populate();
                }
            }
            catch(Exception ex)
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
