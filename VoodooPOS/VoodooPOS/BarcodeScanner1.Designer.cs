namespace VoodooPOS
{
    partial class BarcodeScanner1
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
            this.btnScan = new System.Windows.Forms.Button();
            this.pnlVideo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(206, 403);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // pnlVideo
            // 
            this.pnlVideo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlVideo.Location = new System.Drawing.Point(30, 25);
            this.pnlVideo.Name = "pnlVideo";
            this.pnlVideo.Size = new System.Drawing.Size(458, 351);
            this.pnlVideo.TabIndex = 1;
            // 
            // BarcodeScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 468);
            this.Controls.Add(this.pnlVideo);
            this.Controls.Add(this.btnScan);
            this.Name = "BarcodeScanner";
            this.Text = "Barcode Scanner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Panel pnlVideo;
    }
}

