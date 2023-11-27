using MailboxApi.Data.Entities;

namespace EFCoreRelationships.Data.Entities.one_to_many
{
    public class Order : Entity
    {
        public string ProductName { get; set; }
        public decimal Amount { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }

}
