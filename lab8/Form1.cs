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

namespace lab8
{
    public partial class FormLogin : Form
    {
        string staff_id="001";
        string password="123";
        public FormLogin()
        {
            InitializeComponent();
            btnConfirm.Focus();
        }

        private void txtStaffID_TextChanged(object sender, EventArgs e)
        {
            staff_id = txtStaffID.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            password = txtPassword.Text;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                clsDatabase.openConnection();
                SqlCommand com = new SqlCommand("SELECT dbo.validateStaffCredentials(@input_staff_id , @input_staff_password)", clsDatabase.con);
                SqlParameter p1 = new SqlParameter("@input_staff_id", SqlDbType.NVarChar);
                p1.Value = staff_id;
                com.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@input_staff_password", SqlDbType.NVarChar);
                p2.Value = password;
                com.Parameters.Add(p2);
                bool result= (bool) com.ExecuteScalar();
                clsDatabase.closeConnection();
                if (result)
                {
                    // MessageBox.Show("Login successful!","Success!", MessageBoxButtons.OK);
                    this.Hide();
                    FormPersonalPage personalpage = new FormPersonalPage(staff_id);
                    personalpage.ShowDialog();
                    this.Close();
                }
                if (!result)
                {
                    MessageBox.Show("Either username or password is wrong", "Failure!", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error" , MessageBoxButtons.OK);
            }
        }
    }
}
