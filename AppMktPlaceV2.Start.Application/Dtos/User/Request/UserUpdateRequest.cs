using AppMktPlaceV2.Start.Application.Enums;

namespace AppMktPlaceV2.Start.Application.Dtos.User.Request
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
