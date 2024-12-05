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
    public partial class TransactionHistory : Form
    {
        public TransactionHistory()
        {
            InitializeComponent();
            // Maximize the form to fill the screen
            this.WindowState = FormWindowState.Maximized;

            // Center the form on the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Disable resizing (Fixed size)
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Optional: Disable the maximize button as well if you don't want the user to maximize it
            this.MaximizeBox = false;
        }

        private void TransactionHistory_Load(object sender, EventArgs e)
        {
            LoadTransactionHistory(); // Gọi hàm tải dữ liệu khi form được mở
            dataGridViewTransactionHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadTransactionHistory()
        {
            string query = "SELECT ph.PurchaseID, c.CustomerName, p.Name AS ProductName, ph.Quantity, ph.TotalPrice, ph.PurchaseDate " +
                           "FROM PurchaseHistory ph " +
                           "JOIN Customer c ON ph.CustomerID = c.CustomerID " +
                           "JOIN Product p ON ph.ProductCode = p.Code"; // Truy vấn kết hợp 3 bảng: PurchaseHistory, Customer, Product

            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào DataGridView
                    dataGridViewTransactionHistory.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading transaction data: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị người dùng nhập vào trong textBox1
            string searchText = textBox1.Text;

            // Gọi hàm tìm kiếm và lọc dữ liệu
            SearchTransactionHistory(searchText);
        }
        private void SearchTransactionHistory(string searchText)
        {
            // Nếu textbox tìm kiếm rỗng, hiển thị tất cả dữ liệu
            if (string.IsNullOrEmpty(searchText))
            {
                LoadTransactionHistory(); // Gọi lại hàm load dữ liệu ban đầu
            }
            else
            {
                // Viết câu lệnh SQL tìm kiếm theo tên khách hàng hoặc tên sản phẩm
                string query = "SELECT ph.PurchaseID, c.CustomerName, p.Name AS ProductName, ph.Quantity, ph.TotalPrice, ph.PurchaseDate " +
                               "FROM PurchaseHistory ph " +
                               "JOIN Customer c ON ph.CustomerID = c.CustomerID " +
                               "JOIN Product p ON ph.ProductCode = p.Code " +
                               "WHERE c.CustomerName LIKE @searchText OR p.Name LIKE @searchText";

                using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%"); // Thêm tham số tìm kiếm vào câu lệnh SQL

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Gán kết quả tìm kiếm vào DataGridView
                        dataGridViewTransactionHistory.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while searching transaction data: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn Form hiện tại
            AdminForm formMain = new AdminForm(); // Tạo đối tượng FormMain
            formMain.Show(); // Hiển thị FormMain
        }
    }
}
