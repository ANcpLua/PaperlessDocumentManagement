using FluentValidation;
using PaperlessDocumentManagement.BusinessServices.DTOs;

namespace PaperlessDocumentManagement.BusinessServices.Validators
{
    public class CreateDocumentValidator : AbstractValidator<CreateDocumentDto>
    {
        public CreateDocumentValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("Title is required and must not exceed 255 characters");

            RuleFor(x => x.FileId)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("FileId is required and must not exceed 100 characters");

            RuleFor(x => x.ContentType)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("ContentType is required and must not exceed 100 characters");

            RuleFor(x => x.FileSize)
                .GreaterThan(0)
                .WithMessage("FileSize must be greater than 0");

            RuleFor(x => x.Metadata)
                .Must(x => x == null || x.Count <= 20)
                .WithMessage("Maximum of 20 metadata key-value pairs allowed");
        }
    }
}
