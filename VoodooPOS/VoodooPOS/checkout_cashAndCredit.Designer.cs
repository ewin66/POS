namespace VoodooPOS
{
    partial class checkout_cashAndCredit
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCashTendered = new System.Windows.Forms.TextBox();
            this.btnSplit = new VoodooPOS.GelButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCCAmount = new System.Windows.Forms.Label();
            this.btnCancel = new VoodooPOS.GelButton();
            this.btnCheckout = new VoodooPOS.GelButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(135, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(181, 43);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 17);
            this.lblTotal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cash Tendered:  $";
            // 
            // txtCashTendered
            // 
            this.txtCashTendered.Location = new System.Drawing.Point(251, 76);
            this.txtCashTendered.Name = "txtCashTendered";
            this.txtCashTendered.Size = new System.Drawing.Size(100, 22);
            this.txtCashTendered.TabIndex = 3;
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(207, 117);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(79, 32);
            this.btnSplit.TabIndex = 4;
            this.btnSplit.Text = "Split";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Amount On CC:  $";
            // 
            // lblCCAmount
            // 
            this.lblCCAmount.AutoSize = true;
            this.lblCCAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCCAmount.Location = new System.Drawing.Point(255, 189);
            this.lblCCAmount.Name = "lblCCAmount";
            this.lblCCAmount.Size = new System.Drawing.Size(17, 17);
            this.lblCCAmount.TabIndex = 6;
            this.lblCCAmount.Text = "0";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(271, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCheckout
            // 
            this.btnCheckout.Enabled = false;
            this.btnCheckout.Location = new System.Drawing.Point(147, 238);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(75, 32);
            this.btnCheckout.TabIndex = 8;
            this.btnCheckout.Text = "Checkout";
            this.btnCheckout.UseVisualStyleBackColor = true;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // checkout_cashAndCredit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 297);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblCCAmount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.txtCashTendered);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label1);
            this.Name = "checkout_cashAndCredit";
            this.Text = "Cash And Credit Checkout";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCashTendered;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCCAmount;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCheckout;
    }
}