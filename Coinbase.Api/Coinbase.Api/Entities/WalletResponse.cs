namespace Coinbase.Api.Entities
{
    public class WalletResponse
    {
        public string ObjectId { get; set; } = null!;

        public string RecoveryPhrase { get; set; } = null!;

        public int? OwnerId { get; set; }
    }
}
