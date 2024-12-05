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
    public partial class UpdateWarehouse : Form
    {
        private string productCode;
        public UpdateWarehouse(string code, string name, int quantity, decimal price)
        {
            InitializeComponent();
            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            productCode = code;
            txtProductCode.Text = code; // Mã sản phẩm - chỉ đọc
            txtProductCode.ReadOnly = true; // Chỉ đọc, không cho phép chỉnh sửa

            txtProductName.Text = name; // Tên sản phẩm - chỉ đọc
            txtProductName.ReadOnly = true; // Chỉ đọc, không cho phép chỉnh sửa

            txtPrice.Text = price.ToString("0.00"); // Giá sản phẩm - chỉ đọc
            txtPrice.ReadOnly = true; // Chỉ đọc, không cho phép chỉnh sửa

            txtQuantity.Text = quantity.ToString(); // Số lượng sản phẩm - có thể chỉnh sửa
            txtQuantity.ReadOnly = false; // Cho phép chỉnh sửa
        }

        private void UpdateWarehouse_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các TextBox
            int quantity;
            if (!int.TryParse(txtQuantity.Text, out quantity))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ.");
                return;
            }

            // Cập nhật thông tin vào cơ sở dữ liệu
            string query = @"
                UPDATE Product
                SET Quantity = @Quantity
                WHERE Code = @Code";

            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Code", productCode);

                    // Thực thi câu lệnh cập nhật
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật số lượng sản phẩm thành công!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật số lượng sản phẩm: " + ex.Message);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
