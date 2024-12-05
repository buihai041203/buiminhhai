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
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            string address = txtAddress.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            int active = 1; // Mặc định trạng thái hoạt động là 1 (hoạt động)

            // Kiểm tra các trường nhập liệu có hợp lệ không
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
                {
                    conn.Open(); // Mở kết nối đến cơ sở dữ liệu

                    // Bắt đầu transaction
                    transaction = conn.BeginTransaction();

                    // Câu lệnh SQL để thêm khách hàng vào bảng Customer
                    string queryCustomer = @"
                        INSERT INTO Customer (CustomerName, PhoneNumber, Email, Address, Active) 
                        VALUES (@CustomerName, @PhoneNumber, @Email, @Address, @Active);
                        SELECT SCOPE_IDENTITY();"; // Lấy ID của khách hàng mới thêm vào

                    // Thực thi câu lệnh SQL để thêm khách hàng
                    SqlCommand cmdCustomer = new SqlCommand(queryCustomer, conn, transaction);
                    cmdCustomer.Parameters.AddWithValue("@CustomerName", customerName);
                    cmdCustomer.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmdCustomer.Parameters.AddWithValue("@Email", email);
                    cmdCustomer.Parameters.AddWithValue("@Address", address);
                    cmdCustomer.Parameters.AddWithValue("@Active", active);

                    int customerId = Convert.ToInt32(cmdCustomer.ExecuteScalar()); // Lấy CustomerID của khách hàng vừa thêm

                    // Câu lệnh SQL để thêm tài khoản vào bảng CustomerAccount
                    string queryAccount = @"
                        INSERT INTO CustomerAccount (CustomerID, Username, Password) 
                        VALUES (@CustomerID, @Username, @Password);";

                    // Thực thi câu lệnh SQL để thêm tài khoản
                    SqlCommand cmdAccount = new SqlCommand(queryAccount, conn, transaction);
                    cmdAccount.Parameters.AddWithValue("@CustomerID", customerId);
                    cmdAccount.Parameters.AddWithValue("@Username", username);
                    cmdAccount.Parameters.AddWithValue("@Password", PasswordHasher.HashPassword(password)); // Mã hóa mật khẩu

                    cmdAccount.ExecuteNonQuery(); // Thực thi câu lệnh thêm tài khoản

                    // Commit transaction nếu mọi thứ đều thành công
                    transaction.Commit();

                    // Thông báo thành công khi khách hàng được thêm
                    MessageBox.Show("Customer and account added successfully!");

                    // Làm mới dữ liệu trong form ManageCustomer (Owner)
                    if (Owner is ManagaCustomers manageCustomerForm)
                    {
                        manageCustomerForm.LoadCustomerData(); // Gọi phương thức LoadCustomerData của form ManageCustomer
                    }

                    // Xóa các trường nhập liệu sau khi thêm thành công
                    txtCustomerName.Clear();
                    txtPhoneNumber.Clear();
                    txtEmail.Clear();
                    txtAddress.Clear();
                    txtUsername.Clear();
                    txtPassword.Clear();
                }
            }
            catch (Exception ex)
            {
                // Rollback transaction nếu có lỗi xảy ra
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                // Hiển thị lỗi nếu có vấn đề trong quá trình thêm khách hàng
                MessageBox.Show("An error occurred while adding the customer: " + ex.Message);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}
