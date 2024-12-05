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
    public partial class ManagaCustomers : Form
    {
        public ManagaCustomers()
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

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn Form hiện tại
            AdminForm formMain = new AdminForm(); // Tạo đối tượng FormMain
            formMain.Show(); // Hiển thị FormMain
        }

        private void ManagaCustomers_Load(object sender, EventArgs e)
        {
            LoadCustomerData(); // Tải dữ liệu khi form được mở
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        public void LoadCustomerData()
        {
            // Tải lại dữ liệu khách hàng từ cơ sở dữ liệu và cập nhật DataGridView
            string query = @"
        SELECT c.CustomerID, c.CustomerName, c.PhoneNumber, c.Address, c.Email, ca.Username, ca.Password
        FROM Customer c
        JOIN CustomerAccount ca ON c.CustomerID = ca.CustomerID
        WHERE c.Active = 1"; // Lọc chỉ những tài khoản có trạng thái Active = 1

            // Kết nối đến cơ sở dữ liệu và tải dữ liệu vào DataGridView
            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustomer form = new AddCustomer();
            form.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomerData(); // Tải dữ liệu khi form được mở
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng click vào một hàng (không phải header)
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin từ các ô của hàng được chọn
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Tạo đối tượng form UpdateCustomer và truyền thông tin từ dòng vào form đó
                UpdateCustomer updateForm = new UpdateCustomer();

                updateForm.CustomerID = row.Cells["CustomerID"].Value.ToString();
                updateForm.CustomerName = row.Cells["CustomerName"].Value.ToString();
                updateForm.PhoneNumber = row.Cells["PhoneNumber"].Value.ToString();
                updateForm.Email = row.Cells["Email"].Value.ToString();
                updateForm.Address = row.Cells["Address"].Value.ToString();
                updateForm.Username = row.Cells["Username"].Value.ToString();

                // Hiển thị form UpdateCustomer
                updateForm.ShowDialog(); // Mở form UpdateCustomer dưới dạng modal
            }
        }
    }
}
