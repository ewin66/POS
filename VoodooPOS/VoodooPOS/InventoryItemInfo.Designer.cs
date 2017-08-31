namespace VoodooPOS
{
    partial class InventoryItemInfo
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOK = new VoodooPOS.GelButton();
            this.chbOnSale = new System.Windows.Forms.CheckBox();
            this.chbFeatured = new System.Windows.Forms.CheckBox();
            this.chbDisplayOnWeb = new System.Windows.Forms.CheckBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblUPC = new System.Windows.Forms.Label();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(122, 139);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(132, 310);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chbOnSale
            // 
            this.chbOnSale.AutoSize = true;
            this.chbOnSale.Location = new System.Drawing.Point(12, 157);
            this.chbOnSale.Name = "chbOnSale";
            this.chbOnSale.Size = new System.Drawing.Size(81, 21);
            this.chbOnSale.TabIndex = 28;
            this.chbOnSale.Text = "On Sale";
            this.chbOnSale.UseVisualStyleBackColor = true;
            // 
            // chbFeatured
            // 
            this.chbFeatured.AutoSize = true;
            this.chbFeatured.Location = new System.Drawing.Point(99, 157);
            this.chbFeatured.Name = "chbFeatured";
            this.chbFeatured.Size = new System.Drawing.Size(87, 21);
            this.chbFeatured.TabIndex = 29;
            this.chbFeatured.Text = "Featured";
            this.chbFeatured.UseVisualStyleBackColor = true;
            // 
            // chbDisplayOnWeb
            // 
            this.chbDisplayOnWeb.AutoSize = true;
            this.chbDisplayOnWeb.Location = new System.Drawing.Point(192, 157);
            this.chbDisplayOnWeb.Name = "chbDisplayOnWeb";
            this.chbDisplayOnWeb.Size = new System.Drawing.Size(132, 21);
            this.chbDisplayOnWeb.TabIndex = 36;
            this.chbDisplayOnWeb.Text = "Display On Web";
            this.chbDisplayOnWeb.UseVisualStyleBackColor = true;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(137, 16);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(32, 36);
            this.lblPrice.TabIndex = 37;
            this.lblPrice.Text = "$";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(139, 105);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(57, 20);
            this.lblName.TabIndex = 38;
            this.lblName.Text = "Name";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 181);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(79, 17);
            this.lblDescription.TabIndex = 39;
            this.lblDescription.Text = "Description";
            // 
            // lblUPC
            // 
            this.lblUPC.AutoSize = true;
            this.lblUPC.Location = new System.Drawing.Point(14, 236);
            this.lblUPC.Name = "lblUPC";
            this.lblUPC.Size = new System.Drawing.Size(71, 17);
            this.lblUPC.TabIndex = 40;
            this.lblUPC.Text = "UPC code";
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Location = new System.Drawing.Point(14, 253);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(92, 17);
            this.lblManufacturer.TabIndex = 41;
            this.lblManufacturer.Text = "Manufacturer";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(14, 270);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(46, 17);
            this.lblModel.TabIndex = 42;
            this.lblModel.Text = "Model";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(14, 287);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(35, 17);
            this.lblSize.TabIndex = 43;
            this.lblSize.Text = "Size";
            // 
            // InventoryItemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(323, 340);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.lblManufacturer);
            this.Controls.Add(this.lblUPC);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.chbDisplayOnWeb);
            this.Controls.Add(this.chbFeatured);
            this.Controls.Add(this.chbOnSale);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pictureBox1);
            this.Name = "InventoryItemInfo";
            this.Text = "Inventory Item";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chbOnSale;
        private System.Windows.Forms.CheckBox chbFeatured;
        private GelButton btnOK;
        private System.Windows.Forms.CheckBox chbDisplayOnWeb;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblUPC;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblSize;
    }
}