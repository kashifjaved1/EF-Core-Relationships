using MailboxApi.Data.Entities;

namespace EFCoreRelationships.Data.Entities.one_to_many
{
    public class Client : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
