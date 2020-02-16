using System;
using System.Windows.Forms;
using PersonaPrueba.Domain.Contracts;
using PersonaPrueba.Domain.Models;
using PersonaPrueba.Domain.ObjectValues;
using PersonaPrueba.Views.ViewHelps;

namespace PersonaPrueba.Views.Views
{
    public partial class RegionView : Form
    {
        private readonly IRegion _regionModel;
        private int _index = -1;
        private int _id = 0;

        public RegionView()
        {
            InitializeComponent();

            _regionModel = new RegionModel();
        }

        private void RegionView_Load(object sender, EventArgs e)
        {
            GetRegions();
        }

        private void btnSalve_Click(object sender, EventArgs e)
        {
            if (DialogConfirm.GetResult("Do you want save the changes?", "Question").Equals(DialogResult.Cancel))
            {
                return;
            }

            SetDataToPropierties();

            MessageResult.ShowResults(_regionModel.SaveChanges());

            BlockControllers();

            ActionControls.RefreshControllers(this);

            GetRegions();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ActiveControllers();

            _regionModel.State = EntityState.Added;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_index.Equals(-1))
            {
                MessageResult.LogErrors("Select a region");
                return;
            }

            ActiveControllers();

            _regionModel.State = EntityState.Edited;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_id.Equals(0))
            {
                MessageResult.LogErrors("Select a region");
                return;
            }

            _regionModel.State = EntityState.Deleted;

            btnSalve_Click(this, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (DialogConfirm.GetResult("Do you want cancel the changes", "Question").Equals(DialogResult.No))
            {
                return;
            }

            ActionControls.RefreshControllers(this);

            BlockControllers();
        }

        private void dgvRegion_DoubleClick(object sender, EventArgs e)
        {
            _index = Convert.ToInt32(dgvRegion.CurrentRow.Index);
            _id = Convert.ToInt32(dgvRegion.CurrentRow.Cells["RegionID"].Value);
            txtRegionName.Text = dgvRegion.CurrentRow.Cells["RegionName"].Value.ToString().Trim();
        }

        //------------------- Methods -----------------------------------

        private void GetRegions()
        {
            dgvRegion.DataSource = _regionModel.GetAll();
        }

        private void SetDataToPropierties()
        {
            _regionModel.RegionID = _id;
            _regionModel.RegionName = txtRegionName.Text.Trim();
        }

        private void ActiveControllers()
        {
            ActionControls.ActiveControllers(this);

            btnNew.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            txtFindBy.ReadOnly = true;

            txtRegionName.Focus();
        }

        private void BlockControllers()
        {
            ActionControls.BlockControllers(this);

            btnNew.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            txtFindBy.ReadOnly = false;
        }
    }
}
