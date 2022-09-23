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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace college
{
    public partial class Longin : Form
    {
        public Longin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectModels;Initial Catalog=Seconddatabase;Integrated Security=True;Pooling=False;");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) From LONGIN where UserName='" + maskedTextBox1.Text + "'and Password='" + maskedTextBox2.Text + "'", con);
            DataTable dt
                 = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                MainForm ss = new MainForm();
                ss.Show();
            }
            else
            {
                MessageBox.Show("Please Check Your username And Password");
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    }

