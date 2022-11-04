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

namespace Eye_Managment_System_Front
{
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
            DisplayPat();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\HP\DOCUMENTS\CLINICDB.MDF;Integrated Security=True;Connect Timeout=30");
        private void DisplayPat()
        {
            Con.Open();
            String Query = "Select * from PatientTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PatientDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        int Key = 0;
        private void clear()
        {
            PatName.Text = "";
            PatGen.SelectedIndex = 0;
            PatAdd.Text = "";
            PatPhone.Text = "";
            PatAl.Text = "";
            Key = 0;
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (PatName.Text == "" || PatAl.Text == "" || PatPhone.Text == "" || PatAdd.Text == "" || PatGen.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into PatientTbl(PatName,PatGen,PatAdd,PatPhone,PatAl)values(@PN,@PG,@PA,@PP,@PAl)", Con);
                    cmd.Parameters.AddWithValue("@PN", PatName.Text);
                    cmd.Parameters.AddWithValue("@PG", PatGen.SelectedItem.ToString());
                    //cmd.Parameters.AddWithValue("@PD", PatDate.Value.Date);
                    cmd.Parameters.AddWithValue("@PA", PatAdd.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhone.Text);
                    cmd.Parameters.AddWithValue("@PAl", PatAl.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Added");
                    Con.Close();
                    DisplayPat();
                    clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            {
                if (PatName.Text == "" || PatAl.Text == "" || PatPhone.Text == "" || PatAdd.Text == "" || PatGen.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("update PatientTbl set PatName=@PN , PatGen=@PG, PatAdd=@PA, PatPhone=@PP, PatAl=@PAl where PatId=@Pkey", Con);
                        cmd.Parameters.AddWithValue("@PN", PatName.Text);
                        cmd.Parameters.AddWithValue("@PG", PatGen.SelectedItem.ToString());
                        //cmd.Parameters.AddWithValue("@PD", PatDate.Value.Date);
                        cmd.Parameters.AddWithValue("@PA", PatAdd.Text);
                        cmd.Parameters.AddWithValue("@PP", PatPhone.Text);
                        cmd.Parameters.AddWithValue("@PAl", PatAl.Text);
                        cmd.Parameters.AddWithValue("@PKey", Key);


                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Patient Updated");
                        Con.Close();
                        DisplayPat();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }

                }
            }
        }

        private void PatientDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Patient");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from PatientTbl where PatId=@PKey", Con);

                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Deleted");
                    Con.Close();
                    DisplayPat();
                    clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Patient_Load(object sender, EventArgs e)
        {

        }

        private void PatientDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatName.Text = PatientDGV.SelectedRows[0].Cells[1].Value.ToString();
            PatGen.SelectedItem = PatientDGV.SelectedRows[0].Cells[2].Value.ToString();
            //PatDate.Text = PatientDGV.SelectedRows[0].Cells[3].Value.ToString();
            PatAdd.Text = PatientDGV.SelectedRows[0].Cells[3].Value.ToString();
            PatPhone.Text = PatientDGV.SelectedRows[0].Cells[4].Value.ToString();
            PatAl.Text = PatientDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (PatName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PatientDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void PatPhone_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter a number ");
            }

        }

        private void PatPhone_TextChanged(object sender, EventArgs e)
        {
            if (PatPhone.TextLength == 11)
            {
                PatPhone.ForeColor = Color.Green;

            }
            else
                PatPhone.ForeColor = Color.Red;
        }

        private void PatAl_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
