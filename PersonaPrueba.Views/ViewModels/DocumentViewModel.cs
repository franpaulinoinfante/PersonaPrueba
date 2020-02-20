using PersonaPrueba.Domain.Contracts;
using PersonaPrueba.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaPrueba.Views.ViewModels
{
    public class DocumentViewModel
    {
        private readonly IDocumentOperation _documentOperation;

        public DocumentViewModel(IDocumentOperation documentOperation)
        {
            _documentOperation = documentOperation;
        }

        [Required]
        public int DocumentID { get; set; }

        [Required(ErrorMessage = "The field document's number is requerid")]
        [RegularExpression("^[a-z A-Z]+$")]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Document { get; set; }

        public IEnumerable<DocumentViewModel> GetDocuments()
        {
            List<DocumentViewModel> documentViewModels = new List<DocumentViewModel>();

            var models = _documentOperation.GetAll();

            foreach (DocumentModel documentModel in models)
            {
                var addDocument = new DocumentViewModel(_documentOperation)
                {
                    DocumentID = documentModel.DocumentID,
                    Document = documentModel.Document
                };

                documentViewModels.Add(addDocument);
            }

            return documentViewModels;
        }

        public string SaveChanges()
        {
            DocumentModel model = new DocumentModel
            {
                DocumentID=DocumentID,
                Document = Document
            };

            return _documentOperation.SaveChanges(model);
        }
    }
}
