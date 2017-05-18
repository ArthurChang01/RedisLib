using ReceiverForm.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Transceiver.Receiver;

namespace ReceiverForm
{
    public partial class frmReceiver : Form
    {
        private ReceiverItem _target = null;
        private BindingList<ReceiverItem> _source = new BindingList<ReceiverItem>();

        private void refresh()
        {
            dgvReceiver.Refresh();
            txtID.Text = this._target.ReceiverId;
            txtNodeId.Text = this._target.NodeId.ToString();
            cbIsInitiate.Checked = this._target.IsInitiate;
            txtCurrentState.Text = this._target.CurrentState;
            txtDataKey.Text = this._target.DataKey;
        }

        public frmReceiver()
        {
            InitializeComponent();

            dgvReceiver.AutoGenerateColumns = true;
            dgvReceiver.DataSource = this._source;

            btnInitiate.Click += BtnInitiate_Click;
            btnRun.Click += BtnRun_Click;
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (!this._target.IsInitiate)
            {
                MessageBox.Show("You should initiate before you send message!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this._target.ReceiveMsg();

            this.refresh();
        }

        private void BtnInitiate_Click(object sender, EventArgs e)
        {
            this._target.InitialReceiver();
            this._target.IsInitiate = true;

            this.refresh();
        }

        private void dgvReceiver_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this._target = this._source[e.RowIndex];
            this.refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this._source.Add(new ReceiverItem(new ReceiverContext<object>()));
        }
    }
}
