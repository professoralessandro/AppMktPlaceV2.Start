#region REFERENCES
using AppMktPlaceV2.Security.Application.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion REFERENCES

namespace AppMktPlaceV2.Security.Domain.Entities
{
    [Table("Usuarios", Schema = "seg")]
    public partial class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid? UsuarioId { get; set; }

        public Guid? GrupoId { get; set; }

        public string? NmrDocumento { get; set; }

        public string NmrTelefone { get; set; }

        public TipoDocumentoEnum TipoDocumentoId { get; set; }

        public string Senha { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

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

        public bool Ativo { get; set; }

        public bool TrocaSenha { get; set; }

        public string? Token { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
