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
    public partial class SalesForm1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PANDA\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public SalesForm1()
        {
            InitializeComponent();
            loadOrder();
        }

        public void loadOrder()
        {
            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbOrder", conn);
            conn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            conn.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SalesModuleForm1 salesModuleForm1 = new SalesModuleForm1();
            salesModuleForm1.btnInsert.Enabled = true;
            salesModuleForm1.btnUpdate.Enabled = false;
            salesModuleForm1.ShowDialog();
            loadOrder();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
           
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Order?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cm = new SqlCommand("DELETE FROM tbOrder WHERE orid LIKE'" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            loadOrder();
        }
    }
}
