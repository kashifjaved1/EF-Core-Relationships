using MailboxApi.Data.Entities;

namespace EFCoreRelationships.Data.Entities.many_to_many
{
    public class Teacher : Entity
    {
        public string Name { get; set; }

        public virtual List<Student>? Students { get; set; } 

        //public virtual List<TeacherStudent>? TeacherStudents { get; set; }
    }
}
