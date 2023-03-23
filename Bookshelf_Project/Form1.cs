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

namespace Bookshelf_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection mycon = new SqlConnection("Data Source=WIN-K94OOO3DLN6\\SQL2019;Initial Catalog=Bookshelf_Project;Integrated Security=True");

        void list()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from books order by b_id", mycon);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            list();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            list();
        }

        string status = "";
        private void btnAdd_Click(object sender, EventArgs e)
        {
            mycon.Open();
            SqlCommand com = new SqlCommand("insert into books(b_name,author,genre,page,status) values(@p1,@p2,@p3,@p4,@p5)", mycon);
            com.Parameters.AddWithValue("@p1", txtbookname.Text);
            com.Parameters.AddWithValue("@p2", txtauthor.Text);
            com.Parameters.AddWithValue("@p3", txtpage.Text);
            com.Parameters.AddWithValue("@p4", cmbgenre.Text);
            com.Parameters.AddWithValue("@p5", status);
            com.ExecuteNonQuery();
            mycon.Close();
            MessageBox.Show("Book added", "Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void radioBnew_CheckedChanged(object sender, EventArgs e)
        {
            status = "1";
        }

        private void radioBused_CheckedChanged(object sender, EventArgs e)
        {
            status = "0";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            mycon.Open();
            SqlCommand com = new SqlCommand("delete from books where b_id = @p1",mycon);
            com.Parameters.AddWithValue("@p1", txtbookID.Text);
            com.ExecuteNonQuery();
            mycon.Close();
            MessageBox.Show("Book deleted", "Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;
            txtbookID.Text = dataGridView1.Rows[selected].Cells[0].Value.ToString();
            txtbookname.Text = dataGridView1.Rows[selected].Cells[1].Value.ToString();
            txtauthor.Text = dataGridView1.Rows[selected].Cells[2].Value.ToString();
            cmbgenre.Text = dataGridView1.Rows[selected].Cells[3].Value.ToString();
            txtpage.Text = dataGridView1.Rows[selected].Cells[4].Value.ToString();
            if (dataGridView1.Rows[selected].Cells[5].Value == "True")
            {
                radioBnew.Checked = true;
            }
            else
            {
                radioBused.Checked = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            mycon.Open();
            SqlCommand com = new SqlCommand("update books set b_name=@p1,author=@p2,genre=@p3,page=@p4,status=@p5 where b_id=@p6", mycon);
            com.Parameters.AddWithValue("@p1", txtbookname.Text);
            com.Parameters.AddWithValue("@p2", txtauthor.Text);
            com.Parameters.AddWithValue("@p3", cmbgenre.Text);
            com.Parameters.AddWithValue("@p4", txtpage.Text);
            com.Parameters.AddWithValue("@p6", txtbookID.Text);
            if (radioBnew.Checked == true)
            {
                com.Parameters.AddWithValue("@p5", status);
            }
            if(radioBused.Checked == true)
            {
                com.Parameters.AddWithValue("@p5", status);
            }
            com.ExecuteNonQuery();
            mycon.Close();
            MessageBox.Show("Book updated", "Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("select * from books where b_name = @p1", mycon);
            com.Parameters.AddWithValue("@p1", txtSearch.Text);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("select * from books where b_name like '%"+txtSearch.Text+"%'", mycon);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
