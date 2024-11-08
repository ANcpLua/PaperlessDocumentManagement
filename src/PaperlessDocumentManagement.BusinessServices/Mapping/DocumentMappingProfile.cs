using AutoMapper;
using PaperlessDocumentManagement.BusinessServices.DTOs;
using PaperlessDocumentManagement.DataAccessLayer.Entities;

namespace PaperlessDocumentManagement.BusinessServices.Mapping
{
    public class DocumentMappingProfile : Profile
    {
        public DocumentMappingProfile()
        {
            CreateMap<Document, DocumentDto>()
                .ForMember(dest => dest.Status, 
                    opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Tags, 
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)));
        }
    }
}
