namespace FuelStation.Win
{
    partial class TransactionForm
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
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.grdTransactionLine = new System.Windows.Forms.DataGridView();
            this.grdItem = new System.Windows.Forms.DataGridView();
            this.btnAddItem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransactionLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItem)).BeginInit();
            this.SuspendLayout();
            // 
            // nudQuantity
            // 
            this.nudQuantity.DecimalPlaces = 2;
            this.nudQuantity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudQuantity.Location = new System.Drawing.Point(522, 91);
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(120, 27);
            this.nudQuantity.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(522, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Quantity";
            // 
            // grdTransactionLine
            // 
            this.grdTransactionLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTransactionLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTransactionLine.Location = new System.Drawing.Point(12, 186);
            this.grdTransactionLine.Name = "grdTransactionLine";
            this.grdTransactionLine.RowTemplate.Height = 29;
            this.grdTransactionLine.Size = new System.Drawing.Size(901, 125);
            this.grdTransactionLine.TabIndex = 6;
            // 
            // grdItem
            // 
            this.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdItem.Location = new System.Drawing.Point(12, 69);
            this.grdItem.Name = "grdItem";
            this.grdItem.RowTemplate.Height = 29;
            this.grdItem.Size = new System.Drawing.Size(504, 111);
            this.grdItem.TabIndex = 8;
            this.grdItem.SelectionChanged += new System.EventHandler(this.grdItem_SelectionChanged);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(661, 91);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(83, 26);
            this.btnAddItem.TabIndex = 9;
            this.btnAddItem.Text = "Add..";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // TransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 473);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.grdItem);
            this.Controls.Add(this.grdTransactionLine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudQuantity);
            this.Name = "TransactionForm";
            this.Text = "TransactionForm";
            this.Load += new System.EventHandler(this.TransactionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransactionLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private NumericUpDown nudQuantity;
        private Label label2;
        private DataGridView grdTransactionLine;
        private DataGridView grdItem;
        private Button btnAddItem;
    }
}