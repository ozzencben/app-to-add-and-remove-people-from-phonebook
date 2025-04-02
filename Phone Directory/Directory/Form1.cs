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

namespace Directory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=SOL-9\SQLEXPRESS;Initial Catalog=Directory;Integrated Security=True;TrustServerCertificate=True");

        void getList()
        {
            DataTable table = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Persons", connection);
            dataAdapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        void clear()
        {
            nametxt.Text = "";
            lasttxt.Text = "";
            phonemasktxt.Text = "";
            mailtxt.Text = "";
            nametxt.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getList();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("insert into Persons (Name,Lastname,Phone,Mail) values (@p1,@p2,@p3,@p4)", connection);
            cmd.Parameters.AddWithValue("@p1", nametxt.Text);
            cmd.Parameters.AddWithValue("@p2", lasttxt.Text);
            cmd.Parameters.AddWithValue("@p3", phonemasktxt.Text);
            cmd.Parameters.AddWithValue("@p4", mailtxt.Text);
            cmd.ExecuteNonQuery();
            connection.Close();
            getList();
            MessageBox.Show("Person added to contacts","Add Person",MessageBoxButtons.OK,MessageBoxIcon.Information);
            clear();
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int chosen = dataGridView1.SelectedCells[0].RowIndex;
            ıdtxt.Text = dataGridView1.Rows[chosen].Cells[0].Value.ToString();
            nametxt.Text = dataGridView1.Rows[chosen].Cells[1].Value.ToString();
            lasttxt.Text = dataGridView1.Rows[chosen].Cells[2].Value.ToString();
            phonemasktxt.Text = dataGridView1.Rows[chosen].Cells[3].Value.ToString();
            mailtxt.Text = dataGridView1.Rows[chosen].Cells[4].Value.ToString();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            DialogResult desicion = MessageBox.Show("Are you sure you want to delete the contact?", "Delete Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (desicion == DialogResult.Yes)
            {
                connection.Open();
                SqlCommand cmdI = new SqlCommand("Delete from Persons where ID =" + ıdtxt.Text, connection);
                cmdI.ExecuteNonQuery();
                connection.Close();
                getList();
                MessageBox.Show("The person was deleted from the contact list.", "Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Deletion canceled", "Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void uptadebtn_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmdII = new SqlCommand("update Persons set Name = @p1, Lastname = @p2, Mail = @p3, Phone = @p4 where ID = @p5", connection);
            cmdII.Parameters.AddWithValue("@p1", nametxt.Text);
            cmdII.Parameters.AddWithValue("@p2", lasttxt.Text);
            cmdII.Parameters.AddWithValue("@p3", mailtxt.Text);
            cmdII.Parameters.AddWithValue("@p4", phonemasktxt.Text);
            cmdII.Parameters.AddWithValue("@p5", ıdtxt.Text);
            cmdII.ExecuteNonQuery();
            connection.Close();
            getList();
            MessageBox.Show("Person updated successfully", "Update Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clear();
        }
    }
}
