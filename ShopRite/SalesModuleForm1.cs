using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopRite
{
    public partial class SalesModuleForm1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PANDA\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public SalesModuleForm1()
        {
            InitializeComponent();
            loadProduct();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void loadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE'%" + txtSearchProd.Text + "%' ", conn);
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        int qty = 0;
        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtpId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtpName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            qty = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(nupQty.Value) > qty)
            {
                MessageBox.Show("The Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nupQty.Value = nupQty.Value - 1;
                return;
            }
            if (Convert.ToInt16(nupQty.Value) > 0)
            {
                int total = Convert.ToInt32(txtPrice.Text) * Convert.ToInt32(nupQty.Value);
                txtTotal.Text = total.ToString();
            }
                
        }

        private void txtSearchProd_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpId.Text == "")
                {
                    MessageBox.Show("Please select product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to insert this order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbOrder(ordate,pid,qty,price,total)VALUES(@ordate,@pid,@qty,@price,@total)", conn);
                    cm.Parameters.AddWithValue("@ordate", dtOrder.Value);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt32(txtpId.Text));
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt32(nupQty.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt32(txtTotal.Text));
                    conn.Open();
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Order has been successfully inserted");


                    cm = new SqlCommand("UPDATE tbProduct SET pqty=(pqty-@pqty) WHERE pid LIKE'" + txtpId.Text + "' ", conn);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt32(nupQty.Value));
                    conn.Open();
                    cm.ExecuteNonQuery();
                    conn.Close();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            dtOrder.Value = DateTime.Now;
            txtpId.Clear();
            txtpName.Clear();
            txtPrice.Clear();
            nupQty.Value = 1;
            txtTotal.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnInsert.Enabled = true;
            btnUpdate.Enabled = false;
        }
    }
}
