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

namespace ShopRite
{
    public partial class CategoryForm1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PANDA\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CategoryForm1()
        {
            InitializeComponent();
            loadCategory();
        }
        public void loadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCategory", conn);
            conn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            conn.Close();

        }


        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryModuleForm1 categoryModuleForm1 = new CategoryModuleForm1();
                categoryModuleForm1.lblCatId.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                categoryModuleForm1.txtCatname.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();
             


                categoryModuleForm1.btnSave.Enabled = false;
                categoryModuleForm1.btnUpdate.Enabled = true;
                categoryModuleForm1.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Category?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cm = new SqlCommand("DELETE FROM tbCategory WHERE catid LIKE'" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            loadCategory();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryModuleForm1 formModule1 = new CategoryModuleForm1();
            formModule1.btnSave.Enabled = true;
            formModule1.btnUpdate.Enabled = false;
            formModule1.ShowDialog();
            loadCategory();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
