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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab8
{
    public partial class FormPersonalPage : Form
    {
        string staff_id;
        public FormPersonalPage(string staff_id)
        {
            InitializeComponent();
            this.staff_id = staff_id;
            clsDatabase.openConnection();
            txtStaffID.Text = this.staff_id;
            SqlCommand com = new SqlCommand("select staff_name from staffs where staff_id=@staffID",clsDatabase.con);
            SqlParameter p1 = new SqlParameter("@staffID", SqlDbType.NVarChar);
            p1.Value = this.staff_id;
            com.Parameters.Add(p1);
            txtStaffName.Text = (string) com.ExecuteScalar();
            clsDatabase.closeConnection();
            initializeClassList();
        }
        private void initializeClassList()
        {
            clsDatabase.openConnection();
            SqlCommand com = new SqlCommand("select subject_id, class_id from teach where staff_id=@staffID", clsDatabase.con);
            SqlParameter p1 = new SqlParameter("@staffID", SqlDbType.NVarChar);
            p1.Value = staff_id;
            com.Parameters.Add(p1);
            SqlDataReader reader = com.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dgvClasses.DataSource = dataTable;
            dgvClasses.Columns["subject_id"].HeaderText = "Subject";
            dgvClasses.Columns["class_id"].HeaderText = "Class";
            reader.Close();
            clsDatabase.closeConnection();

        }


        private void FormPersonalPage_Load(object sender, EventArgs e)
        {

        }

        private void dgvClasses_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvClasses_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvClasses.Rows.Count)
            {
                string subjectId = dgvClasses.Rows[e.RowIndex].Cells["subject_id"].Value?.ToString();
                string classId = dgvClasses.Rows[e.RowIndex].Cells["class_id"].Value?.ToString();

                MessageBox.Show($"Subject ID: {subjectId}\nClass ID: {classId}", "Row Details");
            }
        }

    }
}
