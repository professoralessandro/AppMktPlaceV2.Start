using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace AppMktPlaceV2.Security.Domain.Entities
{
    [Table("log", Schema = "public")]
    public partial class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid logid { get; set; }

        public string message { get; set; }

        public string request { get; set; }

        public string method { get; set; }

        public string? response { get; set; }

        public Guid useradded { get; set; }

        public string dateadded { get; set; }

        public string currentpayload { get; set; }

        public string previouspayload { get; set; }
    }
}
