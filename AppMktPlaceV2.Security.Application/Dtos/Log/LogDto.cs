namespace AppMktPlaceV2.Security.Application.Dtos.Log
{
    public class LogDto
    {
        public Guid? Identifier { get; set; }

        public string Message { get; set; }

        public string Request { get; set; }

        public string Method { get; set; }

        public string? Response { get; set; }

        public Guid UserAdded { get; set; }

        public string DateAdded { get; set; }

        public string CurrentPayload { get; set; }

        public string PreviousPayload { get; set; }
    }
}
