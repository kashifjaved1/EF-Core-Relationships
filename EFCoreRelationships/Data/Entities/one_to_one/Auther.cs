using MailboxApi.Data.Entities;

namespace EFCoreRelationships.Data.Entities.one_to_one
{
    // Principal (parent)
    public class Author : Entity
    {
        public string Name { get; set; }

        public virtual Biography? Biography { get; set; } // Reference navigation to dependents. Using Nullable(?) to make this property optional for author. Can use IsRequired(false) in fluent api (modelBuilder) instead of Nullable(?).
    }
}
