using AppMktPlaceV2.Security.Application.Enums;

namespace AppMktPlaceV2.Security.Application.Dtos.User.Request
{
    public class UserUpdateRequest
    {
        public Guid? Identifier { get; set; }

        public string Nome { get; set; }

        public string Sexo { get; set; }

        public string EstadoCivil { get; set; }

        public string? NmrTelefone { get; set; }

        public string? Role { get; set; }
    }
}
