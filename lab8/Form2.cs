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
            p1.Value = this.staff_id;
            com.Parameters.Add(p1);
            txtStaffName.Text = (string)com.ExecuteScalar();
            SqlDataReader reader = com.ExecuteReader();

            lvClasses.View = View.Details;
            lvClasses.Columns.Add("Subject ID");
            lvClasses.Columns.Add("Class ID");

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["subject_id"].ToString());
                item.SubItems.Add(reader["class_id"].ToString());
                lvClasses.Items.Add(item);
            }

            reader.Close();
            clsDatabase.closeConnection();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormPersonalPage_Load(object sender, EventArgs e)
        {

        }
        private void lvClasses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
