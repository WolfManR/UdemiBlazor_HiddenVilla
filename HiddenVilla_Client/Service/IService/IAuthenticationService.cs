using Models;

using System.Threading.Tasks;

namespace HiddenVilla_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegistration);
        Task<AuthenticationResponseDTO> Login(AuthenticationDTO userForAuthentication);
        Task Logout();
    }
}
