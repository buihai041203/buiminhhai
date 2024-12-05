namespace StoreManagementSystem
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnManageProducts = new System.Windows.Forms.Button();
            this.btnManageEmployees = new System.Windows.Forms.Button();
            this.btnManageCustomers = new System.Windows.Forms.Button();
            this.btnTransactionHistory = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnManageProducts
            // 
            this.btnManageProducts.Location = new System.Drawing.Point(28, 41);
            this.btnManageProducts.Name = "btnManageProducts";
            this.btnManageProducts.Size = new System.Drawing.Size(228, 51);
            this.btnManageProducts.TabIndex = 0;
            this.btnManageProducts.Text = "Manage Products";
            this.btnManageProducts.UseVisualStyleBackColor = true;
            this.btnManageProducts.Click += new System.EventHandler(this.btnManageProducts_Click);
            // 
            // btnManageEmployees
            // 
            this.btnManageEmployees.Location = new System.Drawing.Point(28, 111);
            this.btnManageEmployees.Name = "btnManageEmployees";
            this.btnManageEmployees.Size = new System.Drawing.Size(228, 51);
            this.btnManageEmployees.TabIndex = 0;
            this.btnManageEmployees.Text = "Manage Employees";
            this.btnManageEmployees.UseVisualStyleBackColor = true;
            this.btnManageEmployees.Click += new System.EventHandler(this.btnManageEmployees_Click);
            // 
            // btnManageCustomers
            // 
            this.btnManageCustomers.Location = new System.Drawing.Point(28, 183);
            this.btnManageCustomers.Name = "btnManageCustomers";
            this.btnManageCustomers.Size = new System.Drawing.Size(228, 51);
            this.btnManageCustomers.TabIndex = 0;
            this.btnManageCustomers.Text = "Manage Customers";
            this.btnManageCustomers.UseVisualStyleBackColor = true;
            this.btnManageCustomers.Click += new System.EventHandler(this.btnManageCustomers_Click);
            // 
            // btnTransactionHistory
            // 
            this.btnTransactionHistory.Location = new System.Drawing.Point(28, 254);
            this.btnTransactionHistory.Name = "btnTransactionHistory";
            this.btnTransactionHistory.Size = new System.Drawing.Size(228, 51);
            this.btnTransactionHistory.TabIndex = 0;
            this.btnTransactionHistory.Text = "Transaction History";
            this.btnTransactionHistory.UseVisualStyleBackColor = true;
            this.btnTransactionHistory.Click += new System.EventHandler(this.btnTransactionHistory_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(28, 326);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(228, 51);
            this.btnLogOut.TabIndex = 0;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::StoreManagementSystem.Properties.Resources.ad;
            this.pictureBox1.Location = new System.Drawing.Point(283, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(328, 373);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 418);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btnTransactionHistory);
            this.Controls.Add(this.btnManageCustomers);
            this.Controls.Add(this.btnManageEmployees);
            this.Controls.Add(this.btnManageProducts);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnManageProducts;
        private System.Windows.Forms.Button btnManageEmployees;
        private System.Windows.Forms.Button btnManageCustomers;
        private System.Windows.Forms.Button btnTransactionHistory;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}