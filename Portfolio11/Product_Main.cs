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
    enum RowState
    {
        ModifiedNew,
        Deleted,
        Modified,
        Existed
    }

    public partial class Product_Main : Form
    {
        database db = new database();
        int selectedRow;

        public Product_Main()
        {
            InitializeComponent();
        }

        private void Product_Main_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("type_of", "name");
            dataGridView1.Columns.Add("count_of", "description");
            dataGridView1.Columns.Add("postavka", "number");
            dataGridView1.Columns.Add("price", "price");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox_name.Text = row.Cells[1].Value.ToString();
                textBox_description.Text = row.Cells[2].Value.ToString();
                textBox_number.Text = row.Cells[3].Value.ToString();
                textBox_price.Text = row.Cells[4].Value.ToString();
            }
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string querystring = $"select * from product";

            SqlCommand command = new SqlCommand(querystring, db.getConnection());

            db.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0),   // id
                record.GetString(1), // name
                record.GetString(2),  // description
                record.GetString(3), // number
                record.GetString(4),  // price
                RowState.ModifiedNew);
        }

        private void input_click(object sender, EventArgs e)
        {
            Product_Input input = new Product_Input();
            this.Hide();
            input.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }





        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void delete_click(object sender, EventArgs e)
        {
            string idText = textBox_name.Text.Trim(); 

            if (!int.TryParse(idText, out int id))
            {
                MessageBox.Show("올바른 ID를 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 삭제 확인 메시지 표시
            var confirmResult = MessageBox.Show($"ID: {id} 데이터를 정말로 삭제하시겠습니까?",
                                                "삭제 확인",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    db.openConnection();

                    
                    string checkQuery = "SELECT COUNT(*) FROM product WHERE id = @id";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, db.getConnection());
                    checkCommand.Parameters.AddWithValue("@id", id);

                    int recordCount = (int)checkCommand.ExecuteScalar();

                    if (recordCount == 0)
                    {
                        MessageBox.Show("해당 ID 데이터가 존재하지 않습니다.", "삭제 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    
                    string deleteQuery = "DELETE FROM product WHERE id = @id";

 
                    SqlCommand command = new SqlCommand(deleteQuery, db.getConnection());

                    
                    command.Parameters.AddWithValue("@id", id);

                    // 쿼리 실행
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("데이터가 성공적으로 삭제되었습니다.", "삭제 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataGrid(dataGridView1); 
                    }
                    else
                    {
                        MessageBox.Show("해당 ID 데이터가 존재하지 않습니다.", "삭제 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"오류 발생: {ex.Message}", "삭제 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.closeConnection();
                }
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            var name = textBox_name.Text.Trim();
            var description = textBox_description.Text.Trim();
            var number = textBox_number.Text.Trim();
            var price = textBox_price.Text.Trim();

            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Cells["id"].Value == null)
            {
                MessageBox.Show("수정할 데이터를 선택해주세요.", "수정 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();

            // 유효성 검사: 모든 필드가 채워져 있는지 확인
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) ||
                string.IsNullOrEmpty(number) || string.IsNullOrEmpty(price))
            {
                MessageBox.Show("모든 필드를 입력해야 합니다.", "수정 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(number, out _) || !int.TryParse(price, out _))
            {
                MessageBox.Show("숫자 필드에는 유효한 숫자를 입력해야 합니다.", "수정 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                db.openConnection();

                string query = $"UPDATE product SET name = @name, description = @description, number = @number, price = @price WHERE id = @id";
                SqlCommand command = new SqlCommand(query, db.getConnection());
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@number", number);
                command.Parameters.AddWithValue("@price", price);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("데이터가 성공적으로 수정되었습니다.", "수정 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDataGrid(dataGridView1); // 데이터 그리드뷰 갱신
                }
                else
                {
                    MessageBox.Show("수정할 데이터를 찾을 수 없습니다.", "수정 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "수정 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Product_Input input = new Product_Input();

            this.Hide();
            input.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login log = new Login();

            this.Hide();
            log.Show();
        }
    }
}
