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
    public partial class UpdateEployees : Form
    {
        // Constructor nhận thông tin Employee Code để hiển thị chi tiết
        private string employeeCode;

        public UpdateEployees(string employeeCode)
        {
            InitializeComponent();
            this.employeeCode = employeeCode;
            LoadEmployeeData();
            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void LoadEmployeeData()
        {
            using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Code, Name, Position, RoleID, Username FROM Employee WHERE Code = @Code";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Code", employeeCode);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtEmployeeCode.Text = reader["Code"].ToString();
                        txtEmployeeName.Text = reader["Name"].ToString();
                        cmbPosition.SelectedItem = reader["Position"].ToString();
                        cmbRole.SelectedValue = int.Parse(reader["RoleID"].ToString());
                        txtUsername.Text = reader["Username"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading employee data: " + ex.Message);
                }
            }
        }

        private void UpdateEployees_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
        "Are you sure you want to delete this employee?",
        "Confirm Delete",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM Employee WHERE Code = @Code";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Code", txtEmployeeCode.Text);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee deleted successfully!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete employee.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting employee: " + ex.Message);
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = txtEmployeeName.Text.Trim();
            string position = cmbPosition.SelectedItem?.ToString();
            int roleID = (int)cmbRole.SelectedValue;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Kiểm tra mật khẩu có được nhập hay không
            string hashedPassword = string.IsNullOrEmpty(password)
                ? null
                : PasswordHasher.HashPassword(password);

            using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    connection.Open();

                    // Cập nhật thông tin
                    string query = hashedPassword == null
                        ? "UPDATE Employee SET Name = @Name, Position = @Position, RoleID = @RoleID, Username = @Username WHERE Code = @Code"
                        : "UPDATE Employee SET Name = @Name, Position = @Position, RoleID = @RoleID, Username = @Username, Password = @Password WHERE Code = @Code";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Position", position);
                    command.Parameters.AddWithValue("@RoleID", roleID);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Code", txtEmployeeCode.Text);

                    if (hashedPassword != null)
                    {
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                    }

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Employee updated successfully!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update employee.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating employee: " + ex.Message);
                }
            }
        }
    }
}
