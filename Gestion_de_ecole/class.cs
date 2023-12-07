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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Xml.Linq;

namespace Gestion_de_ecole
{
    public partial class Class : Form
    {
        public Class()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=Gestion_ecole;Integrated Security=True";
        SqlConnection connection;
        SqlCommand Command;
        SqlDataAdapter dataAdapter;
        DataTable dataTable;
     

        private void Class_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'gestion_ecoleDataSet.etudient_list'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.etudient_listTableAdapter.Fill(this.gestion_ecoleDataSet.etudient_list);
            connection = new SqlConnection(connectionString);
            connection.Open();



            ChargerDonnees();

        }
        private void ChargerDonnees()
        {
            string query = "SELECT * FROM etudient_list";
            Command = new SqlCommand(query, connection);

            // Créer un objet SqlDataAdapter pour exécuter la commande SQL
            dataAdapter = new SqlDataAdapter(Command);

            // Créer un DataTable pour stocker les données
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            // Lier le DataTable au DataGridView
            dataGridView1.DataSource = dataTable;
        }
        private void Calc()
        {
            float a = 0;
            float b = 0;
            float c = 0;

            if (float.TryParse(txtfne.Text, out a) && float.TryParse(txtsn.Text, out b) && float.TryParse(txttn.Text, out c))
            {
                if (a <= 20 && a >= 0 && b <= 20 && b >= 0 && c <= 20 && c >= 0)
                {
                    float s = (a + b + c) / 3;
                    // Pass the parameter s to the SQL command
                    Command.Parameters.AddWithValue("@g_note", s);
                }
                else
                {
                    // Show a message that the input is out of range
                    MessageBox.Show("Please ensure that all inputs are between 0 and 20.");
                }
            }
            else
            {
                // Show a message that the input is not a valid float
                MessageBox.Show("Please ensure that all inputs are valid floating point numbers.");
            }
        }
        private void ADD()
        {
            txtfn.Text = "";
            txtln.Text = "";
;
            txtg.Text = "";
            txtal.Text = "";
            txtc.Text = "";
            txts.Text = "";
            txtfne.Text = "";
            txtsn.Text = "";
            txttn.Text = "";
            txtn.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        { 

            string insertQuery = "INSERT INTO etudient_list (First name, Last name, Birthday, Gender, Academic level, Class, Subject, First Note, Seconde Note, Third Note, Note) VALUES (@f_name, @l_name, @b_day, @gender, @acadamic_level, @class, @subject, @f_note, @s_note, @t_note, @note)";
            SqlCommand Command = new SqlCommand(insertQuery, connection);
            Command.Parameters.AddWithValue("@f_name", txtfn.Text);
            Command.Parameters.AddWithValue("@l_name", txtln.Text);
            Command.Parameters.AddWithValue("@b_day",Convert.ToDateTime(txtbd.Text));
            Command.Parameters.AddWithValue("@gender", txtg.Text);
            Command.Parameters.AddWithValue("@academic_level", txtal.Text);
            Command.Parameters.AddWithValue("@class", txtc.Text);
            Command.Parameters.AddWithValue("@subject", txts.Text);
            Command.Parameters.AddWithValue("@f_note", txtfne.Text);
            Command.Parameters.AddWithValue("@s_note", txtsn.Text);
            Command.Parameters.AddWithValue("@t_note", txttn.Text);
            Command.Parameters.AddWithValue("@note", txtn.Text);


        //    Command.ExecuteNonQuery();
            
            ChargerDonnees();
            Calc();

            ADD();

            MessageBox.Show("ok ");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Les données sont stockées dans les cellules de la ligne sélectionnée
                string f_name = selectedRow.Cells["First Name"].Value.ToString();
                string l_name = selectedRow.Cells["Last Name"].Value.ToString();

                string b_day = selectedRow.Cells["Birthday"].Value.ToString();
                string gender = selectedRow.Cells["Genre"].Value.ToString();
                string academic_level = selectedRow.Cells["Acadimec level"].Value.ToString();
                string Class = selectedRow.Cells["Class"].Value.ToString();

                string subject = selectedRow.Cells["Subject"].Value.ToString();
                string f_note = selectedRow.Cells["First Note"].Value.ToString();
                string s_note = selectedRow.Cells["Seconde Note"].Value.ToString();
                string t_note = selectedRow.Cells["Third Note"].Value.ToString();

                string note = selectedRow.Cells["Note"].Value.ToString();

              
                // Afficher les données dans les TextBox
                txtfn.Text = f_name;
                txtln.Text = l_name;
                txtbd.Text = b_day;
                txtg.Text = gender;
                txtal.Text = academic_level;
                txtc.Text = Class;
                txts.Text = subject;
                txtfne.Text = f_note;
                txtsn.Text = s_note;
                txttn.Text = t_note;
                txtn.Text = note;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "UPDATE etudient_list SET [First Name] = @f_name, [Last Name] = @l_name, [Birthday] = @b_day, [Genre] = @gender, [Acadimec level] = @academic_level, [Class] = @class, [Subject] = @subject, [First Note] = @f_note, [Seconde Note] = @s_note, [Third Note] = @t_note, [Note] = @note WHERE id = @id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@f_name", txtfn.Text);
            command.Parameters.AddWithValue("@l_name", txtln.Text);
            command.Parameters.AddWithValue("@b_day", Convert.ToDateTime(txtbd.Text));
            command.Parameters.AddWithValue("@gender", txtg.Text);
            command.Parameters.AddWithValue("@academic_level", txtal.Text);
            command.Parameters.AddWithValue("@class", txtc.Text);
            command.Parameters.AddWithValue("@subject", txts.Text);
            command.Parameters.AddWithValue("@f_note", txtfne.Text);
            command.Parameters.AddWithValue("@s_note", txtsn.Text);
            command.Parameters.AddWithValue("@t_note", txttn.Text);
            command.Parameters.AddWithValue("@note", txtn.Text);
            command.ExecuteNonQuery();
            ChargerDonnees();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Home().Show();
            this.Hide();
        }
    }
}
