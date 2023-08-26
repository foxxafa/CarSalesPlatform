using Application.Abstractions.Services.Authentications;

namespace Application.Abstractions.Services
{
    public interface IAuthService :IExternalAuthentication,IInternalAuthentication
    {

    }
}
