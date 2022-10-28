namespace FoodHub_Web_API.Helpers
{
    /// <summary>
    /// Using CreateMap() to make custom maps from an object to another object. AutoMapper
    /// </summary>
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Account, DirectAccountResponse>();
            CreateMap<Account, StaticAccountResponse>();
            CreateMap<AccountRequest, Account>();

            CreateMap<Customer, DirectCustomerResponse>();
            CreateMap<Customer, StaticCustomerResponse>();
            CreateMap<CustomerRequest, Customer>();

            CreateMap<AuthenticationResponse, StaticRefreshTokenResponse>();
        }
    }
}
