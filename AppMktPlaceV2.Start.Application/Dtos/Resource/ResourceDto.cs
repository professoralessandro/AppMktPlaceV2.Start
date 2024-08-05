namespace AppMktPlaceV2.Start.Application.Dtos.Resource
{
    public class ResourceDto
    {
        public Guid? Identifier { get; set; }

        public string Nome { get; set; }

        public string Chave { get; set; }

        public string ToolTip { get; set; }

        public string Route { get; set; }

        public bool Menu { get; set; }

        public Guid? RecursoIdPai { get; set; }

        public int? Ordem { get; set; }

        public bool Ativo { get; set; }

        public string Type { get; set; }

        public string Icon { get; set; }

        public string Path { get; set; }

        public bool IsSubMenu { get; set; }
    }
}
