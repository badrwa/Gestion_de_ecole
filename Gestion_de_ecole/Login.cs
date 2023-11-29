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

namespace Gestion_de_ecole
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=Gestion_ecole;Integrated Security=True"; 
        SqlConnection connection;
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Sign_Up().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txte.Text != "" && txtp.Text != "")
            {

                connection = new SqlConnection(connectionString);
                connection.Open();
                string query = "select count(*) from prof_login where email= '" + txte.Text + "' and password='" + txtp.Text + "'";
                SqlCommand command = new SqlCommand(query, connection);
                int v = (int)command.ExecuteScalar();
                connection.Close();
                if (v != 1)
                {
                    MessageBox.Show("Error email or password", "Error!");
                }
                else
                {
                    //    MessageBox.Show("Welcome to your profile!");
                    new Loading().Show();
                    this.Hide();

                }

            }
            else
            {
                MessageBox.Show("Fill the blanks!");
            }
        }

        private void txtrm_CheckedChanged(object sender, EventArgs e)
        {
            if (txtrm.Checked)
            {
                txtp.PasswordChar = '\0';

            }
            else
            {
                txtp.PasswordChar = '*';

            }
        }
    }
}
