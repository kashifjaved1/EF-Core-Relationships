using MailboxApi.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace EFCoreRelationships.Data.Entities.one_to_one
{

    // Dependent (child)
    public class Biography : Entity
    {
        public string Content { get; set; }

        public int AuthorId { get; set; } // Required foreign key property
        public Author? Author { get; set; } // Required reference navigation to principal
    }
}
