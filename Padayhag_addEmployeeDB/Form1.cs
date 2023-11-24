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

namespace $safeprojectname$
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-V2RGLK8\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }
        public String varid;
        private void Form1_Load(object sender, EventArgs e)
        {
            cmd.Connection = conn;
            loaddata();
        }
        private void loaddata()
        {
            listView1.Items.Clear();
            try
            {
                String q = "Select * from tblEmployee";
                cmd.CommandText = q;
                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListViewItem item = new ListViewItem(dr["id"].ToString());
                        item.SubItems.Add(dr["firstName"].ToString());
                        item.SubItems.Add(dr["lastName"].ToString());
                        item.SubItems.Add(dr["position"].ToString());
                        listView1.Items.Add(item);
                    }
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());

            }
        }

        private void save()
        {

            conn.Open();
            cmd.CommandText = "insert into tblEmployee values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
            cmd.ExecuteNonQuery();

            conn.Close();
            MessageBox.Show("Record Added");
            loaddata();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();

        }
        private void update()
        {
            conn.Open();
            cmd.CommandText = "update tblEmployee set id =  '" + textBox1.Text + "', firstName='" + textBox2.Text + "', lastName='" + textBox3.Text + "', position='" + textBox4.Text + "' where id = " + varid + "";
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Record Updated");
            loaddata();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();

        }

        private void delete()
        {
            conn.Open();
            cmd.CommandText = "delete from tblEmployee where id = "+ varid + "";
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Record Deleted");
            loaddata();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                e.Cancel = true;
                textBox1.Focus();
                errorProvider1.SetError(textBox1, "Please input your ID");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox1, null);
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                e.Cancel = true;
                textBox2.Focus();
                errorProvider1.SetError(textBox2, "Please input your Firstname");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox2, null);
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                e.Cancel = true;
                textBox3.Focus();
                errorProvider1.SetError(textBox3, "Please input your Lastname");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox3, null);
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(textBox4.Text))
            {
                e.Cancel = true;
                textBox4.Focus();
                errorProvider1.SetError(textBox4, "Please input your Position");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox4,  null);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            try
            {

                SqlDataReader dr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from tblEmployee WHERE id like'%" + textBox5.Text + "%' or firstName like '%" + textBox5.Text + "%' or lastName like '%" + textBox5.Text + "%' or position like '%" + textBox5.Text + "%' or position like '%" + textBox5.Text + "'";         
                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListViewItem item = new ListViewItem(dr["id"].ToString());
                        item.SubItems.Add(dr["firstName"].ToString());
                        item.SubItems.Add(dr["lastName"].ToString());
                        item.SubItems.Add(dr["position"].ToString());
                        listView1.Items.Add(item);
                    }

                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                varid = listView1.SelectedItems[0].Text;
                textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
                textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;

            }
        }

       
        
    
    }
}
