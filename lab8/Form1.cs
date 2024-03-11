using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    public partial class FormLogin : Form
    {
        string staff_id;
        string password;
        public FormLogin()
        {
            InitializeComponent();
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
            this.close();
        }
    }
}
