namespace Coinbase.Api.Entities
{
    public class WalletRequest
    {
        public string ObjectId { get; set; } = null!;

        public string RecoveryPhrase { get; set; } = null!;
    }
}
