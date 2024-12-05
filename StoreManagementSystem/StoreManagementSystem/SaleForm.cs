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
    public partial class SaleForm : Form
    {
        public SaleForm()
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text; // Lấy nội dung tìm kiếm từ TextBox

            if (string.IsNullOrEmpty(searchText))
            {
                LoadProductData(); // Nếu không có gì tìm kiếm, tải lại tất cả sản phẩm
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                SearchProduct(searchText); // Gọi phương thức tìm kiếm
            }
        }
        private void SearchProduct(string searchText)
        {
            string query = "SELECT * FROM Product WHERE Code LIKE @searchText OR Name LIKE @searchText"; // Tìm theo mã hoặc tên sản phẩm

            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%"); // Thêm tham số vào câu truy vấn

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt); // Đổ dữ liệu vào DataTable

                    dataGridView1.DataSource = dt; // Gán DataTable vào DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm sản phẩm: " + ex.Message);
                }
            }
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
            LoadProductData(); // Gọi phương thức LoadProductData để tải dữ liệu sản phẩm khi form được mở
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadProductData()
        {
            string query = "SELECT * FROM Product"; // Lấy tất cả dữ liệu từ bảng Product

            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt); // Lấy dữ liệu vào DataTable
                    dataGridView1.DataSource = dt; // Gán DataTable vào DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu sản phẩm: " + ex.Message);
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn SaleForm
            LoginApp loginForm = new LoginApp(); // Tạo đối tượng LoginForm
            loginForm.Show(); // Hiển thị LoginForm
        }
    }
}
