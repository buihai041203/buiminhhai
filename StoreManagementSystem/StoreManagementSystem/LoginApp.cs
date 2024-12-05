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
    public partial class LoginApp : Form
    {
        public LoginApp()
        {
            InitializeComponent();
            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void LoginApp_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Băm mật khẩu nhập vào
            string hashedPassword = PasswordHasher.HashPassword(password);

            // Kiểm tra đăng nhập
            if (CheckEmployeeLogin(username, hashedPassword))
            {
                // Chuyển hướng tới form tương ứng với vai trò Employee
                string role = GetEmployeeRole(username);
                NavigateToEmployeeForm(role);
            }
            else if (CheckCustomerAccountLogin(username, hashedPassword))
            {
                // Chuyển hướng tới form Trang chủ cho Customer
                CustomerHomeForm customerHomeForm = new CustomerHomeForm();
                customerHomeForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại! Kiểm tra lại tên đăng nhập hoặc mật khẩu.");
            }
        }

        // Kiểm tra đăng nhập với bảng Employee
        private bool CheckEmployeeLogin(string username, string hashedPassword)
        {
            bool isValid = false;
            string query = "SELECT COUNT(*) FROM Employee WHERE Username = @username AND Password = @password";

            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);

                try
                {
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        isValid = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
            return isValid;
        }

        // Kiểm tra đăng nhập với bảng CustomerAccount
        private bool CheckCustomerAccountLogin(string username, string hashedPassword)
        {
            bool isValid = false;
            string query = "SELECT COUNT(*) FROM CustomerAccount WHERE Username = @username AND Password = @password";

            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);

                try
                {
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        isValid = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
            return isValid;
        }

        // Lấy vai trò của Employee sau khi đăng nhập
        private string GetEmployeeRole(string username)
        {
            string role = "";
            string query = "SELECT RoleID FROM Employee WHERE Username = @username";

            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                try
                {
                    conn.Open();
                    role = cmd.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }

            return role;
        }

        // Chuyển hướng đến form Employee tương ứng
        private void NavigateToEmployeeForm(string role)
        {
            Form employeeForm = null;

            switch (role)
            {
                case "1": // Admin
                    employeeForm = new AdminForm();
                    break;
                case "2": // Sale
                    employeeForm = new SaleForm();
                    break;
                case "3": // Warehouse
                    employeeForm = new WarehouseForm();
                    break;
                default:
                    MessageBox.Show("Vai trò không hợp lệ.");
                    return;
            }

            if (employeeForm != null)
            {
                employeeForm.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    
}
