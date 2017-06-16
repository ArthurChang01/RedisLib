namespace FinderForm
{
    partial class frmFinder
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
            this.dgvResultSet = new System.Windows.Forms.DataGridView();
            this.gbCondition = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtTerm = new System.Windows.Forms.TextBox();
            this.lbTerm = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lbEnd = new System.Windows.Forms.Label();
            this.lbStart = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultSet)).BeginInit();
            this.gbCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvResultSet
            // 
            this.dgvResultSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResultSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultSet.Location = new System.Drawing.Point(18, 118);
            this.dgvResultSet.Margin = new System.Windows.Forms.Padding(4);
            this.dgvResultSet.Name = "dgvResultSet";
            this.dgvResultSet.RowTemplate.Height = 24;
            this.dgvResultSet.Size = new System.Drawing.Size(684, 297);
            this.dgvResultSet.TabIndex = 0;
            // 
            // gbCondition
            // 
            this.gbCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCondition.Controls.Add(this.btnSearch);
            this.gbCondition.Controls.Add(this.txtTerm);
            this.gbCondition.Controls.Add(this.lbTerm);
            this.gbCondition.Controls.Add(this.dtpEnd);
            this.gbCondition.Controls.Add(this.dtpStart);
            this.gbCondition.Controls.Add(this.lbEnd);
            this.gbCondition.Controls.Add(this.lbStart);
            this.gbCondition.Location = new System.Drawing.Point(18, 13);
            this.gbCondition.Margin = new System.Windows.Forms.Padding(4);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Padding = new System.Windows.Forms.Padding(4);
            this.gbCondition.Size = new System.Drawing.Size(684, 97);
            this.gbCondition.TabIndex = 1;
            this.gbCondition.TabStop = false;
            this.gbCondition.Text = "Condition";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(598, 57);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(76, 33);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查詢";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtTerm
            // 
            this.txtTerm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTerm.Location = new System.Drawing.Point(387, 21);
            this.txtTerm.Name = "txtTerm";
            this.txtTerm.Size = new System.Drawing.Size(288, 27);
            this.txtTerm.TabIndex = 5;
            // 
            // lbTerm
            // 
            this.lbTerm.AutoSize = true;
            this.lbTerm.Location = new System.Drawing.Point(321, 24);
            this.lbTerm.Name = "lbTerm";
            this.lbTerm.Size = new System.Drawing.Size(60, 16);
            this.lbTerm.TabIndex = 4;
            this.lbTerm.Text = "關鍵字:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(89, 50);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 27);
            this.dtpEnd.TabIndex = 3;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(89, 17);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 27);
            this.dtpStart.TabIndex = 2;
            // 
            // lbEnd
            // 
            this.lbEnd.AutoSize = true;
            this.lbEnd.Location = new System.Drawing.Point(7, 55);
            this.lbEnd.Name = "lbEnd";
            this.lbEnd.Size = new System.Drawing.Size(76, 16);
            this.lbEnd.TabIndex = 1;
            this.lbEnd.Text = "結束日期:";
            // 
            // lbStart
            // 
            this.lbStart.AutoSize = true;
            this.lbStart.Location = new System.Drawing.Point(7, 24);
            this.lbStart.Name = "lbStart";
            this.lbStart.Size = new System.Drawing.Size(76, 16);
            this.lbStart.TabIndex = 0;
            this.lbStart.Text = "啟始日期:";
            // 
            // frmFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 430);
            this.Controls.Add(this.gbCondition);
            this.Controls.Add(this.dgvResultSet);
            this.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmFinder";
            this.Text = "Finder";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultSet)).EndInit();
            this.gbCondition.ResumeLayout(false);
            this.gbCondition.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResultSet;
        private System.Windows.Forms.GroupBox gbCondition;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtTerm;
        private System.Windows.Forms.Label lbTerm;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lbEnd;
        private System.Windows.Forms.Label lbStart;
    }
}

