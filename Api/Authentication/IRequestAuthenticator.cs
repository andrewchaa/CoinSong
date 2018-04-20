namespace GdaxApi.Authentication
{
    public interface IRequestAuthenticator
    {
        AuthenticationToken GetAuthenticationToken(ApiRequest request);
    }
}
