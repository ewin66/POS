namespace VoodooPOS
{
    partial class Reports
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
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.giftCertificatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saleItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ddTransactionDates = new System.Windows.Forms.ComboBox();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbCash = new System.Windows.Forms.RadioButton();
            this.rbCredit = new System.Windows.Forms.RadioButton();
            this.ddInventoryItems = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.grpReportResults = new System.Windows.Forms.GroupBox();
            this.lblVoidTransactionsTotal = new System.Windows.Forms.Label();
            this.lblVoidTransactionsCount = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblRefundsTotal = new System.Windows.Forms.Label();
            this.lblRefundsCount = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lblMoneyDiscountTotal = new System.Windows.Forms.Label();
            this.lblMoneyDiscountCount = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblPercentDiscountTotal = new System.Windows.Forms.Label();
            this.lblPercentDiscountCount = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblTotalTaxTotal = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblTax2Total = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblTax1Total = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblSubtotalTotal = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblTaxable2Total = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblTaxable1Total = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblTotalTransactionsCount = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblNoSaleCount = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblVoidTransCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblRegisterTotalTotal = new System.Windows.Forms.Label();
            this.lblRegisterTotalCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblVisaMastercardTotal = new System.Windows.Forms.Label();
            this.lblVisaMastercardCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCashTotal = new System.Windows.Forms.Label();
            this.lblCashCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlNoResults = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPrintReport = new VoodooPOS.GelButton();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.featuredItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickButtonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.grpReportResults.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pnlNoResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvReport
            // 
            this.dgvReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(8, 6);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.RowTemplate.Height = 24;
            this.dgvReport.Size = new System.Drawing.Size(1166, 250);
            this.dgvReport.TabIndex = 0;
            this.dgvReport.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReport_CellValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem,
            this.reportsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1190, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailyToolStripMenuItem,
            this.overallToolStripMenuItem,
            this.cashToolStripMenuItem,
            this.creditToolStripMenuItem,
            this.monthlyToolStripMenuItem,
            this.giftCertificatesToolStripMenuItem,
            this.saleItemsToolStripMenuItem,
            this.featuredItemsToolStripMenuItem,
            this.quickButtonsToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // dailyToolStripMenuItem
            // 
            this.dailyToolStripMenuItem.Name = "dailyToolStripMenuItem";
            this.dailyToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.dailyToolStripMenuItem.Text = "End of Day";
            this.dailyToolStripMenuItem.Click += new System.EventHandler(this.endOfDayToolStripMenuItem_Click);
            // 
            // overallToolStripMenuItem
            // 
            this.overallToolStripMenuItem.Name = "overallToolStripMenuItem";
            this.overallToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.overallToolStripMenuItem.Text = "Overall";
            this.overallToolStripMenuItem.Click += new System.EventHandler(this.overallToolStripMenuItem_Click);
            // 
            // cashToolStripMenuItem
            // 
            this.cashToolStripMenuItem.Name = "cashToolStripMenuItem";
            this.cashToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.cashToolStripMenuItem.Text = "Cash";
            // 
            // creditToolStripMenuItem
            // 
            this.creditToolStripMenuItem.Name = "creditToolStripMenuItem";
            this.creditToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.creditToolStripMenuItem.Text = "Credit";
            // 
            // monthlyToolStripMenuItem
            // 
            this.monthlyToolStripMenuItem.Name = "monthlyToolStripMenuItem";
            this.monthlyToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.monthlyToolStripMenuItem.Text = "Monthly";
            this.monthlyToolStripMenuItem.Click += new System.EventHandler(this.monthlyToolStripMenuItem_Click);
            // 
            // giftCertificatesToolStripMenuItem
            // 
            this.giftCertificatesToolStripMenuItem.Name = "giftCertificatesToolStripMenuItem";
            this.giftCertificatesToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.giftCertificatesToolStripMenuItem.Text = "Gift Certificates";
            this.giftCertificatesToolStripMenuItem.Click += new System.EventHandler(this.giftCertificatesToolStripMenuItem_Click);
            // 
            // saleItemsToolStripMenuItem
            // 
            this.saleItemsToolStripMenuItem.Name = "saleItemsToolStripMenuItem";
            this.saleItemsToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.saleItemsToolStripMenuItem.Text = "Sale Items";
            this.saleItemsToolStripMenuItem.Click += new System.EventHandler(this.saleItemsToolStripMenuItem_Click);
            // 
            // ddTransactionDates
            // 
            this.ddTransactionDates.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ddTransactionDates.FormattingEnabled = true;
            this.ddTransactionDates.Location = new System.Drawing.Point(358, 33);
            this.ddTransactionDates.Name = "ddTransactionDates";
            this.ddTransactionDates.Size = new System.Drawing.Size(286, 24);
            this.ddTransactionDates.TabIndex = 3;
            this.ddTransactionDates.Text = "Transaction Dates";
            this.ddTransactionDates.SelectedIndexChanged += new System.EventHandler(this.ddTransactionDates_SelectedIndexChanged);
            // 
            // rbAll
            // 
            this.rbAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(650, 33);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(44, 21);
            this.rbAll.TabIndex = 6;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.Visible = false;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbCash
            // 
            this.rbCash.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbCash.AutoSize = true;
            this.rbCash.Location = new System.Drawing.Point(700, 34);
            this.rbCash.Name = "rbCash";
            this.rbCash.Size = new System.Drawing.Size(61, 21);
            this.rbCash.TabIndex = 7;
            this.rbCash.TabStop = true;
            this.rbCash.Text = "Cash";
            this.rbCash.UseVisualStyleBackColor = true;
            this.rbCash.Visible = false;
            this.rbCash.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbCredit
            // 
            this.rbCredit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbCredit.AutoSize = true;
            this.rbCredit.Location = new System.Drawing.Point(767, 33);
            this.rbCredit.Name = "rbCredit";
            this.rbCredit.Size = new System.Drawing.Size(66, 21);
            this.rbCredit.TabIndex = 8;
            this.rbCredit.TabStop = true;
            this.rbCredit.Text = "Credit";
            this.rbCredit.UseVisualStyleBackColor = true;
            this.rbCredit.Visible = false;
            this.rbCredit.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // ddInventoryItems
            // 
            this.ddInventoryItems.DisplayMember = "name";
            this.ddInventoryItems.FormattingEnabled = true;
            this.ddInventoryItems.Location = new System.Drawing.Point(121, 73);
            this.ddInventoryItems.Name = "ddInventoryItems";
            this.ddInventoryItems.Size = new System.Drawing.Size(330, 24);
            this.ddInventoryItems.TabIndex = 9;
            this.ddInventoryItems.Text = "Inventory";
            this.ddInventoryItems.ValueMember = "id";
            this.ddInventoryItems.Visible = false;
            this.ddInventoryItems.SelectedIndexChanged += new System.EventHandler(this.ddInventoryItems_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Inventory:";
            this.label1.Visible = false;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblReportTitle.AutoSize = true;
            this.lblReportTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportTitle.Location = new System.Drawing.Point(534, 75);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(123, 25);
            this.lblReportTitle.TabIndex = 11;
            this.lblReportTitle.Text = "Report Title";
            // 
            // grpReportResults
            // 
            this.grpReportResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.grpReportResults.Controls.Add(this.pnlNoResults);
            this.grpReportResults.Controls.Add(this.label7);
            this.grpReportResults.Controls.Add(this.label6);
            this.grpReportResults.Controls.Add(this.label4);
            this.grpReportResults.Controls.Add(this.label3);
            this.grpReportResults.Controls.Add(this.lblVoidTransactionsTotal);
            this.grpReportResults.Controls.Add(this.lblVoidTransactionsCount);
            this.grpReportResults.Controls.Add(this.label41);
            this.grpReportResults.Controls.Add(this.lblRefundsTotal);
            this.grpReportResults.Controls.Add(this.lblRefundsCount);
            this.grpReportResults.Controls.Add(this.label38);
            this.grpReportResults.Controls.Add(this.lblMoneyDiscountTotal);
            this.grpReportResults.Controls.Add(this.lblMoneyDiscountCount);
            this.grpReportResults.Controls.Add(this.label35);
            this.grpReportResults.Controls.Add(this.lblPercentDiscountTotal);
            this.grpReportResults.Controls.Add(this.lblPercentDiscountCount);
            this.grpReportResults.Controls.Add(this.label32);
            this.grpReportResults.Controls.Add(this.lblTotalTaxTotal);
            this.grpReportResults.Controls.Add(this.label29);
            this.grpReportResults.Controls.Add(this.lblTax2Total);
            this.grpReportResults.Controls.Add(this.label26);
            this.grpReportResults.Controls.Add(this.lblTax1Total);
            this.grpReportResults.Controls.Add(this.label23);
            this.grpReportResults.Controls.Add(this.lblSubtotalTotal);
            this.grpReportResults.Controls.Add(this.label21);
            this.grpReportResults.Controls.Add(this.lblTaxable2Total);
            this.grpReportResults.Controls.Add(this.label20);
            this.grpReportResults.Controls.Add(this.lblTaxable1Total);
            this.grpReportResults.Controls.Add(this.label15);
            this.grpReportResults.Controls.Add(this.lblTotalTransactionsCount);
            this.grpReportResults.Controls.Add(this.label17);
            this.grpReportResults.Controls.Add(this.lblNoSaleCount);
            this.grpReportResults.Controls.Add(this.label14);
            this.grpReportResults.Controls.Add(this.lblVoidTransCount);
            this.grpReportResults.Controls.Add(this.label11);
            this.grpReportResults.Controls.Add(this.lblRegisterTotalTotal);
            this.grpReportResults.Controls.Add(this.lblRegisterTotalCount);
            this.grpReportResults.Controls.Add(this.label8);
            this.grpReportResults.Controls.Add(this.lblVisaMastercardTotal);
            this.grpReportResults.Controls.Add(this.lblVisaMastercardCount);
            this.grpReportResults.Controls.Add(this.label5);
            this.grpReportResults.Controls.Add(this.lblCashTotal);
            this.grpReportResults.Controls.Add(this.lblCashCount);
            this.grpReportResults.Controls.Add(this.label2);
            this.grpReportResults.Location = new System.Drawing.Point(321, 6);
            this.grpReportResults.Name = "grpReportResults";
            this.grpReportResults.Size = new System.Drawing.Size(541, 484);
            this.grpReportResults.TabIndex = 13;
            this.grpReportResults.TabStop = false;
            this.grpReportResults.Text = "Report Results";
            // 
            // lblVoidTransactionsTotal
            // 
            this.lblVoidTransactionsTotal.AutoSize = true;
            this.lblVoidTransactionsTotal.Location = new System.Drawing.Point(492, 441);
            this.lblVoidTransactionsTotal.Name = "lblVoidTransactionsTotal";
            this.lblVoidTransactionsTotal.Size = new System.Drawing.Size(16, 17);
            this.lblVoidTransactionsTotal.TabIndex = 46;
            this.lblVoidTransactionsTotal.Text = "0";
            // 
            // lblVoidTransactionsCount
            // 
            this.lblVoidTransactionsCount.AutoSize = true;
            this.lblVoidTransactionsCount.Location = new System.Drawing.Point(378, 441);
            this.lblVoidTransactionsCount.Name = "lblVoidTransactionsCount";
            this.lblVoidTransactionsCount.Size = new System.Drawing.Size(16, 17);
            this.lblVoidTransactionsCount.TabIndex = 45;
            this.lblVoidTransactionsCount.Text = "0";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(30, 441);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(122, 17);
            this.label41.TabIndex = 44;
            this.label41.Text = "Void Transactions";
            // 
            // lblRefundsTotal
            // 
            this.lblRefundsTotal.AutoSize = true;
            this.lblRefundsTotal.Location = new System.Drawing.Point(492, 424);
            this.lblRefundsTotal.Name = "lblRefundsTotal";
            this.lblRefundsTotal.Size = new System.Drawing.Size(16, 17);
            this.lblRefundsTotal.TabIndex = 43;
            this.lblRefundsTotal.Text = "0";
            // 
            // lblRefundsCount
            // 
            this.lblRefundsCount.AutoSize = true;
            this.lblRefundsCount.Location = new System.Drawing.Point(378, 424);
            this.lblRefundsCount.Name = "lblRefundsCount";
            this.lblRefundsCount.Size = new System.Drawing.Size(16, 17);
            this.lblRefundsCount.TabIndex = 42;
            this.lblRefundsCount.Text = "0";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(30, 424);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(61, 17);
            this.label38.TabIndex = 41;
            this.label38.Text = "Refunds";
            // 
            // lblMoneyDiscountTotal
            // 
            this.lblMoneyDiscountTotal.AutoSize = true;
            this.lblMoneyDiscountTotal.Location = new System.Drawing.Point(492, 407);
            this.lblMoneyDiscountTotal.Name = "lblMoneyDiscountTotal";
            this.lblMoneyDiscountTotal.Size = new System.Drawing.Size(16, 17);
            this.lblMoneyDiscountTotal.TabIndex = 40;
            this.lblMoneyDiscountTotal.Text = "0";
            // 
            // lblMoneyDiscountCount
            // 
            this.lblMoneyDiscountCount.AutoSize = true;
            this.lblMoneyDiscountCount.Location = new System.Drawing.Point(378, 407);
            this.lblMoneyDiscountCount.Name = "lblMoneyDiscountCount";
            this.lblMoneyDiscountCount.Size = new System.Drawing.Size(16, 17);
            this.lblMoneyDiscountCount.TabIndex = 39;
            this.lblMoneyDiscountCount.Text = "0";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(30, 407);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(75, 17);
            this.label35.TabIndex = 38;
            this.label35.Text = "$ Discount";
            // 
            // lblPercentDiscountTotal
            // 
            this.lblPercentDiscountTotal.AutoSize = true;
            this.lblPercentDiscountTotal.Location = new System.Drawing.Point(492, 390);
            this.lblPercentDiscountTotal.Name = "lblPercentDiscountTotal";
            this.lblPercentDiscountTotal.Size = new System.Drawing.Size(16, 17);
            this.lblPercentDiscountTotal.TabIndex = 37;
            this.lblPercentDiscountTotal.Text = "0";
            // 
            // lblPercentDiscountCount
            // 
            this.lblPercentDiscountCount.AutoSize = true;
            this.lblPercentDiscountCount.Location = new System.Drawing.Point(378, 390);
            this.lblPercentDiscountCount.Name = "lblPercentDiscountCount";
            this.lblPercentDiscountCount.Size = new System.Drawing.Size(16, 17);
            this.lblPercentDiscountCount.TabIndex = 36;
            this.lblPercentDiscountCount.Text = "0";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(30, 390);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(79, 17);
            this.label32.TabIndex = 35;
            this.label32.Text = "% Discount";
            // 
            // lblTotalTaxTotal
            // 
            this.lblTotalTaxTotal.AutoSize = true;
            this.lblTotalTaxTotal.Location = new System.Drawing.Point(492, 349);
            this.lblTotalTaxTotal.Name = "lblTotalTaxTotal";
            this.lblTotalTaxTotal.Size = new System.Drawing.Size(16, 17);
            this.lblTotalTaxTotal.TabIndex = 34;
            this.lblTotalTaxTotal.Text = "0";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(30, 349);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(40, 17);
            this.label29.TabIndex = 32;
            this.label29.Text = "Total";
            // 
            // lblTax2Total
            // 
            this.lblTax2Total.AutoSize = true;
            this.lblTax2Total.Location = new System.Drawing.Point(492, 315);
            this.lblTax2Total.Name = "lblTax2Total";
            this.lblTax2Total.Size = new System.Drawing.Size(16, 17);
            this.lblTax2Total.TabIndex = 31;
            this.lblTax2Total.Text = "0";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(30, 315);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(43, 17);
            this.label26.TabIndex = 29;
            this.label26.Text = "Tax 2";
            // 
            // lblTax1Total
            // 
            this.lblTax1Total.AutoSize = true;
            this.lblTax1Total.Location = new System.Drawing.Point(492, 298);
            this.lblTax1Total.Name = "lblTax1Total";
            this.lblTax1Total.Size = new System.Drawing.Size(16, 17);
            this.lblTax1Total.TabIndex = 28;
            this.lblTax1Total.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(30, 298);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(43, 17);
            this.label23.TabIndex = 26;
            this.label23.Text = "Tax 1";
            // 
            // lblSubtotalTotal
            // 
            this.lblSubtotalTotal.AutoSize = true;
            this.lblSubtotalTotal.Location = new System.Drawing.Point(492, 259);
            this.lblSubtotalTotal.Name = "lblSubtotalTotal";
            this.lblSubtotalTotal.Size = new System.Drawing.Size(16, 17);
            this.lblSubtotalTotal.TabIndex = 25;
            this.lblSubtotalTotal.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(30, 259);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(141, 17);
            this.label21.TabIndex = 23;
            this.label21.Text = "Subtotal (NET Sales)";
            // 
            // lblTaxable2Total
            // 
            this.lblTaxable2Total.AutoSize = true;
            this.lblTaxable2Total.Location = new System.Drawing.Point(492, 225);
            this.lblTaxable2Total.Name = "lblTaxable2Total";
            this.lblTaxable2Total.Size = new System.Drawing.Size(16, 17);
            this.lblTaxable2Total.TabIndex = 22;
            this.lblTaxable2Total.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(30, 225);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 17);
            this.label20.TabIndex = 20;
            this.label20.Text = "Taxable 2";
            // 
            // lblTaxable1Total
            // 
            this.lblTaxable1Total.AutoSize = true;
            this.lblTaxable1Total.Location = new System.Drawing.Point(492, 208);
            this.lblTaxable1Total.Name = "lblTaxable1Total";
            this.lblTaxable1Total.Size = new System.Drawing.Size(16, 17);
            this.lblTaxable1Total.TabIndex = 19;
            this.lblTaxable1Total.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(30, 208);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 17);
            this.label15.TabIndex = 17;
            this.label15.Text = "Taxable 1";
            // 
            // lblTotalTransactionsCount
            // 
            this.lblTotalTransactionsCount.AutoSize = true;
            this.lblTotalTransactionsCount.Location = new System.Drawing.Point(378, 171);
            this.lblTotalTransactionsCount.Name = "lblTotalTransactionsCount";
            this.lblTotalTransactionsCount.Size = new System.Drawing.Size(16, 17);
            this.lblTotalTransactionsCount.TabIndex = 16;
            this.lblTotalTransactionsCount.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(30, 171);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(126, 17);
            this.label17.TabIndex = 15;
            this.label17.Text = "Total Transactions";
            // 
            // lblNoSaleCount
            // 
            this.lblNoSaleCount.AutoSize = true;
            this.lblNoSaleCount.Location = new System.Drawing.Point(378, 154);
            this.lblNoSaleCount.Name = "lblNoSaleCount";
            this.lblNoSaleCount.Size = new System.Drawing.Size(16, 17);
            this.lblNoSaleCount.TabIndex = 13;
            this.lblNoSaleCount.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(30, 154);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 17);
            this.label14.TabIndex = 12;
            this.label14.Text = "No Sale";
            // 
            // lblVoidTransCount
            // 
            this.lblVoidTransCount.AutoSize = true;
            this.lblVoidTransCount.Location = new System.Drawing.Point(378, 137);
            this.lblVoidTransCount.Name = "lblVoidTransCount";
            this.lblVoidTransCount.Size = new System.Drawing.Size(16, 17);
            this.lblVoidTransCount.TabIndex = 10;
            this.lblVoidTransCount.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 17);
            this.label11.TabIndex = 9;
            this.label11.Text = "Void Transactions";
            // 
            // lblRegisterTotalTotal
            // 
            this.lblRegisterTotalTotal.AutoSize = true;
            this.lblRegisterTotalTotal.Location = new System.Drawing.Point(492, 94);
            this.lblRegisterTotalTotal.Name = "lblRegisterTotalTotal";
            this.lblRegisterTotalTotal.Size = new System.Drawing.Size(16, 17);
            this.lblRegisterTotalTotal.TabIndex = 8;
            this.lblRegisterTotalTotal.Text = "0";
            // 
            // lblRegisterTotalCount
            // 
            this.lblRegisterTotalCount.AutoSize = true;
            this.lblRegisterTotalCount.Location = new System.Drawing.Point(378, 94);
            this.lblRegisterTotalCount.Name = "lblRegisterTotalCount";
            this.lblRegisterTotalCount.Size = new System.Drawing.Size(16, 17);
            this.lblRegisterTotalCount.TabIndex = 7;
            this.lblRegisterTotalCount.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 17);
            this.label8.TabIndex = 6;
            this.label8.Text = "Register Total";
            // 
            // lblVisaMastercardTotal
            // 
            this.lblVisaMastercardTotal.AutoSize = true;
            this.lblVisaMastercardTotal.Location = new System.Drawing.Point(492, 60);
            this.lblVisaMastercardTotal.Name = "lblVisaMastercardTotal";
            this.lblVisaMastercardTotal.Size = new System.Drawing.Size(16, 17);
            this.lblVisaMastercardTotal.TabIndex = 5;
            this.lblVisaMastercardTotal.Text = "0";
            // 
            // lblVisaMastercardCount
            // 
            this.lblVisaMastercardCount.AutoSize = true;
            this.lblVisaMastercardCount.Location = new System.Drawing.Point(378, 60);
            this.lblVisaMastercardCount.Name = "lblVisaMastercardCount";
            this.lblVisaMastercardCount.Size = new System.Drawing.Size(16, 17);
            this.lblVisaMastercardCount.TabIndex = 4;
            this.lblVisaMastercardCount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Visa/Mastercard";
            // 
            // lblCashTotal
            // 
            this.lblCashTotal.AutoSize = true;
            this.lblCashTotal.Location = new System.Drawing.Point(492, 43);
            this.lblCashTotal.Name = "lblCashTotal";
            this.lblCashTotal.Size = new System.Drawing.Size(16, 17);
            this.lblCashTotal.TabIndex = 2;
            this.lblCashTotal.Text = "0";
            // 
            // lblCashCount
            // 
            this.lblCashCount.AutoSize = true;
            this.lblCashCount.Location = new System.Drawing.Point(378, 43);
            this.lblCashCount.Name = "lblCashCount";
            this.lblCashCount.Size = new System.Drawing.Size(16, 17);
            this.lblCashCount.TabIndex = 1;
            this.lblCashCount.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cash";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 103);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1190, 525);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grpReportResults);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1182, 496);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvReport);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1182, 496);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Details";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(437, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 17);
            this.label3.TabIndex = 47;
            this.label3.Text = "------------------";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(322, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 17);
            this.label4.TabIndex = 48;
            this.label4.Text = "------------------";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(437, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 17);
            this.label6.TabIndex = 49;
            this.label6.Text = "------------------";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(437, 332);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 17);
            this.label7.TabIndex = 50;
            this.label7.Text = "------------------";
            // 
            // pnlNoResults
            // 
            this.pnlNoResults.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlNoResults.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlNoResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNoResults.Controls.Add(this.label9);
            this.pnlNoResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlNoResults.Location = new System.Drawing.Point(170, 192);
            this.pnlNoResults.Name = "pnlNoResults";
            this.pnlNoResults.Size = new System.Drawing.Size(200, 100);
            this.pnlNoResults.TabIndex = 15;
            this.pnlNoResults.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(8, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(184, 39);
            this.label9.TabIndex = 0;
            this.label9.Text = "No Results";
            // 
            // btnPrintReport
            // 
            this.btnPrintReport.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrintReport.Location = new System.Drawing.Point(549, 634);
            this.btnPrintReport.Name = "btnPrintReport";
            this.btnPrintReport.Size = new System.Drawing.Size(95, 34);
            this.btnPrintReport.TabIndex = 12;
            this.btnPrintReport.Text = "Print Report";
            this.btnPrintReport.UseVisualStyleBackColor = true;
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkoutToolStripMenuItem,
            this.inventoryToolStripMenuItem,
            this.reportsToolStripMenuItem1});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // checkoutToolStripMenuItem
            // 
            this.checkoutToolStripMenuItem.Name = "checkoutToolStripMenuItem";
            this.checkoutToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.checkoutToolStripMenuItem.Text = "Checkout";
            this.checkoutToolStripMenuItem.Click += new System.EventHandler(this.checkoutToolStripMenuItem_Click);
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.inventoryToolStripMenuItem.Text = "Inventory";
            this.inventoryToolStripMenuItem.Click += new System.EventHandler(this.inventoryToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem1
            // 
            this.reportsToolStripMenuItem1.Name = "reportsToolStripMenuItem1";
            this.reportsToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.reportsToolStripMenuItem1.Text = "Reports";
            // 
            // featuredItemsToolStripMenuItem
            // 
            this.featuredItemsToolStripMenuItem.Name = "featuredItemsToolStripMenuItem";
            this.featuredItemsToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.featuredItemsToolStripMenuItem.Text = "Featured Items";
            this.featuredItemsToolStripMenuItem.Click += new System.EventHandler(this.featuredItemsToolStripMenuItem_Click);
            // 
            // quickButtonsToolStripMenuItem
            // 
            this.quickButtonsToolStripMenuItem.Name = "quickButtonsToolStripMenuItem";
            this.quickButtonsToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.quickButtonsToolStripMenuItem.Text = "Quick Buttons";
            this.quickButtonsToolStripMenuItem.Click += new System.EventHandler(this.quickButtonsToolStripMenuItem_Click);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1190, 678);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnPrintReport);
            this.Controls.Add(this.lblReportTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddInventoryItems);
            this.Controls.Add(this.rbCredit);
            this.Controls.Add(this.rbCash);
            this.Controls.Add(this.rbAll);
            this.Controls.Add(this.ddTransactionDates);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Reports";
            this.Text = "Reports";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpReportResults.ResumeLayout(false);
            this.grpReportResults.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.pnlNoResults.ResumeLayout(false);
            this.pnlNoResults.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailyToolStripMenuItem;
        private System.Windows.Forms.ComboBox ddTransactionDates;
        private System.Windows.Forms.ToolStripMenuItem overallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthlyToolStripMenuItem;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbCash;
        private System.Windows.Forms.RadioButton rbCredit;
        private System.Windows.Forms.ToolStripMenuItem giftCertificatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleItemsToolStripMenuItem;
        private System.Windows.Forms.ComboBox ddInventoryItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblReportTitle;
        private GelButton btnPrintReport;
        private System.Windows.Forms.GroupBox grpReportResults;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSubtotalTotal;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblTaxable2Total;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblTaxable1Total;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblTotalTransactionsCount;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblNoSaleCount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblVoidTransCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblRegisterTotalTotal;
        private System.Windows.Forms.Label lblRegisterTotalCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblVisaMastercardTotal;
        private System.Windows.Forms.Label lblVisaMastercardCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCashTotal;
        private System.Windows.Forms.Label lblCashCount;
        private System.Windows.Forms.Label lblVoidTransactionsTotal;
        private System.Windows.Forms.Label lblVoidTransactionsCount;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label lblRefundsTotal;
        private System.Windows.Forms.Label lblRefundsCount;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lblMoneyDiscountTotal;
        private System.Windows.Forms.Label lblMoneyDiscountCount;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lblPercentDiscountTotal;
        private System.Windows.Forms.Label lblPercentDiscountCount;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label lblTotalTaxTotal;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblTax2Total;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblTax1Total;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlNoResults;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem featuredItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickButtonsToolStripMenuItem;
    }
}