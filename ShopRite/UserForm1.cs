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
    public partial class UserForm1 : Form
    {

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PANDA\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public UserForm1()
        {
            InitializeComponent();
            loadUser();
        }
        public void loadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbUser", conn);
            conn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvUser.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            conn.Close();

        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserForm1_Load(object sender, EventArgs e)
        {

        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                UserModuleForm1 userModuleForm1 = new UserModuleForm1();
                userModuleForm1.txtUsername.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModuleForm1.txtFullName.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModuleForm1.txtPass.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModuleForm1.txtPhone.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModuleForm1.btnSave.Enabled = false;
                userModuleForm1.btnUpdate.Enabled = true;
                userModuleForm1.txtUsername.Enabled = false;
                userModuleForm1.ShowDialog();
            }else if(colName == "Delete")
            {
                if(MessageBox.Show("Are you sure you want to delete this User?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cm = new SqlCommand("DELETE FROM tbUser WHERE username LIKE'" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            loadUser();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UserModuleForm1 userModuleForm1 = new UserModuleForm1();
            userModuleForm1.btnSave.Enabled = true;
            userModuleForm1.btnUpdate.Enabled = false;
            userModuleForm1.ShowDialog();
            loadUser();

        }
    }
}
