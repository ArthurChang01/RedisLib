namespace ReceiverForm
{
    partial class frmReceiver
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
            this.gbReceiverList = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.dgvReceiver = new System.Windows.Forms.DataGridView();
            this.lbID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lbIsInitiate = new System.Windows.Forms.Label();
            this.cbIsInitiate = new System.Windows.Forms.CheckBox();
            this.lbNodeId = new System.Windows.Forms.Label();
            this.txtNodeId = new System.Windows.Forms.TextBox();
            this.txtCurrentState = new System.Windows.Forms.TextBox();
            this.lbCurrentState = new System.Windows.Forms.Label();
            this.txtDataKey = new System.Windows.Forms.TextBox();
            this.lbDataKey = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnInitiate = new System.Windows.Forms.Button();
            this.gbReceiverList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceiver)).BeginInit();
            this.SuspendLayout();
            // 
            // gbReceiverList
            // 
            this.gbReceiverList.Controls.Add(this.btnAdd);
            this.gbReceiverList.Controls.Add(this.btnRemove);
            this.gbReceiverList.Controls.Add(this.dgvReceiver);
            this.gbReceiverList.Location = new System.Drawing.Point(18, 16);
            this.gbReceiverList.Margin = new System.Windows.Forms.Padding(4);
            this.gbReceiverList.Name = "gbReceiverList";
            this.gbReceiverList.Padding = new System.Windows.Forms.Padding(4);
            this.gbReceiverList.Size = new System.Drawing.Size(777, 310);
            this.gbReceiverList.TabIndex = 0;
            this.gbReceiverList.TabStop = false;
            this.gbReceiverList.Text = "Receiver List";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(536, 266);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(112, 31);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(656, 266);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(112, 31);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // dgvReceiver
            // 
            this.dgvReceiver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReceiver.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvReceiver.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvReceiver.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceiver.Location = new System.Drawing.Point(9, 28);
            this.dgvReceiver.Margin = new System.Windows.Forms.Padding(4);
            this.dgvReceiver.MultiSelect = false;
            this.dgvReceiver.Name = "dgvReceiver";
            this.dgvReceiver.RowTemplate.Height = 24;
            this.dgvReceiver.Size = new System.Drawing.Size(759, 223);
            this.dgvReceiver.TabIndex = 0;
            this.dgvReceiver.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceiver_RowEnter);
            // 
            // lbID
            // 
            this.lbID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(804, 47);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(28, 16);
            this.lbID.TabIndex = 1;
            this.lbID.Text = "ID:";
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Location = new System.Drawing.Point(899, 44);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(137, 27);
            this.txtID.TabIndex = 2;
            // 
            // lbIsInitiate
            // 
            this.lbIsInitiate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIsInitiate.AutoSize = true;
            this.lbIsInitiate.Location = new System.Drawing.Point(804, 108);
            this.lbIsInitiate.Name = "lbIsInitiate";
            this.lbIsInitiate.Size = new System.Drawing.Size(66, 16);
            this.lbIsInitiate.TabIndex = 3;
            this.lbIsInitiate.Text = "IsInitiate:";
            // 
            // cbIsInitiate
            // 
            this.cbIsInitiate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIsInitiate.AutoSize = true;
            this.cbIsInitiate.Location = new System.Drawing.Point(899, 110);
            this.cbIsInitiate.Name = "cbIsInitiate";
            this.cbIsInitiate.Size = new System.Drawing.Size(15, 14);
            this.cbIsInitiate.TabIndex = 4;
            this.cbIsInitiate.UseVisualStyleBackColor = true;
            // 
            // lbNodeId
            // 
            this.lbNodeId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNodeId.AutoSize = true;
            this.lbNodeId.Location = new System.Drawing.Point(804, 80);
            this.lbNodeId.Name = "lbNodeId";
            this.lbNodeId.Size = new System.Drawing.Size(59, 16);
            this.lbNodeId.TabIndex = 5;
            this.lbNodeId.Text = "NodeId:";
            // 
            // txtNodeId
            // 
            this.txtNodeId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNodeId.Location = new System.Drawing.Point(899, 77);
            this.txtNodeId.Name = "txtNodeId";
            this.txtNodeId.Size = new System.Drawing.Size(54, 27);
            this.txtNodeId.TabIndex = 6;
            // 
            // txtCurrentState
            // 
            this.txtCurrentState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentState.Location = new System.Drawing.Point(899, 130);
            this.txtCurrentState.Name = "txtCurrentState";
            this.txtCurrentState.Size = new System.Drawing.Size(137, 27);
            this.txtCurrentState.TabIndex = 8;
            // 
            // lbCurrentState
            // 
            this.lbCurrentState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCurrentState.AutoSize = true;
            this.lbCurrentState.Location = new System.Drawing.Point(804, 133);
            this.lbCurrentState.Name = "lbCurrentState";
            this.lbCurrentState.Size = new System.Drawing.Size(89, 16);
            this.lbCurrentState.TabIndex = 7;
            this.lbCurrentState.Text = "CurrentState:";
            // 
            // txtDataKey
            // 
            this.txtDataKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataKey.Location = new System.Drawing.Point(899, 163);
            this.txtDataKey.Name = "txtDataKey";
            this.txtDataKey.Size = new System.Drawing.Size(137, 27);
            this.txtDataKey.TabIndex = 10;
            // 
            // lbDataKey
            // 
            this.lbDataKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDataKey.AutoSize = true;
            this.lbDataKey.Location = new System.Drawing.Point(804, 166);
            this.lbDataKey.Name = "lbDataKey";
            this.lbDataKey.Size = new System.Drawing.Size(67, 16);
            this.lbDataKey.TabIndex = 9;
            this.lbDataKey.Text = "DataKey:";
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(923, 197);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(112, 31);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // btnInitiate
            // 
            this.btnInitiate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitiate.Location = new System.Drawing.Point(803, 197);
            this.btnInitiate.Margin = new System.Windows.Forms.Padding(4);
            this.btnInitiate.Name = "btnInitiate";
            this.btnInitiate.Size = new System.Drawing.Size(112, 31);
            this.btnInitiate.TabIndex = 11;
            this.btnInitiate.Text = "Initiate";
            this.btnInitiate.UseVisualStyleBackColor = true;
            // 
            // frmReceiver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 334);
            this.Controls.Add(this.btnInitiate);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtDataKey);
            this.Controls.Add(this.lbDataKey);
            this.Controls.Add(this.txtCurrentState);
            this.Controls.Add(this.lbCurrentState);
            this.Controls.Add(this.txtNodeId);
            this.Controls.Add(this.lbNodeId);
            this.Controls.Add(this.cbIsInitiate);
            this.Controls.Add(this.lbIsInitiate);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.gbReceiverList);
            this.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmReceiver";
            this.Text = "Receiver Form";
            this.gbReceiverList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceiver)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbReceiverList;
        private System.Windows.Forms.DataGridView dgvReceiver;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lbIsInitiate;
        private System.Windows.Forms.CheckBox cbIsInitiate;
        private System.Windows.Forms.Label lbNodeId;
        private System.Windows.Forms.TextBox txtNodeId;
        private System.Windows.Forms.TextBox txtCurrentState;
        private System.Windows.Forms.Label lbCurrentState;
        private System.Windows.Forms.TextBox txtDataKey;
        private System.Windows.Forms.Label lbDataKey;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnInitiate;
    }
}

