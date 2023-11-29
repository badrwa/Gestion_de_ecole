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
    public partial class Sign_Up : Form
    {
        public Sign_Up()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=Gestion_ecole;Integrated Security=True"; 
        SqlConnection connection;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfn.Text != "" && txtln.Text != "" && txtbd.Text != "" && txtg.Text != "" && txta.Text != "" &&
                    txte.Text != "" && txtp.Text != "" && txtcp.Text != "")
                {
                    if (txtp.Text == txtcp.Text)
                    {
                        int v = check(txte.Text);
                        if (v != 1)
                        {
                            connection = new SqlConnection(connectionString);
                            connection.Open();
                            SqlCommand command = new SqlCommand("insert into prof_login values(@f_name,@l_name,@b_day,@gender,@address,@email,@password)", connection);
                            command.Parameters.AddWithValue("@f_name", txtfn.Text);
                            command.Parameters.AddWithValue("@l_name", txtln.Text);
                            command.Parameters.AddWithValue("@b_day", Convert.ToDateTime(txtbd.Text));
                            command.Parameters.AddWithValue("@gender", txtg.Text);
                            command.Parameters.AddWithValue("@address", txta.Text);
                            command.Parameters.AddWithValue("@email", txte.Text);
                            command.Parameters.AddWithValue("@password", txtp.Text);
                            command.ExecuteScalar();
                            connection.Close();
                            MessageBox.Show("Register Succesfully!");
                            txtfn.Text = "";
                            txtln.Text = "";
                            txtg.Text = "";
                            txta.Text = "";
                            txte.Text = "";
                            txtp.Text = "";
                            txtcp.Text = "";


                        }
                        else
                        {
                            MessageBox.Show("You are already registered");
                        }
                    }
                    else
                    {
                        MessageBox.Show("The password does not match");
                    }

                }
                else
                {
                    MessageBox.Show("Fill in the blinks!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        int check(string email)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "select count(*) from prof_login where email= '" + email + "'";
            SqlCommand command = new SqlCommand(query, connection);
            int v = (int)command.ExecuteScalar();
            connection.Close();
            return v;
        }

        private void txtrm_CheckedChanged(object sender, EventArgs e)
        {
            if (txtrm.Checked)
            {
                txtp.PasswordChar = '\0';
                txtcp.PasswordChar = '\0';

            }
            else
            {
                txtp.PasswordChar = '*';
                txtcp.PasswordChar = '*';
            }
        }
    }
}
