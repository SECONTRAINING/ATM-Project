using AutoMapper;
using DataObjectLayer;

namespace BusinessLogicLayer
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CustomerAccount,CustomerAccountView>().ReverseMap();
            CreateMap<LoginCredential,LoginCredentialView>().ReverseMap();
            CreateMap<BranchDetail,BranchDetailView>().ReverseMap();
        }
    }
}
