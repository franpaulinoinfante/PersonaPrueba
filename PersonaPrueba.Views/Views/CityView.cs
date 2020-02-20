using System;
using System.Windows.Forms;
using PersonaPrueba.Domain.ObjectValues;
using PersonaPrueba.Views.ViewHelps;
using PersonaPrueba.Views.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PersonaPrueba.Views.Views
{
    public partial class CityView : Form
    {
        private CityViewModel _cityViewModel;

        private int _index = -1;
        private int _id = -1;

        public CityView()
        {
            InitializeComponent();

            _cityViewModel = new CityViewModel();
        }

        private void CityView_Load(object sender, EventArgs e)
        {
            GetCities();
            GetRegions();
        }

        private void btnSalve_Click(object sender, EventArgs e)
        {
            if (DialogConfirm.GetResult("Do you want save the changes?", "Question").Equals(DialogResult.No))
            {
                return;
            }

            SetDataToPropierties();

            if (ValidationData(_cityViewModel) == true)
            {
                return;
            }

            MessageResult.ShowResults(_cityViewModel.SaveChanges());

            BlockControllers();

            ActionControls.RefreshControllers(this);

            GetCities();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ActionControls.RefreshControllers(this);

            ActiveControlleres();
            
            _cityViewModel.State = EntityState.Added;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_index.Equals(-1))
            {
                MessageResult.LogErrors("Select a city");
                return;
            }

            ActiveControlleres();

            _cityViewModel.State = EntityState.Edited;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_id.Equals(-1))
            {
                MessageResult.LogErrors("Select a city");
                return;
            }

            _cityViewModel.State = EntityState.Deleted;

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

        private void dgvCity_DoubleClick(object sender, EventArgs e)
        {
            _index = Convert.ToInt32(dgvCity.CurrentRow.Index);
            _id = Convert.ToInt32(dgvCity.CurrentRow.Cells["CityID"].Value);
            cmbRegion.Text = dgvCity.CurrentRow.Cells["dtxtRegion"].Value.ToString();
            txtCityName.Text = dgvCity.CurrentRow.Cells["dtxtCityName"].Value.ToString();
        }

        private void GetCities()
        {
            try
            {
                dataGridView1.DataSource = _cityViewModel.GetCities();
                //foreach (var item in _cityView.GetCities())
                //{
                //    dgvCity.Rows.Add(item.CityID, item.RegionName, item.CityName);
                //}
            }
            catch (Exception ex)
            {
                MessageResult.LogErrors(ex.Message);
            }
        }

        private void GetRegions()
        {
            cmbRegion.DisplayMember = "RegionName";
            cmbRegion.ValueMember = "RegionID";
            cmbRegion.DataSource = _cityViewModel.GetRegionModels();
        }

        private void SetDataToPropierties()
        {
            _cityViewModel.CityID = _id;
            _cityViewModel.RegionID = Convert.ToInt32(cmbRegion.SelectedValue);
            _cityViewModel.CityName = txtCityName.Text;
        }

        private bool ValidationData(CityViewModel cityViewModel)
        {
            bool valid = new DataValidation(cityViewModel).Validate();

            if (valid == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ActiveControlleres()
        {
            ActionControls.ActiveControllers(this);

            btnNew.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            txtFindBy.ReadOnly = true;

            cmbRegion.Focus();
        }

        private void BlockControllers()
        {
            ActionControls.BlockControllers(this);

            btnNew.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            _index = -1;
            _id = -1;
        }
    }
}
