using EFCoreRelationships.Data.Entities.many_to_many;
using MailboxApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Controllers.many_to_many
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public StudentsController(ApiDbContext context)
        {
            _context = context;
        }

        // The Joint/Join Entity way
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Student>>> GetStuents()
        //{
        //    return await _context.Students.Include(t => t.TeacherStudents).ThenInclude(ts => ts.Teacher).ToListAsync();
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.Include(s => s.Teachers).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.Include(s => s.Teachers).FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("AddTeachers/{studentId}")]
        public async Task<ActionResult> AddTeachersToStudent(int studentId, [FromBody] List<int> teacherIds)
        {
            var student = await _context.Students.FindAsync(studentId);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            if (student.Teachers == null)
            {
                student.Teachers = new List<Teacher>();
            }

            var teachersToAdd = await _context.Teachers.Where(t => teacherIds.Contains(t.Id)).ToListAsync();

            foreach (var teacher in teachersToAdd)
            {
                student.Teachers.Add(teacher);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }

}
