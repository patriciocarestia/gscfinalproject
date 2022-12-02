using GSC_FinalProject.Dto;
using GSC_FinalProject.Entities;

namespace GSC_FinalProject.Handlers
{
    public interface IJwtHandler
    {
        string GenerateToken(UserDTO user, Role roles);
    }
}
