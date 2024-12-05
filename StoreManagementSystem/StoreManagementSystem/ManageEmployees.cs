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
    public partial class ManageEmployees : Form
    {
        public ManageEmployees()
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

        private void ManageEmployees_Load(object sender, EventArgs e)
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

                    // Câu lệnh SQL để lấy dữ liệu từ bảng 
                    string query = "SELECT * FROM Employee;";

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
            btnClose form = new btnClose();

            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string employeeCode = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                UpdateEployees updateForm = new UpdateEployees(employeeCode);
                updateForm.ShowDialog();
                LoadProductData(); // Reload data after closing the Update Form
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchEmployee(txtSearch.Text);
        }
        private void SearchEmployee(string searchText)
        {
            // Nếu textbox tìm kiếm rỗng, hiển thị tất cả dữ liệu
            if (string.IsNullOrEmpty(searchText))
            {
                LoadProductData(); // Gọi lại hàm load dữ liệu ban đầu
            }
            else
            {
                // Viết câu lệnh SQL tìm kiếm nhân viên theo mã hoặc tên
                string query = "SELECT * FROM Employee WHERE Code LIKE @searchText OR Name LIKE @searchText";

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
                        dataGridView1.DataSource = dt; // Đảm bảo đặt tên đúng cho DataGridView
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error during search: " + ex.Message);
                    }
                }
            }
        }

    }
    
}
