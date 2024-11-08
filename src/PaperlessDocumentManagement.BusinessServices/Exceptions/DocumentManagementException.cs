namespace PaperlessDocumentManagement.BusinessServices.Exceptions
{
    public class DocumentManagementException : Exception
    {
        public string ErrorCode { get; }

        public DocumentManagementException(string message, string errorCode = "DOCUMENT_ERROR") 
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public DocumentManagementException(string message, Exception innerException, string errorCode = "DOCUMENT_ERROR") 
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }

    public class DocumentNotFoundException : DocumentManagementException
    {
        public DocumentNotFoundException(Guid id) 
            : base($"Document with ID {id} was not found", "DOCUMENT_NOT_FOUND")
        {
        }
    }
}
