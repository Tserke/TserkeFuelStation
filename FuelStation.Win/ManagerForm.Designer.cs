namespace FuelStation.Win
{
    partial class ManagerForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nudRentingCost = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRentingCost)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCustomer,
            this.menuItemTransaction,
            this.menuItemItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemCustomer
            // 
            this.menuItemCustomer.Name = "menuItemCustomer";
            this.menuItemCustomer.Size = new System.Drawing.Size(84, 24);
            this.menuItemCustomer.Text = "Customer";
            this.menuItemCustomer.Click += new System.EventHandler(this.menuItemCustomer_Click);
            // 
            // menuItemTransaction
            // 
            this.menuItemTransaction.Name = "menuItemTransaction";
            this.menuItemTransaction.Size = new System.Drawing.Size(96, 24);
            this.menuItemTransaction.Text = "Transaction";
            this.menuItemTransaction.Click += new System.EventHandler(this.menuItemTransaction_Click);
            // 
            // menuItemItem
            // 
            this.menuItemItem.Name = "menuItemItem";
            this.menuItemItem.Size = new System.Drawing.Size(51, 24);
            this.menuItemItem.Text = "Item";
            this.menuItemItem.Click += new System.EventHandler(this.menuItemItem_Click);
            // 
            // nudRentingCost
            // 
            this.nudRentingCost.Location = new System.Drawing.Point(45, 112);
            this.nudRentingCost.Name = "nudRentingCost";
            this.nudRentingCost.Size = new System.Drawing.Size(120, 27);
            this.nudRentingCost.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Renting Cost";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(597, 404);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 34);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(703, 404);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 34);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudRentingCost);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ManagerForm";
            this.Text = "ManagerForm";
            this.Load += new System.EventHandler(this.ManagerForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRentingCost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuItemCustomer;
        private ToolStripMenuItem menuItemTransaction;
        private ToolStripMenuItem menuItemItem;
        private NumericUpDown nudRentingCost;
        private Label label1;
        private Button btnSave;
        private Button btnClose;
    }
}