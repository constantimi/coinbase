using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coinbase.Api.Entities
{
    public class Wallet
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string ObjectId { get; set; } = null!;

        [Required]
        public string RecoveryPhrase { get; set; } = null!;

        [Required]
        [ForeignKey("ExternalId")]
        public int OwnerId { get; set; }
    }
}
