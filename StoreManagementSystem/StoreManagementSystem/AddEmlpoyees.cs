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
    public partial class btnClose : Form
    {
        public btnClose()
        {
            InitializeComponent();
            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các textbox và combobox
            string employeeCode = txtEmployeeCode.Text.Trim();
            string employeeName = txtEmployeeName.Text.Trim();
            string position = cmbPosition.SelectedItem?.ToString();
            int roleID = cmbRole.SelectedIndex + 1; // Role ID tương ứng với vị trí trong danh sách
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(employeeCode) || string.IsNullOrEmpty(employeeName) ||
                string.IsNullOrEmpty(position) || roleID <= 0 ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Băm mật khẩu
            string hashedPassword = PasswordHasher.HashPassword(password);

            // Chuỗi truy vấn SQL
            string query = @"
        INSERT INTO Employee (Code, Name, Position, RoleID, Username, Password)
        VALUES (@Code, @Name, @Position, @RoleID, @Username, @Password)";

            // Kết nối đến cơ sở dữ liệu và thực hiện truy vấn
            using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số vào truy vấn
                        command.Parameters.AddWithValue("@Code", employeeCode);
                        command.Parameters.AddWithValue("@Name", employeeName);
                        command.Parameters.AddWithValue("@Position", position);
                        command.Parameters.AddWithValue("@RoleID", roleID);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", hashedPassword);

                        // Thực thi truy vấn
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Xóa dữ liệu sau khi thêm để tiếp tục nhập
                            txtEmployeeCode.Clear();
                            txtEmployeeName.Clear();
                            cmbPosition.SelectedIndex = -1;
                            cmbRole.SelectedIndex = -1;
                            txtUsername.Clear();
                            txtPassword.Clear();

                            txtEmployeeCode.Focus(); // Đưa con trỏ vào mã nhân viên
                        }
                        else
                        {
                            MessageBox.Show("Failed to add employee. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
