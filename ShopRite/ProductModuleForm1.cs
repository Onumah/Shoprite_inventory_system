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
    public partial class ProductModuleForm1 : Form
    {

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PANDA\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm1()
        {
            InitializeComponent();
            loadCategory();
        }

        public void loadCategory()
        {
            comboQty.Items.Clear();
            cm = new SqlCommand("SELECT catname FROM tbCategory", conn);
            conn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboQty.Items.Add(dr[0].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void ProductModuleForm1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbProduct(pname,pqty,pprice,pdescription,pcategory)VALUES(@pname,@pqty,@pprice,@pdescription,@pcategory)", conn);
                    cm.Parameters.AddWithValue("@pname", txtPname.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtQty.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPrice.Text));
                    cm.Parameters.AddWithValue("@pdescription", txtDescription.Text);
                    cm.Parameters.AddWithValue("@pcategory", comboQty.Text);
                    conn.Open();
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Product has been successfully saved");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        public void Clear()
        {
            txtPname.Clear();
            txtQty.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            comboQty.Text = "";
        }

        private void comboQty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Product?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbProduct SET pname=@pname, pqty=@pqty, pprice=@pprice,pdescription=@pdescription,pcategory=@pcategory WHERE pid LIKE'" + lblPid.Text + "' ", conn);
                    cm.Parameters.AddWithValue("@pname", txtPname.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtQty.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPrice.Text));
                    cm.Parameters.AddWithValue("@pdescription", txtDescription.Text);
                    cm.Parameters.AddWithValue("@pcategory", comboQty.Text);
                    conn.Open();
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Product has been successfully updated!");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
