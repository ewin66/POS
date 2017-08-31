namespace VoodooPOS
{
    partial class InventoryWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpGridDisplay = new System.Windows.Forms.GroupBox();
            this.btnSave = new VoodooPOS.GelButton();
            this.btnRemove = new VoodooPOS.GelButton();
            this.dgvItemsToDisplay = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Items = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wholesalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onSale = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.salePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.featuredItem = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Manufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ddInventory = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSearchBox = new System.Windows.Forms.TextBox();
            this.btnCancelSearch = new VoodooPOS.GelButton();
            this.btnSearch = new VoodooPOS.GelButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ddTransactionDates = new System.Windows.Forms.ComboBox();
            this.ddTransactionTimes = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grpGridDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsToDisplay)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGridDisplay
            // 
            this.grpGridDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGridDisplay.BackColor = System.Drawing.Color.Transparent;
            this.grpGridDisplay.Controls.Add(this.btnSave);
            this.grpGridDisplay.Controls.Add(this.btnRemove);
            this.grpGridDisplay.Controls.Add(this.dgvItemsToDisplay);
            this.grpGridDisplay.ForeColor = System.Drawing.Color.Black;
            this.grpGridDisplay.Location = new System.Drawing.Point(12, 146);
            this.grpGridDisplay.Name = "grpGridDisplay";
            this.grpGridDisplay.Size = new System.Drawing.Size(956, 447);
            this.grpGridDisplay.TabIndex = 1;
            this.grpGridDisplay.TabStop = false;
            this.grpGridDisplay.Text = "Inventory";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(382, 416);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRemove.ForeColor = System.Drawing.Color.Black;
            this.btnRemove.Location = new System.Drawing.Point(500, 416);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 25);
            this.btnRemove.TabIndex = 16;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // dgvItemsToDisplay
            // 
            this.dgvItemsToDisplay.AllowUserToAddRows = false;
            this.dgvItemsToDisplay.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvItemsToDisplay.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemsToDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemsToDisplay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItemsToDisplay.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.dgvItemsToDisplay.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemsToDisplay.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItemsToDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemsToDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Items,
            this.Description,
            this.categoryID,
            this.Price,
            this.wholesalePrice,
            this.onSale,
            this.salePrice,
            this.Quantity,
            this.subtotal,
            this.featuredItem,
            this.Model,
            this.Size,
            this.Color,
            this.UPC,
            this.Manufacturer});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemsToDisplay.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvItemsToDisplay.GridColor = System.Drawing.Color.Green;
            this.dgvItemsToDisplay.Location = new System.Drawing.Point(3, 18);
            this.dgvItemsToDisplay.MultiSelect = false;
            this.dgvItemsToDisplay.Name = "dgvItemsToDisplay";
            this.dgvItemsToDisplay.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemsToDisplay.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvItemsToDisplay.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvItemsToDisplay.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvItemsToDisplay.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Lime;
            this.dgvItemsToDisplay.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvItemsToDisplay.RowTemplate.Height = 24;
            this.dgvItemsToDisplay.Size = new System.Drawing.Size(947, 394);
            this.dgvItemsToDisplay.TabIndex = 0;
            this.dgvItemsToDisplay.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemsToDisplay_CellMouseDoubleClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "ID";
            this.id.HeaderText = "id";
            this.id.MinimumWidth = 2;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // Items
            // 
            this.Items.DataPropertyName = "name";
            this.Items.FillWeight = 239.5939F;
            this.Items.HeaderText = "Name";
            this.Items.MinimumWidth = 100;
            this.Items.Name = "Items";
            this.Items.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // categoryID
            // 
            this.categoryID.DataPropertyName = "categoryID";
            this.categoryID.HeaderText = "categoryID";
            this.categoryID.Name = "categoryID";
            this.categoryID.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "price";
            this.Price.FillWeight = 119.797F;
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // wholesalePrice
            // 
            this.wholesalePrice.DataPropertyName = "WholesalePrice";
            this.wholesalePrice.HeaderText = "WholesalePrice";
            this.wholesalePrice.Name = "wholesalePrice";
            this.wholesalePrice.ReadOnly = true;
            // 
            // onSale
            // 
            this.onSale.DataPropertyName = "OnSale";
            this.onSale.HeaderText = "On Sale";
            this.onSale.Name = "onSale";
            this.onSale.ReadOnly = true;
            // 
            // salePrice
            // 
            this.salePrice.DataPropertyName = "SalePrice";
            this.salePrice.HeaderText = "Sale Price";
            this.salePrice.Name = "salePrice";
            this.salePrice.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "quantity";
            this.Quantity.FillWeight = 40.60914F;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.MinimumWidth = 20;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // subtotal
            // 
            this.subtotal.DataPropertyName = "subtotal";
            this.subtotal.HeaderText = "Subtotal";
            this.subtotal.Name = "subtotal";
            this.subtotal.ReadOnly = true;
            // 
            // featuredItem
            // 
            this.featuredItem.HeaderText = "Featured Item";
            this.featuredItem.Name = "featuredItem";
            this.featuredItem.ReadOnly = true;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            // 
            // Size
            // 
            this.Size.DataPropertyName = "Size";
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            // 
            // Color
            // 
            this.Color.DataPropertyName = "Color";
            this.Color.HeaderText = "Color";
            this.Color.Name = "Color";
            this.Color.ReadOnly = true;
            // 
            // UPC
            // 
            this.UPC.DataPropertyName = "upc";
            this.UPC.HeaderText = "UPC";
            this.UPC.MinimumWidth = 2;
            this.UPC.Name = "UPC";
            this.UPC.ReadOnly = true;
            // 
            // Manufacturer
            // 
            this.Manufacturer.DataPropertyName = "Manufacturer";
            this.Manufacturer.HeaderText = "Manufacturer";
            this.Manufacturer.MinimumWidth = 2;
            this.Manufacturer.Name = "Manufacturer";
            this.Manufacturer.ReadOnly = true;
            // 
            // ddInventory
            // 
            this.ddInventory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ddInventory.BackColor = System.Drawing.Color.PowderBlue;
            this.ddInventory.DisplayMember = "name";
            this.ddInventory.FormattingEnabled = true;
            this.ddInventory.Location = new System.Drawing.Point(6, 43);
            this.ddInventory.Name = "ddInventory";
            this.ddInventory.Size = new System.Drawing.Size(334, 24);
            this.ddInventory.TabIndex = 3;
            this.ddInventory.Text = "Select/Scan an Item";
            this.ddInventory.ValueMember = "id";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.txtSearchBox);
            this.groupBox1.Controls.Add(this.btnCancelSearch);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(423, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 52);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Items";
            // 
            // txtSearchBox
            // 
            this.txtSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchBox.BackColor = System.Drawing.Color.PowderBlue;
            this.txtSearchBox.Location = new System.Drawing.Point(4, 22);
            this.txtSearchBox.Name = "txtSearchBox";
            this.txtSearchBox.Size = new System.Drawing.Size(258, 22);
            this.txtSearchBox.TabIndex = 1;
            // 
            // btnCancelSearch
            // 
            this.btnCancelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelSearch.Location = new System.Drawing.Point(297, 21);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.Size = new System.Drawing.Size(28, 23);
            this.btnCancelSearch.TabIndex = 3;
            this.btnCancelSearch.Text = "X";
            this.btnCancelSearch.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(268, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 23);
            this.btnSearch.TabIndex = 107;
            this.btnSearch.Text = "?";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(981, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkoutToolStripMenuItem,
            this.inventoryToolStripMenuItem,
            this.reportsToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // checkoutToolStripMenuItem
            // 
            this.checkoutToolStripMenuItem.Name = "checkoutToolStripMenuItem";
            this.checkoutToolStripMenuItem.Size = new System.Drawing.Size(139, 24);
            this.checkoutToolStripMenuItem.Text = "Checkout";
            this.checkoutToolStripMenuItem.Click += new System.EventHandler(this.checkoutToolStripMenuItem_Click);
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.Checked = true;
            this.inventoryToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(139, 24);
            this.inventoryToolStripMenuItem.Text = "Inventory";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(139, 24);
            this.reportsToolStripMenuItem.Text = "Reports";
            this.reportsToolStripMenuItem.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.ddInventory);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(105, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(770, 80);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Inventory List";
            // 
            // ddTransactionDates
            // 
            this.ddTransactionDates.FormattingEnabled = true;
            this.ddTransactionDates.Location = new System.Drawing.Point(6, 21);
            this.ddTransactionDates.Name = "ddTransactionDates";
            this.ddTransactionDates.Size = new System.Drawing.Size(209, 24);
            this.ddTransactionDates.TabIndex = 28;
            // 
            // ddTransactionTimes
            // 
            this.ddTransactionTimes.FormattingEnabled = true;
            this.ddTransactionTimes.Location = new System.Drawing.Point(6, 55);
            this.ddTransactionTimes.Name = "ddTransactionTimes";
            this.ddTransactionTimes.Size = new System.Drawing.Size(209, 24);
            this.ddTransactionTimes.TabIndex = 25;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.ddTransactionDates);
            this.groupBox3.Controls.Add(this.ddTransactionTimes);
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(917, 32);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 109);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "History";
            this.groupBox3.Visible = false;
            // 
            // InventoryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(981, 605);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpGridDisplay);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InventoryWindow";
            this.Text = "Inventory";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.grpGridDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsToDisplay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpGridDisplay;
        private GelButton btnRemove;
        private System.Windows.Forms.DataGridView dgvItemsToDisplay;
        private System.Windows.Forms.ComboBox ddInventory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ddTransactionDates;
        private System.Windows.Forms.ComboBox ddTransactionTimes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSearchBox;
        private GelButton btnSearch;
        private GelButton btnCancelSearch;
        private System.Windows.Forms.ToolStripMenuItem checkoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Items;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn wholesalePrice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn onSale;
        private System.Windows.Forms.DataGridViewTextBoxColumn salePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn featuredItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Color;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Manufacturer;
        private GelButton btnSave;
    }
}