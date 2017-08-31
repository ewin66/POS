namespace VoodooPOS
{
    partial class CashRegisterDrawer
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
            this.dgvDrawer = new System.Windows.Forms.DataGridView();
            this.txtCurrentAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChange = new VoodooPOS.GelButton();
            this.btnSave = new VoodooPOS.GelButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRemoveMoney = new System.Windows.Forms.TextBox();
            this.btnUpdate = new VoodooPOS.GelButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawer)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDrawer
            // 
            this.dgvDrawer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDrawer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDrawer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrawer.Location = new System.Drawing.Point(12, 32);
            this.dgvDrawer.Name = "dgvDrawer";
            this.dgvDrawer.RowTemplate.Height = 24;
            this.dgvDrawer.Size = new System.Drawing.Size(651, 232);
            this.dgvDrawer.TabIndex = 0;
            // 
            // txtCurrentAmount
            // 
            this.txtCurrentAmount.BackColor = System.Drawing.Color.LightGray;
            this.txtCurrentAmount.Location = new System.Drawing.Point(150, 273);
            this.txtCurrentAmount.Name = "txtCurrentAmount";
            this.txtCurrentAmount.ReadOnly = true;
            this.txtCurrentAmount.Size = new System.Drawing.Size(100, 22);
            this.txtCurrentAmount.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current Amount:";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(69, 298);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 27);
            this.btnChange.TabIndex = 3;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(159, 298);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 27);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Remove Money:";
            // 
            // txtRemoveMoney
            // 
            this.txtRemoveMoney.Location = new System.Drawing.Point(486, 275);
            this.txtRemoveMoney.Name = "txtRemoveMoney";
            this.txtRemoveMoney.Size = new System.Drawing.Size(100, 22);
            this.txtRemoveMoney.TabIndex = 6;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(445, 303);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 25);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // CashRegisterDrawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 343);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtRemoveMoney);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCurrentAmount);
            this.Controls.Add(this.dgvDrawer);
            this.Name = "CashRegisterDrawer";
            this.Text = "Cash Register Drawer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDrawer;
        private System.Windows.Forms.TextBox txtCurrentAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRemoveMoney;
        private System.Windows.Forms.Button btnUpdate;
    }
}