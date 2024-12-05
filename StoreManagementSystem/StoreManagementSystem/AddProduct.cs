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

namespace StoreManagementSystem
{
    public partial class AddProduct : Form
    {
        
        public AddProduct()
        {
            InitializeComponent();
            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các TextBox
            string productCode = txtCode.Text.Trim();
            string productName = txtName.Text.Trim();
            int quantity = 0;
            decimal price = 0;

            // Kiểm tra và chuyển đổi dữ liệu nhập vào
            if (int.TryParse(txtQuantity.Text.Trim(), out quantity) && decimal.TryParse(txtPrice.Text.Trim(), out price))
            {
                // Câu lệnh SQL để thêm sản phẩm vào cơ sở dữ liệu
                string insertQuery = "INSERT INTO Product (Code, Name, Quantity, Price) VALUES (@Code, @Name, @Quantity, @Price)";

                try
                {
                    // Tạo kết nối đến cơ sở dữ liệu
                    using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
                    {
                        // Mở kết nối
                        conn.Open();

                        // Tạo command để thực thi câu lệnh SQL
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            // Thêm tham số vào câu lệnh SQL
                            cmd.Parameters.AddWithValue("@Code", productCode);
                            cmd.Parameters.AddWithValue("@Name", productName);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@Price", price);

                            // Thực thi câu lệnh SQL
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Kiểm tra nếu có bản ghi nào bị ảnh hưởng (thêm thành công)
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Product added successfully!");
                            }
                            else
                            {
                                MessageBox.Show("Failed to add product.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                // Hiển thị lỗi nếu dữ liệu không hợp lệ
                MessageBox.Show("Please enter valid quantity and price.");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
