using System;
using System.Windows.Forms;
using PersonaPrueba.Domain.Contracts;
using PersonaPrueba.Domain.Models;
using PersonaPrueba.Domain.ObjectValues;
using PersonaPrueba.Views.ViewHelps;
using PersonaPrueba.Views.ViewModels;

namespace PersonaPrueba.Views.Views
{
    public partial class CityView : Form
    {
        //private ICity _cityModel = new CityModel();
        //private IRegion _RegionModel = new RegionModel();

        private CityViewModel _cityView;
        private EntityState _state;
        private int _index = -1;
        private int _id = -1;

        public CityView()
        {
            InitializeComponent();

            _cityView = new CityViewModel();
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

            _cityView.CityID = _id;
            _cityView.RegionID = Convert.ToInt32(cmbRegion.SelectedValue);
            _cityView.CityName = txtCityName.Text;

            _cityView.SetDataToPropierties();

            MessageResult.ShowResults(_cityView.SaveChanges());

            _cityView.SaveChanges();

            BlockControlleres();

            ActionControls.RefreshControllers(this);

            GetCities();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ActiveControlleres();
            
            _state = EntityState.Added;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_index.Equals(-1))
            {
                MessageResult.LogErrors("Select a city");
                return;
            }

            ActiveControlleres();

            _state = EntityState.Edited;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_id.Equals(-1))
            {
                MessageResult.LogErrors("Select a city");
                return;
            }

            _state = EntityState.Deleted;

            btnSalve_Click(this, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (DialogConfirm.GetResult("Do you want cancel the changes", "Question").Equals(DialogResult.No))
            {
                return;
            }

            ActionControls.RefreshControllers(this);

            BlockControlleres();
        }

        private void dgvCity_DoubleClick(object sender, EventArgs e)
        {
            _index = Convert.ToInt32(dgvCity.CurrentRow.Index);
            _id = Convert.ToInt32(dgvCity.CurrentRow.Cells["CityID"].Value);
            cmbRegion.Text = dgvCity.CurrentRow.Cells["dtxtRegion"].Value.ToString();
            txtCityName.Text = dgvCity.CurrentRow.Cells["dtxtCity"].Value.ToString();
        }

        private void GetCities()
        {
            dgvCity.Rows.Clear();

            try
            {
                foreach (var city in _cityView.GetCities())
                {
                    dgvCity.Rows.Add(city.CityID, city.RegionName, city.CityName);
                }
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
            cmbRegion.DataSource = _cityView.GetRegionModels();
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

        private void BlockControlleres()
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
