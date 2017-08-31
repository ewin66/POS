namespace VoodooPOS
{
    partial class GiftCertificateFormRedeem
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
            this.btnApply = new VoodooPOS.GelButton();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new VoodooPOS.GelButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtUpc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAmountToApply = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnActivate = new VoodooPOS.GelButton();
            this.btnInfo = new VoodooPOS.GelButton();
            this.ddGiftCertificates = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(204, 213);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 26);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(227, 108);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(100, 22);
            this.txtAmount.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Amount:   $";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(303, 213);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(227, 80);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(100, 22);
            this.txtName.TabIndex = 0;
            // 
            // txtUpc
            // 
            this.txtUpc.Location = new System.Drawing.Point(227, 136);
            this.txtUpc.Name = "txtUpc";
            this.txtUpc.ReadOnly = true;
            this.txtUpc.Size = new System.Drawing.Size(100, 22);
            this.txtUpc.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "UPC:";
            // 
            // txtAmountToApply
            // 
            this.txtAmountToApply.Location = new System.Drawing.Point(227, 166);
            this.txtAmountToApply.Name = "txtAmountToApply";
            this.txtAmountToApply.Size = new System.Drawing.Size(100, 22);
            this.txtAmountToApply.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Amount To Apply:   $";
            // 
            // btnActivate
            // 
            this.btnActivate.Location = new System.Drawing.Point(109, 216);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(75, 23);
            this.btnActivate.TabIndex = 9;
            this.btnActivate.Text = "Activate";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(367, 80);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(51, 23);
            this.btnInfo.TabIndex = 10;
            this.btnInfo.Text = "Info";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // ddGiftCertificates
            // 
            this.ddGiftCertificates.DisplayMember = "DisplayName";
            this.ddGiftCertificates.FormattingEnabled = true;
            this.ddGiftCertificates.Location = new System.Drawing.Point(98, 41);
            this.ddGiftCertificates.Name = "ddGiftCertificates";
            this.ddGiftCertificates.Size = new System.Drawing.Size(290, 24);
            this.ddGiftCertificates.TabIndex = 11;
            this.ddGiftCertificates.ValueMember = "id";
            this.ddGiftCertificates.SelectedIndexChanged += new System.EventHandler(this.ddGiftCertificates_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(197, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            this.lblStatus.TabIndex = 12;
            // 
            // GiftCertificateFormRedeem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 262);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.ddGiftCertificates);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.txtAmountToApply);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUpc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnApply);
            this.Name = "GiftCertificateFormRedeem";
            this.Text = "Gift Certificate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtUpc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAmountToApply;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.ComboBox ddGiftCertificates;
        private System.Windows.Forms.Label lblStatus;
    }
}