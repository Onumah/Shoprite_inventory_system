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

namespace ShopRite
{
    public partial class CustomerForm1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PANDA\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CustomerForm1()
        {
            InitializeComponent();
            loadCustomer();
        }
        public void loadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCustomer", conn);
            conn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            conn.Close();

        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CustomerModuleForm1 customerModuleForm1 = new CustomerModuleForm1();
                customerModuleForm1.lblCId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModuleForm1.txtCname.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerModuleForm1.txtCPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
               

                customerModuleForm1.btnSave.Enabled = false;
                customerModuleForm1.btnUpdate.Enabled = true;
                customerModuleForm1.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this User?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cm = new SqlCommand("DELETE FROM tbCustomer WHERE cid LIKE'" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            loadCustomer();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CustomerModuleForm1 moduleForm1 = new CustomerModuleForm1();
            moduleForm1.btnSave.Enabled = true;
            moduleForm1.btnUpdate.Enabled = false;
            moduleForm1.ShowDialog();
            loadCustomer();

        }
    }
}
