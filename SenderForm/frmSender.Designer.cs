namespace SenderForm
{
    partial class frmSender
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.gbBorder = new System.Windows.Forms.GroupBox();
            this.dgvSender = new System.Windows.Forms.DataGridView();
            this.lbID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lbIsInitiate = new System.Windows.Forms.Label();
            this.cbIsInitiate = new System.Windows.Forms.CheckBox();
            this.lbCurrentState = new System.Windows.Forms.Label();
            this.txtCurrentState = new System.Windows.Forms.TextBox();
            this.txtDataKey = new System.Windows.Forms.TextBox();
            this.lbDataKey = new System.Windows.Forms.Label();
            this.btnInitiate = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.gbBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSender)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(369, 325);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(112, 31);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(489, 325);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(112, 31);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // gbBorder
            // 
            this.gbBorder.Controls.Add(this.dgvSender);
            this.gbBorder.Controls.Add(this.btnAdd);
            this.gbBorder.Controls.Add(this.btnRemove);
            this.gbBorder.Location = new System.Drawing.Point(18, 16);
            this.gbBorder.Margin = new System.Windows.Forms.Padding(4);
            this.gbBorder.Name = "gbBorder";
            this.gbBorder.Padding = new System.Windows.Forms.Padding(4);
            this.gbBorder.Size = new System.Drawing.Size(609, 364);
            this.gbBorder.TabIndex = 2;
            this.gbBorder.TabStop = false;
            this.gbBorder.Text = "Sender List";
            // 
            // dgvSender
            // 
            this.dgvSender.AllowUserToAddRows = false;
            this.dgvSender.AllowUserToDeleteRows = false;
            this.dgvSender.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvSender.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvSender.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSender.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvSender.Location = new System.Drawing.Point(4, 24);
            this.dgvSender.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSender.MultiSelect = false;
            this.dgvSender.Name = "dgvSender";
            this.dgvSender.ReadOnly = true;
            this.dgvSender.RowTemplate.Height = 24;
            this.dgvSender.Size = new System.Drawing.Size(601, 293);
            this.dgvSender.TabIndex = 0;
            this.dgvSender.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSender_RowEnter);
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(635, 40);
            this.lbID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(28, 16);
            this.lbID.TabIndex = 3;
            this.lbID.Text = "ID:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(731, 37);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(216, 27);
            this.txtID.TabIndex = 4;
            // 
            // lbIsInitiate
            // 
            this.lbIsInitiate.AutoSize = true;
            this.lbIsInitiate.Location = new System.Drawing.Point(635, 73);
            this.lbIsInitiate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIsInitiate.Name = "lbIsInitiate";
            this.lbIsInitiate.Size = new System.Drawing.Size(66, 16);
            this.lbIsInitiate.TabIndex = 5;
            this.lbIsInitiate.Text = "IsInitiate:";
            // 
            // cbIsInitiate
            // 
            this.cbIsInitiate.AutoSize = true;
            this.cbIsInitiate.Location = new System.Drawing.Point(731, 75);
            this.cbIsInitiate.Name = "cbIsInitiate";
            this.cbIsInitiate.Size = new System.Drawing.Size(15, 14);
            this.cbIsInitiate.TabIndex = 6;
            this.cbIsInitiate.UseVisualStyleBackColor = true;
            // 
            // lbCurrentState
            // 
            this.lbCurrentState.AutoSize = true;
            this.lbCurrentState.Location = new System.Drawing.Point(635, 104);
            this.lbCurrentState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCurrentState.Name = "lbCurrentState";
            this.lbCurrentState.Size = new System.Drawing.Size(89, 16);
            this.lbCurrentState.TabIndex = 7;
            this.lbCurrentState.Text = "CurrentState:";
            // 
            // txtCurrentState
            // 
            this.txtCurrentState.Location = new System.Drawing.Point(731, 101);
            this.txtCurrentState.Name = "txtCurrentState";
            this.txtCurrentState.ReadOnly = true;
            this.txtCurrentState.Size = new System.Drawing.Size(141, 27);
            this.txtCurrentState.TabIndex = 8;
            // 
            // txtDataKey
            // 
            this.txtDataKey.Location = new System.Drawing.Point(731, 144);
            this.txtDataKey.Name = "txtDataKey";
            this.txtDataKey.ReadOnly = true;
            this.txtDataKey.Size = new System.Drawing.Size(216, 27);
            this.txtDataKey.TabIndex = 10;
            // 
            // lbDataKey
            // 
            this.lbDataKey.AutoSize = true;
            this.lbDataKey.Location = new System.Drawing.Point(635, 147);
            this.lbDataKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDataKey.Name = "lbDataKey";
            this.lbDataKey.Size = new System.Drawing.Size(67, 16);
            this.lbDataKey.TabIndex = 9;
            this.lbDataKey.Text = "DataKey:";
            // 
            // btnInitiate
            // 
            this.btnInitiate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitiate.Location = new System.Drawing.Point(715, 187);
            this.btnInitiate.Margin = new System.Windows.Forms.Padding(4);
            this.btnInitiate.Name = "btnInitiate";
            this.btnInitiate.Size = new System.Drawing.Size(112, 31);
            this.btnInitiate.TabIndex = 11;
            this.btnInitiate.Text = "Initiate";
            this.btnInitiate.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(835, 187);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(112, 31);
            this.btnRun.TabIndex = 12;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // frmSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 389);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnInitiate);
            this.Controls.Add(this.txtDataKey);
            this.Controls.Add(this.lbDataKey);
            this.Controls.Add(this.txtCurrentState);
            this.Controls.Add(this.lbCurrentState);
            this.Controls.Add(this.cbIsInitiate);
            this.Controls.Add(this.lbIsInitiate);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.gbBorder);
            this.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSender";
            this.Text = "Sender Form";
            this.gbBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSender)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.GroupBox gbBorder;
        private System.Windows.Forms.DataGridView dgvSender;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lbIsInitiate;
        private System.Windows.Forms.CheckBox cbIsInitiate;
        private System.Windows.Forms.Label lbCurrentState;
        private System.Windows.Forms.TextBox txtCurrentState;
        private System.Windows.Forms.TextBox txtDataKey;
        private System.Windows.Forms.Label lbDataKey;
        private System.Windows.Forms.Button btnInitiate;
        private System.Windows.Forms.Button btnRun;
    }
}

