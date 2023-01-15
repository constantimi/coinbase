using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coinbase.Api.Entities
{
    public class Wallet
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ObjectId { get; set; } = null!;

        [Required]
        public string RecoveryPhrase { get; set; } = null!;

        [Required]
        [ForeignKey("ExternalId")]
        public int? OwnerId { get; set; }
    }
}
