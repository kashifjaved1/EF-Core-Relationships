using MailboxApi.Data.Entities;

namespace EFCoreRelationships.Data.Entities.many_to_many
{
    public class Student : Entity
    {
        public string Name { get; set; }

        // Used virtual for enabling lazy loading on 'TeacherStudent' navigation property
        // Used List over IList because maybe I need to use functionalities that're are implemented beyond IList.
        public virtual List<Teacher>? Teachers { get; set; } // Configuring without creating join/joint entity in codeFirst, but joint table will be create in any case.

        //public virtual List<TeacherStudent>? TeacherStudents { get; set; } // Configuring by creating a join/joint entity in codeFirst.
    }
}
