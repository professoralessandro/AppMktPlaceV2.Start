namespace AppMktPlaceV2.Security.Application.Models.Dto.Autenticate
{
    public class AuthenticateRequest
    {
        public string Password { get; set; }

        public string? Email { get; set; }
    }
}
