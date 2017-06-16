using CoreLib.DB;
using CoreLib.ES;
using FinderForm.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;

namespace FinderForm
{
    public partial class frmFinder : Form
    {
        private IESer _eser;
        private IDBer _dber;
        private BindingList<DTO> _source = new BindingList<DTO>();

        public frmFinder()
        {
            InitializeComponent();

            dgvResultSet.AutoGenerateColumns = true;
            dgvResultSet.DataSource = this._source;

            string esConString = ConfigurationManager.ConnectionStrings["es"].ConnectionString,
                      dbConString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            this._eser = new ESer(esConString);
            this._dber = new DBer(dbConString);
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            DateTime dtStart = dtpStart.Value.ToUniversalTime(),
                             dtEnd = dtpEnd.Value.ToUniversalTime(),
                             dtNow = DateTime.Now.ToUniversalTime();
            string term = txtTerm.Text;


        }
    }
}
