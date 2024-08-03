#region REFERENCES
using AppMktPlaceV2.Security.Application.Enums;
#endregion REFERENCES

namespace AppMktPlaceV2.Security.Application.Models.Dto.User
{
    public class UserDto
    {
        public Guid? Identifier { get; set; }

        public string NmrDocumento { get; set; }

        public TipoDocumentoEnum TipoDocumento { get; set; }

        public string Password { get; set; }

        public string Nome { get; set; }

        public string UserName { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Sexo { get; set; }

        public string EstadoCivil { get; set; }

        public string Email { get; set; }

        public bool Bloqueado { get; set; }

        public Guid UsuarioInclusaoId { get; set; }

        public Guid? UsuarioUltimaAlteracaoId { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataUltimaAlteracao { get; set; }

        public DateTime? DataUltimaTrocaSenha { get; set; }

        public DateTime? DataUltimoLogin { get; set; }

        public string? NmrTelefone { get; set; }

        public bool? TrocaSenha { get; set; }

        public bool Ativo { get; set; }

        public string? Token { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
