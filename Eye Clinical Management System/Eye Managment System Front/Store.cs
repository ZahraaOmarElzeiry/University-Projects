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
    public partial class Store : Form
    {
        public Store()
        {
            InitializeComponent();
            DisplayStore();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\HP\DOCUMENTS\CLINICDB.MDF;Integrated Security=True;Connect Timeout=30");

        private void DisplayStore()
        {
            Con.Open();
            string Query = "Select * from storetbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DEVICESDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        int Key = 0;
        private void clear()
        {
            DEVNAME.Text = "";
            DEVCOST.Text = "";
            DEVQUA.Text = "";
            Key = 0;
        }
        private void Store_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AddLabbtn_Click(object sender, EventArgs e)
        {
            if (DEVNAME.Text == "" || DEVCOST.Text == "" || DEVQUA.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information ");
            }
            else
            {
                try
                {

                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into storetbl(DEVNAME,DEVCOST,DEVQUA)values(@DN,@DC,@DQ)", Con);
                    cmd.Parameters.AddWithValue("@DN", DEVNAME.Text);
                    cmd.Parameters.AddWithValue("@DC", DEVCOST.Text);
                    cmd.Parameters.AddWithValue("@DQ", DEVQUA.SelectedItem);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Device Ordered");
                    Con.Close();
                    DisplayStore();
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
            if (DEVNAME.Text == "" || DEVCOST.Text == "" || DEVQUA.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information ");
            }
            else
            {
                try
                {

                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update storetbl Set DEVNAME=@DN, DEVQUA=@DQ ,DEVCOST=@DC where DEVNUM=@DKey", Con);
                    cmd.Parameters.AddWithValue("@DN", DEVNAME.Text);
                    cmd.Parameters.AddWithValue("@DC", DEVCOST.Text);
                    cmd.Parameters.AddWithValue("@DQ", DEVQUA.SelectedItem);
                    cmd.Parameters.AddWithValue("@DKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Device Edited");
                    Con.Close();
                    DisplayStore();
                    clear();
                }


                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {

            if (Key == 0)
            {
                MessageBox.Show("Select Lab Test");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from storetbl where DEVNum=@DKey", Con);
                    cmd.Parameters.AddWithValue("@DKey", Key);
                    cmd.Parameters.AddWithValue("@DQ", DEVQUA.SelectedItem);
                    cmd.Parameters.AddWithValue("@DC", DEVCOST.Text);
                    cmd.Parameters.AddWithValue("@DN", DEVNAME.Text);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Deleted");
                    Con.Close();
                    DisplayStore();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DEVICESDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DEVNAME.Text = DEVICESDGV.SelectedRows[0].Cells[1].Value.ToString();
            DEVCOST.Text = DEVICESDGV.SelectedRows[0].Cells[2].Value.ToString();
            DEVQUA.SelectedItem = DEVICESDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (DEVNAME.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DEVICESDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
