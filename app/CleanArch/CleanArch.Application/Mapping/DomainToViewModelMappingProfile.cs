using AutoMapper;
using CleanArch.Domain.Entities;
using CleanArch.Application.ViewModels;

namespace CleanArch.Application.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}