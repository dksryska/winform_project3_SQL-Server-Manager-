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
using Manager_Project1;

namespace Portfolio11
{
    public partial class Login : Form
    {
        database db = new database();

        public Login()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginUser = textBox_login.Text;
            var passUser = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id, name, pw from register where name='{loginUser}'and pw='{passUser}'";

            SqlCommand command = new SqlCommand(querystring, db.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("로그인이 되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Product_Main main = new Product_Main();
                this.Hide();
                main.Show();
            }
            else
            {
                MessageBox.Show("로그인 실패", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Join join = new Join();

            this.Hide();
            join.Show();
        }
    }
}
