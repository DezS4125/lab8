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
    public partial class editPoint : Form
    {
        string classID;
        string subjectID;
        public editPoint(string class_id, string subject_id)
        {
            InitializeComponent();
            classID = class_id;
            subjectID = subject_id;
            txtClass.Text = classID;
            txtSubject.Text = subjectID;
            initializeDgvClasses();
        }
        public void initializeDgvClasses()
        {
            clsDatabase.openConnection();
            string query = "SELECT s.mssv, s.student_name, ts.score " +
                "FROM students s left JOIN (select student_id, score from testScore where subject_id=@subject_id) ts " +
                "on s.mssv = ts.student_id " +
                "where s.class=@class_id";
            SqlCommand com = new SqlCommand(query, clsDatabase.con);
            SqlParameter p1 = new SqlParameter("@subject_id", SqlDbType.NVarChar);
            p1.Value = subjectID;
            com.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@class_id", SqlDbType.NVarChar);
            p2.Value = classID;
            com.Parameters.Add(p2);
            SqlDataReader reader = com.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dgvScoreBoard.DataSource = dataTable;
            reader.Close();
            clsDatabase.closeConnection();
        }

        private void editPoint_Load(object sender, EventArgs e)
        {

        }

        private void dgvClasses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvScoreBoard.Rows)
            {
                string mssv = row.Cells["mssv"].Value?.ToString(); 
                string subject = subjectID;
                string score = row.Cells["score"].Value?.ToString();
                if (mssv!=null && score != null && mssv!="" && score!="")
                {
                    clsDatabase.openConnection();
                    SqlCommand command = new SqlCommand("InsertOrUpdateScore", clsDatabase.con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@student_id", mssv);
                    command.Parameters.AddWithValue("@subject_id", subject);
                    command.Parameters.AddWithValue("@score", score);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        clsDatabase.closeConnection();
                    }
                }
            }
            MessageBox.Show("Student score updated!");
        }
    }
}
