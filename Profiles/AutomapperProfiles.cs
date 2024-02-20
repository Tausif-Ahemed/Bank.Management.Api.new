using AutoMapper;
using Bank.Management.Api.Models;

namespace Bank.Management.Api;

public class AutomapperProfiles : Profile
{
    // Note :- we use AutoMapper because we dont want to expose some sesitive field to user
    public AutomapperProfiles()
    {
        CreateMap<RegisterNewAccountModel, Account>();
    
        CreateMap<UpdateAccountModel, Account>();
    
        CreateMap<Account, GetAccountModel>();
    }
    //we will create for dto classes 
}
