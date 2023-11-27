using EFCoreRelationships.Data.Entities.many_to_many;
using EFCoreRelationships.Data.Entities.one_to_many;
using EFCoreRelationships.Data.Entities.one_to_one;
using Microsoft.EntityFrameworkCore;

namespace MailboxApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        #region 1 - 1
        public DbSet<Author> Authors { get; set; }
        public DbSet<Biography> Biographies { get; set; }
        #endregion

        #region 1 - m
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        #endregion

        #region m - m
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        //public DbSet<TeacherStudent> TeacherStudents { get; set; } // Join/Joint Entity for Many-Many
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region 1 - 1
            modelBuilder
                .Entity<Author>()
                    .HasOne(a => a.Biography)
                    .WithOne(b => b.Author)
                    .HasForeignKey<Biography>(b => b.AuthorId)
                    /*.IsRequired(false)*/;
            #endregion

            #region 1 - m
            modelBuilder.Entity<Client>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Client)
            .HasForeignKey(o => o.ClientId);
            #endregion

            #region m - m
            // Configure Many-to-Many relationship without a join entity
            modelBuilder
                .Entity<Teacher>()
                .HasMany(t => t.Students)
                .WithMany(s => s.Teachers)
                .UsingEntity(j => j.ToTable("TeacherStudent"));                

            // Configure Many-to-Many relationship with a join entity
            //modelBuilder.Entity<TeacherStudent>()
            //.HasKey(ts => new { ts.TeacherId, ts.StudentId });

            //modelBuilder.Entity<TeacherStudent>()
            //    .HasOne(ts => ts.Teacher)
            //    .WithMany(t => t.TeacherStudents)
            //    .HasForeignKey(ts => ts.TeacherId);

            //modelBuilder.Entity<TeacherStudent>()
            //    .HasOne(ts => ts.Student)
            //    .WithMany(s => s.TeacherStudents)
            //    .HasForeignKey(ts => ts.StudentId);
            #endregion


        }
    }
}
