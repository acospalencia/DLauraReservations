using DLaura.Entities;
using Mapster;

namespace DLaura.BusinessLogic.DTOs.Mappings
{
    public class MappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config) {

            config.NewConfig<User, UserResponse>()
                .Map(ud => ud.RoleName, u => u.Rol.RoleName);
        
        }
    }
}
