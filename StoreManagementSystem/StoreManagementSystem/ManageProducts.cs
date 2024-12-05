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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy mã sản phẩm từ cột Code (hoặc cột bất kỳ bạn muốn)
                string productCode = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString();

                // Mở form UpdateProductForm với mã sản phẩm được chọn
                UpdateProduct updateForm = new UpdateProduct(productCode);
                updateForm.ShowDialog();  // Mở form ở chế độ modal (người dùng phải đóng form này trước khi quay lại form chính)
            }
        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            LoadProductData();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadProductData()
        {
            // Tạo đối tượng kết nối
            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    // Mở kết nối
                    conn.Open();

                    // Câu lệnh SQL để lấy dữ liệu từ bảng Product
                    string query = "SELECT Code, Name, Quantity, Price FROM Product";

                    // Tạo đối tượng DataAdapter để thực thi câu lệnh SQL
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    // Tạo DataTable để lưu trữ dữ liệu
                    DataTable dt = new DataTable();

                    // Điền dữ liệu vào DataTable
                    adapter.Fill(dt);

                    // Gán DataTable vào DataGridView để hiển thị
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchProduct(txtSearch.Text);
        }
        private void SearchProduct(string searchText)
        {
            // Nếu textbox tìm kiếm rỗng, hiển thị tất cả dữ liệu
            if (string.IsNullOrEmpty(searchText))
            {
                LoadProductData(); // Gọi lại hàm load dữ liệu ban đầu
            }
            else
            {
                // Viết câu lệnh SQL tìm kiếm sản phẩm theo mã hoặc tên
                string query = "SELECT * FROM Product WHERE Code LIKE @searchText OR Name LIKE @searchText";

                // Thực thi truy vấn và cập nhật DataGridView
                using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Gán kết quả tìm kiếm vào DataGridView
                        dataGridView1.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
                    }
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn Form hiện tại
            AdminForm formMain = new AdminForm(); // Tạo đối tượng FormMain
            formMain.Show(); // Hiển thị FormMain
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProductData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct form = new AddProduct();

            form.ShowDialog();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
