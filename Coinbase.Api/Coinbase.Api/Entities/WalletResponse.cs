namespace Coinbase.Api.Entities
{
    public class WalletResponse
    {
        public string ObjectId { get; set; } = null!;

        public string RecoveryPhrase { get; set; } = null!;

        public string BlockchainType { get; set; } = null!;

        public string WalletType { get; set; } = null!;

        public int OwnerId { get; set; }
    }
}
