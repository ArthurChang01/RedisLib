﻿using SenderForm.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Transceiver.Model;
using Transceiver.Sender;

namespace SenderForm
{
    public partial class frmSender : Form
    {
        private SenderItem _target = null;
        private BindingList<SenderItem> _source = new BindingList<SenderItem>();

        private void refresh()
        {
            dgvSender.Refresh();
            txtID.Text = this._target.SenderId;
            cbIsInitiate.Checked = this._target.IsInitiate;
            txtCurrentState.Text = this._target.CurrentState;
            txtDataKey.Text = this._target.DataKey;
        }

        public frmSender()
        {
            InitializeComponent();

            dgvSender.AutoGenerateColumns = true;
            dgvSender.DataSource = _source;

            btnInitiate.Click += BtnInitiate_Click;
            btnRun.Click += BtnRun_Click;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SenderItem si =
                new SenderItem(new SenderContext<DTO>());
            _source.Add(si);
        }

        private void dgvSender_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _target = this._source[e.RowIndex];
            this.refresh();
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (!this._target.IsInitiate)
            {
                MessageBox.Show("You should initiate before you send message!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTO msg = new DTO { ID = Guid.NewGuid().ToString(), Value = "abc" };
            this._target.SendMsg(enLogType.API, msg);

            this.refresh();
        }

        private void BtnInitiate_Click(object sender, EventArgs e)
        {
            if (this._target == null)
            {
                MessageBox.Show("Must add a new redis node first", "error operation", MessageBoxButtons.OK);
                return;
            }

            this._target.InitialSender();
            this._target.IsInitiate = true;

            this.refresh();
        }

    }
}
