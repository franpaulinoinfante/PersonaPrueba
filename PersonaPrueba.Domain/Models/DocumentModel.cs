using PersonaPrueba.DataAccess.Repository.Entities;
using PersonaPrueba.DataAccess.Repository.Repositories;
using PersonaPrueba.Domain.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaPrueba.Domain.Models
{
    public class DocumentModel : Contracts.IDocumentOperation
    {
        private EntityState _state;
        private readonly DocumentEntity _documentEntity;
        private readonly DocumentRepository _documentRepository;

        public DocumentModel()
        {
            _documentEntity = new DocumentEntity();
            _documentRepository = new DocumentRepository();
        }

        public int DocumentID { get; set; }
        public string Document { get; set; }

        public EntityState State { set => _state = value; }

        public IEnumerable<DocumentModel> GetAll()
        {
            List<DocumentModel> documentModels = new List<DocumentModel>();

           var models = _documentRepository.GetAll();

            foreach (DocumentEntity model in models)
            {
                documentModels.Add(new DocumentModel
                {
                    DocumentID = model.DocumentID,
                    Document = model.Document
                });
            }

            return documentModels;
        }

        public string SaveChanges(DocumentModel model)
        {
            string message = string.Empty;

            try
            {
                DocumentEntity documentEntity = new DocumentEntity();
                documentEntity.DocumentID = model.DocumentID;
                documentEntity.Document = model.Document;

                switch (_state)
                {
                    case EntityState.Added:
                        DocumentID = _documentRepository.Add(documentEntity);
                        message =$"Successfully Record. \nThe new DocumentID is: {DocumentID}";
                        break;
                    case EntityState.Edited:
                        _documentRepository.Edit(documentEntity);
                        message = $"Successfully Edited.";
                        break;
                    case EntityState.Deleted:
                        _documentRepository.Delete(documentEntity);
                        message = $"Successfully deleted.";
                        break;
                    default:
                        message = "Select a operation";
                        break;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }
    }
}
