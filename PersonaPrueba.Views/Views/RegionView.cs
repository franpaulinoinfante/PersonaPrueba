using System;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using PersonaPrueba.Domain.Contracts;
using PersonaPrueba.Domain.Models;
using PersonaPrueba.Domain.ObjectValues;
using PersonaPrueba.Views.ViewHelps;
using PersonaPrueba.Views.ViewModels;

namespace PersonaPrueba.Views.Views
{
    public partial class RegionView : Form
    {
        private readonly RegionViewModel _region;
        private int _index = -1;
        private int _id = 0;

        public RegionView()
        {
            InitializeComponent();

            _region = new RegionViewModel();
        }

        private void RegionView_Load(object sender, EventArgs e)
        {
            GetRegions();
        }

        private void btnSalve_Click(object sender, EventArgs e)
        {
            if (DialogConfirm.GetResult("Do you want save the changes?", "Question").Equals(DialogResult.No))
            {
                return;
            }

            SetDataToPropierties();

            if (ValidationData(_region) == false)
            {
                return;
            }


            MessageResult.ShowResults(_region.SaveChanges());

            BlockControllers();

            ActionControls.RefreshControllers(this);

            GetRegions();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ActiveControllers();

            _region.State = EntityState.Added;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_index.Equals(-1))
            {
                MessageResult.LogErrors("Select a region");
                return;
            }

            ActiveControllers();

            _region.State = EntityState.Edited;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_id.Equals(0))
            {
                MessageResult.LogErrors("Select a region");
                return;
            }

            _region.State = EntityState.Deleted;

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
            try
            {
                dgvRegion.DataSource = _region.GetRegions();
            }
            catch (Exception ex)
            {
                MessageResult.LogErrors(ex.Message);
            }
        }

        private void SetDataToPropierties()
        {
            _region.RegionID = _id;
            _region.RegionName = txtRegionName.Text.Trim();
        }

        private bool ValidationData(RegionViewModel region)
        {
            bool valid = new DataValidation(region).Validate();

            if (valid == true)
            {
                return true;
            }
            else
            {
                return false;
            }
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
