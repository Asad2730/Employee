using Employee.Data;
using Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly AppDbContext db;

        public DepartmentController(AppDbContext db)
        {
            this.db = db;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAll()
        {
            try
            {  var q = await db.Departments.ToListAsync();
                return Ok(q);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Get(int id)
        {
            try
            {
                var q = await db.Departments.FirstOrDefaultAsync(i => i.Id == id);
                if (q == null)
                {
                    return NotFound("Department Not found");
                }

                return Ok(q);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }



        [HttpPost]
        public async Task<ActionResult<Department>> Create(Department department)
        {
            try
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = department.Id }, department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Department department)
        {
            try
            {
                if (id != department.Id)
                {
                    return NotFound("Department not found");
                }

                db.Entry(department).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok("Department updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var q = await db.Departments.FirstOrDefaultAsync(x => x.Id == id);
                if (q == null)
                {
                    return NotFound();
                }

                db.Departments.Remove(q);
                await db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
