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
using System.Data.SqlClient;
using Manager_Project1;

namespace Portfolio11
{
    public partial class Join : Form
    {
        database db = new database();

        public Join()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void join_btn_Click(object sender, EventArgs e)
        {
            var login = textBox_login2.Text;
            var password = textBox_password2.Text;

            string querystring = $"insert into register(name, pw) values ('{login}', '{password}')";

            SqlCommand command = new SqlCommand(querystring, db.getConnection());

            db.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("회원가입 완료", "알림");

                Login frm_login = new Login();
                this.Hide();
                frm_login.Show();
            }
            else
            {
                MessageBox.Show("회원가입 실패", "알림");
            }

            db.closeConnection();
        }
    }
}
