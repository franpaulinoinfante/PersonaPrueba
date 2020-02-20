using PersonaPrueba.Domain.Models;
using PersonaPrueba.Domain.ObjectValues;
using PersonaPrueba.Views.ViewHelps;
using PersonaPrueba.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonaPrueba.Views.Views
{
    public partial class DocumentView : Form
    {
        private  DocumentModel _documentModel;
        private  DocumentViewModel _documentViewModel;

        private int _index;
        private int _id;

        public DocumentView()
        {
            InitializeComponent();
            _documentModel = new DocumentModel();
            _documentViewModel = new DocumentViewModel(_documentModel);
        }

        private void DocumentView_Load(object sender, EventArgs e)
        {
            GetDocument();
        }

        private void GetDocument()
        {
            try
            {
                dgvDocument.DataSource = _documentViewModel.GetDocuments();
            }
            catch (Exception ex)
            {
                MessageResult.LogErrors(ex.Message);
            }
        }

        private void btnSalve_Click(object sender, EventArgs e)
        {
            if (DialogConfirm.GetResult("Do you want save the changes?", "Question").Equals(DialogResult.No))
            {
                return;
            }

            SetDataToPropierties();

            if (ValidationData(_documentViewModel) == false)
            {
                return;
            }

            MessageResult.ShowResults(_documentViewModel.SaveChanges());

            BlockControllers();

            RefreshControllers()
;
            GetDocument();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ActionControls.RefreshControllers(this);

            ActiveControllers();

            _documentModel.State = EntityState.Added;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_index == -1)
            {
                MessageResult.LogErrors("Select a Document");
                return;
            }

            ActiveControllers();

            _documentModel.State = EntityState.Edited;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_id == -1)
            {
                MessageResult.ShowResults("Select a Document");
                return;
            }

            _documentModel.State = EntityState.Deleted;

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

        private void dgvDocument_DoubleClick(object sender, EventArgs e)
        {
            _index = Convert.ToInt32(dgvDocument.CurrentRow.Index);
            _id = Convert.ToInt32(dgvDocument.CurrentRow.Cells["DocumentID"].Value);
            txtDocument.Text = dgvDocument.CurrentRow.Cells["Document"].Value.ToString().Trim();
        }



        // ---------------------------- METHODS ----------------------------------
        private void SetDataToPropierties()
        {
            _documentViewModel.DocumentID = _id;
            _documentViewModel.Document = txtDocument.Text;


            //if (string.IsNullOrWhiteSpace(txtDocument.Text))
            //{
            //    MessageResult.LogErrors("No White space or null, write something");
            //}
        }

        private bool ValidationData(DocumentViewModel documentViewModel)
        {
            bool valid = new DataValidation(documentViewModel).Validate();

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

            txtDocument.Focus();
        }

        private void BlockControllers()
        {
            ActionControls.BlockControllers(this);

            btnNew.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            txtFindBy.ReadOnly = false;

            _index = -1;
            _id = -1;
        }

        private void RefreshControllers()
        {
            ActionControls.RefreshControllers(this);

            _index = -1;
            _id = -1;
        }
    }
}
