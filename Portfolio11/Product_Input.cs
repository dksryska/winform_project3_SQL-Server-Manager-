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
    public partial class Product_Input : Form
    {
        database db = new database();

        public Product_Input()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Product_Main main = new Product_Main();
            this.Hide();
            main.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = textBox_name.Text;
            var description = textBox_description.Text;
            var number = textBox_number.Text;
            var price = textBox_price.Text;

            string querystring = $"insert into product(name, description, number, price) values('{name}', '{description}', '{number}', '{price}')";

            SqlCommand command = new SqlCommand(querystring, db.getConnection());

            db.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("제품을 입력하셨어요.!", "알림");
            }
            else
            {
                MessageBox.Show("알수없는 오류", "알림");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Product_Main main = new Product_Main();

            this.Hide();
            main.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login log = new Login();

            this.Hide();
            log.Show();
        }
    }
}
