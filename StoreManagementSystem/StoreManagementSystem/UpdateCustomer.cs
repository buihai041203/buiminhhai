using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StoreManagementSystem
{
    public partial class UpdateCustomer : Form
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public UpdateCustomer()
        {
            InitializeComponent();
            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void UpdateCustomer_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin khách hàng vào các TextBox khi form được tải
            txtCustomerID.Text = CustomerID;
            txtCustomerName.Text = CustomerName;
            txtPhoneNumber.Text = PhoneNumber;
            txtEmail.Text = Email;
            txtAddress.Text = Address;
            txtUsername.Text = Username;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox
            string customerID = txtCustomerID.Text;
            string customerName = txtCustomerName.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            string address = txtAddress.Text;
            string username = txtUsername.Text;

            // Cập nhật thông tin khách hàng trong bảng Customer
            string updateCustomerQuery = @"
        UPDATE Customer
        SET CustomerName = @CustomerName, PhoneNumber = @PhoneNumber, Email = @Email, Address = @Address
        WHERE CustomerID = @CustomerID";

            // Cập nhật thông tin tài khoản trong bảng CustomerAccount
            string updateAccountQuery = @"
        UPDATE CustomerAccount
        SET Username = @Username
        WHERE CustomerID = @CustomerID";

            // Kết nối đến cơ sở dữ liệu và thực thi câu lệnh SQL
            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    conn.Open(); // Mở kết nối đến cơ sở dữ liệu

                    // Cập nhật thông tin khách hàng
                    using (SqlCommand cmd = new SqlCommand(updateCustomerQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", customerName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                    }

                    // Cập nhật thông tin tài khoản khách hàng
                    using (SqlCommand cmd = new SqlCommand(updateAccountQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                    }

                    // Thông báo cập nhật thành công
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    MessageBox.Show("Lỗi khi cập nhật thông tin khách hàng: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string customerID = txtCustomerID.Text; // Lấy CustomerID từ TextBox

            if (string.IsNullOrEmpty(customerID))
            {
                MessageBox.Show("Please select a customer to delete.");
                return;
            }

            // Câu lệnh SQL để cập nhật trường Active thành 0 (không hoạt động)
            string updateActiveStatusQuery = @"
        UPDATE Customer
        SET Active = 0
        WHERE CustomerID = @CustomerID";

            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    conn.Open(); // Mở kết nối tới cơ sở dữ liệu

                    using (SqlCommand cmd = new SqlCommand(updateActiveStatusQuery, conn))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        // Thực thi câu lệnh SQL để cập nhật trường Active thành 0
                        cmd.ExecuteNonQuery();
                    }

                    // Thông báo thành công
                    MessageBox.Show("Customer has been deactivated successfully!");

                    // Đóng form UpdateCustomer và trở về form ManageCustomers
                    this.Close();
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    MessageBox.Show("An error occurred while deactivating the customer: " + ex.Message);
                }
            }
        }
    }
}
