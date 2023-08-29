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

namespace databaseWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textRoll_TextChanged(object sender, EventArgs e)
        {

        }

        private void btExecute_Click(object sender, EventArgs e)
        {
            //making connection
            //connection string
            SqlConnection conn = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123");
            conn.Open();

            //MessageBox.Show("Connection is Ready");

            string str = txtQuery.Text;// "Create table Pooja_std(RollNo varchar(20), Name varchar(50), Father varchar(50), State varchar(50),
                                       // gender varchar(1))";

            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Query Executed..");
            conn.Close();

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //if rollno already exits, dont add repeted roll
            if (!checkexistance())
            {
                if (txtRoll.Text == "" || txtFather.Text == "" || txtName.Text == "")
                {
                    MessageBox.Show("Enter all entries");
                }
                else
                {
                    //inserting values into table
                    SqlConnection conn = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123");
                    conn.Open();

                    string gender = "M";
                    if (rdFemale.Checked == true)
                    {
                        gender = "F";
                    }

                    String str = "insert into Pooja_std values('" + txtRoll.Text + "','" + txtName.Text + "','" + txtFather.Text + "','" + cbState.SelectedItem.ToString() + "','" + gender + "')";

                    SqlCommand cmd = new SqlCommand(str, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Saved..");
                    //show data when saved a new data
                    fetchalldata();

                    conn.Close();

                    /*txtName.Text = "";
                    txtFather.Text = "";
                    txtRoll.Text = "";*/
                }


            }
            else {

                MessageBox.Show("Student with same RollNo already exists");

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btFetch_Click(object sender, EventArgs e)
        {
            //fetching data to show on the screen
            SqlConnection conn = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123");
            conn.Open();

            string str = txtQuery.Text;

            SqlDataAdapter adapter = new SqlDataAdapter(str,conn);
            DataTable dt = new DataTable(); 
            adapter.Fill(dt);

            conn.Close();
            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private bool checkexistance() {

            SqlConnection conn = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123");
            conn.Open();

            string str = "Select * from Pooja_std where RollNo='" +txtRoll.Text+ "' ";

            SqlDataAdapter adapter = new SqlDataAdapter(str, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            conn.Close();

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else {
                return false;
            }

        }
        private void fetchalldata() {


            //showing item in listview
            SqlConnection conn = new SqlConnection("Data Source=10.0.0.13;Database=mcadotnet;uid=student;pwd=mca@123");
            conn.Open();

            string str = "Select rollno,name,father,state,gender from Pooja_std order by name";

            SqlDataAdapter adapter = new SqlDataAdapter(str, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            conn.Close();
            //fetch code upto here

            //clearing the listview before
            listView1.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dt.Rows[i].ItemArray[0].ToString()); //roll
                    li.SubItems.Add(dt.Rows[i].ItemArray[1].ToString()); // name
                    li.SubItems.Add(dt.Rows[i].ItemArray[2].ToString()); //father
                    li.SubItems.Add(dt.Rows[i].ItemArray[4].ToString()); //gender
                    li.SubItems.Add(dt.Rows[i].ItemArray[3].ToString()); //state

                    listView1.Items.Add(li);
                }
            }


        }
        private void btRefresh_Click(object sender, EventArgs e)
        {
            fetchalldata();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbState.SelectedIndex = 0;
            fetchalldata();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //on double click show listview items on their feilds
            txtRoll.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtFather.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtName.Text = listView1.SelectedItems[0].SubItems[1].Text;
            if (listView1.SelectedItems[0].SubItems[3].Text == "M") {
                rdMale.Checked = true;
            }else {
                rdFemale.Checked = true; 
            }
            cbState.SelectedItem = listView1.SelectedItems[0].SubItems[4].Text;

        }
    }
}
