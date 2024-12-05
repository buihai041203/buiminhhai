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
    public partial class UpdateProduct : Form
    {
        private string productCode;
        public UpdateProduct(string code)
        {
            InitializeComponent();
            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            productCode = code;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtProductCode.Text;
                string name = txtProductName.Text;
                int quantity = int.Parse(txtQuantity.Text);
                decimal price = decimal.Parse(txtPrice.Text);

                // Cập nhật thông tin sản phẩm vào cơ sở dữ liệu
                string updateQuery = "UPDATE Product SET Name = @Name, Quantity = @Quantity, Price = @Price WHERE Code = @Code";

                using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
                {
                    SqlCommand cmd = new SqlCommand(updateQuery, connection);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Price", price);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();  // Đóng form sau khi cập nhật
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtProductCode.Text;

                // Xóa sản phẩm khỏi cơ sở dữ liệu
                string deleteQuery = "DELETE FROM Product WHERE Code = @Code";

                using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
                {
                    SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                    cmd.Parameters.AddWithValue("@Code", code);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();  // Đóng form sau khi xóa
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateProduct_Load(object sender, EventArgs e)
        {
            LoadProductDetails();
        }
        private void LoadProductDetails()
        {
            // Kết nối với cơ sở dữ liệu và tải thông tin sản phẩm dựa trên mã sản phẩm
            string query = "SELECT * FROM Product WHERE Code = @ProductCode";

            using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ProductCode", productCode);

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    // Điền dữ liệu vào các textbox
                    txtProductCode.Text = dataTable.Rows[0]["Code"].ToString();
                    txtProductName.Text = dataTable.Rows[0]["Name"].ToString();
                    txtQuantity.Text = dataTable.Rows[0]["Quantity"].ToString();
                    txtPrice.Text = dataTable.Rows[0]["Price"].ToString();
                }
            }
        }

    }
}
