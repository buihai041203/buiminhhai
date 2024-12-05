using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagementSystem
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Đóng form hiện tại (AdminForm)
                this.Close();

                // Mở lại form đăng nhập (LoginForm)
                LoginApp loginForm = new LoginApp();
                loginForm.Show();
            }
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            // Mở form quản lý sản phẩm
            ManageProducts manageProductsForm = new ManageProducts();
            manageProductsForm.Show();

            // Ẩn form AdminForm hiện tại (nếu cần)
            this.Hide();
        }

        private void btnManageEmployees_Click(object sender, EventArgs e)
        {
            // Mở form quản lý nhân viên
            ManageEmployees manageProductsForm = new ManageEmployees();
            manageProductsForm.Show();

            // Ẩn form AdminForm hiện tại (nếu cần)
            this.Hide();
        }

        private void btnManageCustomers_Click(object sender, EventArgs e)
        {
            // Mở form quản lý người dùng
            ManagaCustomers manageProductsForm = new ManagaCustomers();
            manageProductsForm.Show();

            // Ẩn form AdminForm hiện tại (nếu cần)
            this.Hide();
        }

        private void btnTransactionHistory_Click(object sender, EventArgs e)
        {
            // Mở form lịch sử giao dịch
            TransactionHistory manageProductsForm = new TransactionHistory();
            manageProductsForm.Show();

            // Ẩn form AdminForm hiện tại (nếu cần)
            this.Hide();
        }
    }
}
