namespace VoodooPOS
{
    partial class checkout_cash
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
            this.btnCheckout = new VoodooPOS.GelButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.btnCancel = new VoodooPOS.GelButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(177, 122);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 17);
            this.lblTotal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cash Tendered:  $";
            // 
            // txtCashTendered
            // 
            this.txtCashTendered.Location = new System.Drawing.Point(254, 186);
            this.txtCashTendered.Name = "txtCashTendered";
            this.txtCashTendered.Size = new System.Drawing.Size(100, 22);
            this.txtCashTendered.TabIndex = 3;
            // 
            // btnCheckout
            // 
            this.btnCheckout.Location = new System.Drawing.Point(153, 330);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(89, 23);
            this.btnCheckout.TabIndex = 4;
            this.btnCheckout.Text = "Checkout";
            this.btnCheckout.UseVisualStyleBackColor = true;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 246);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Change:  $";
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.Location = new System.Drawing.Point(204, 246);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(17, 17);
            this.lblChange.TabIndex = 6;
            this.lblChange.Text = "0";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(264, 330);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkout_cash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 385);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.txtCashTendered);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label1);
            this.Name = "checkout_cash";
            this.Text = "Cash Checkout";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCashTendered;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Button btnCancel;
    }
}