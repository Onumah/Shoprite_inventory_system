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
    public partial class ProductForm1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PANDA\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductForm1()
        {
            InitializeComponent();
            loadProduct();
        }
        public void loadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE'%"+ txtSearch.Text +"%' ", conn);
            conn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            conn.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModuleForm1 productModule1 = new ProductModuleForm1();
            productModule1.btnSave.Enabled = true;
            productModule1.btnUpdate.Enabled = false;
            productModule1.ShowDialog();
            loadProduct();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm1 productModuleForm1 = new ProductModuleForm1();
                productModuleForm1.lblPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModuleForm1.txtPname.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModuleForm1.txtQty.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                productModuleForm1.txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModuleForm1.txtDescription.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                productModuleForm1.comboQty.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                productModuleForm1.btnSave.Enabled = false;
                productModuleForm1.btnUpdate.Enabled = true;
                productModuleForm1.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE pid LIKE'" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            loadProduct();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }
    }
}
