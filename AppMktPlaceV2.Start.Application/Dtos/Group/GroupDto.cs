namespace AppMktPlaceV2.Start.Application.Dtos.Group
{
    public class GroupDto
    {
        public Guid? Identifier { get; set; }

        public string GroupName { get; set; }

        public Guid UsuarioInclusaoId { get; set; }

        public Guid? UsuarioUltimaAlteracaoId { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataUltimaAlteracao { get; set; }

        public bool Ativo { get; set; }
    }
}
