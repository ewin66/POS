namespace VoodooPOS
{
    partial class LabelTemplate
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
            this.dgvLabels = new System.Windows.Forms.DataGridView();
            this.btnOK = new VoodooPOS.GelButton();
            this.btnCancel = new VoodooPOS.GelButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSelectedRow = new System.Windows.Forms.TextBox();
            this.btnReset = new VoodooPOS.GelButton();
            this.chkPrintOnSelect = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUpdate = new VoodooPOS.GelButton();
            this.ddColumns = new System.Windows.Forms.ComboBox();
            this.txtSelectedColumn = new System.Windows.Forms.TextBox();
            this.lblRow = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLabels
            // 
            this.dgvLabels.AllowUserToAddRows = false;
            this.dgvLabels.AllowUserToDeleteRows = false;
            this.dgvLabels.AllowUserToResizeColumns = false;
            this.dgvLabels.AllowUserToResizeRows = false;
            this.dgvLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLabels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLabels.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLabels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLabels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabels.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLabels.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLabels.Location = new System.Drawing.Point(0, 50);
            this.dgvLabels.MultiSelect = false;
            this.dgvLabels.Name = "dgvLabels";
            this.dgvLabels.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLabels.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLabels.RowHeadersVisible = false;
            this.dgvLabels.RowTemplate.Height = 24;
            this.dgvLabels.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvLabels.ShowEditingIcon = false;
            this.dgvLabels.Size = new System.Drawing.Size(545, 514);
            this.dgvLabels.TabIndex = 0;
            this.dgvLabels.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLabels_CellContentClick);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(168, 610);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Print";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(302, 610);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 573);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start printing at label: ";
            // 
            // txtSelectedRow
            // 
            this.txtSelectedRow.BackColor = System.Drawing.Color.White;
            this.txtSelectedRow.Location = new System.Drawing.Point(258, 568);
            this.txtSelectedRow.Name = "txtSelectedRow";
            this.txtSelectedRow.ReadOnly = true;
            this.txtSelectedRow.Size = new System.Drawing.Size(37, 22);
            this.txtSelectedRow.TabIndex = 4;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(402, 568);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // chkPrintOnSelect
            // 
            this.chkPrintOnSelect.AutoSize = true;
            this.chkPrintOnSelect.Checked = true;
            this.chkPrintOnSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintOnSelect.Location = new System.Drawing.Point(186, 647);
            this.chkPrintOnSelect.Name = "chkPrintOnSelect";
            this.chkPrintOnSelect.Size = new System.Drawing.Size(173, 21);
            this.chkPrintOnSelect.TabIndex = 6;
            this.chkPrintOnSelect.Text = "Print on label selection";
            this.chkPrintOnSelect.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Number of Columns:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(350, 10);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 34);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // ddColumns
            // 
            this.ddColumns.FormattingEnabled = true;
            this.ddColumns.Items.AddRange(new object[] {
            "3",
            "4"});
            this.ddColumns.Location = new System.Drawing.Point(261, 12);
            this.ddColumns.Name = "ddColumns";
            this.ddColumns.Size = new System.Drawing.Size(49, 24);
            this.ddColumns.TabIndex = 12;
            // 
            // txtSelectedColumn
            // 
            this.txtSelectedColumn.BackColor = System.Drawing.Color.White;
            this.txtSelectedColumn.Location = new System.Drawing.Point(354, 567);
            this.txtSelectedColumn.Name = "txtSelectedColumn";
            this.txtSelectedColumn.ReadOnly = true;
            this.txtSelectedColumn.Size = new System.Drawing.Size(37, 22);
            this.txtSelectedColumn.TabIndex = 13;
            // 
            // lblRow
            // 
            this.lblRow.AutoSize = true;
            this.lblRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRow.Location = new System.Drawing.Point(227, 577);
            this.lblRow.Name = "lblRow";
            this.lblRow.Size = new System.Drawing.Size(28, 12);
            this.lblRow.TabIndex = 14;
            this.lblRow.Text = "Row:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(311, 576);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "Column:";
            // 
            // LabelTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 680);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRow);
            this.Controls.Add(this.txtSelectedColumn);
            this.Controls.Add(this.ddColumns);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkPrintOnSelect);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtSelectedRow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvLabels);
            this.Name = "LabelTemplate";
            this.Text = "LabelTemplate";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLabels;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSelectedRow;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkPrintOnSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox ddColumns;
        private System.Windows.Forms.TextBox txtSelectedColumn;
        private System.Windows.Forms.Label lblRow;
        private System.Windows.Forms.Label label3;
    }
}