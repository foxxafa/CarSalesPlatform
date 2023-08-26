namespace Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentication // dış kaynaktan gelen login
    {
        //Task<DTOs.Token.Token> GoogleLoginAsync(string Credential,int accessTokenLifeTime);

        //Twitter , facebook ... gibi login işlemleri gelebilir
    }
}
