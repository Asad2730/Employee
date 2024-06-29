using Employee.Data;
using Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext db;

        public UserController(AppDbContext db) {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {   
                var q = await db.Users.Include(e => e.Department.Name).ToListAsync();
                return Ok(q);
            }
            catch (Exception ex) { 

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                var q = await db.Users.Include(i => i.Department.Name).FirstOrDefaultAsync(i => i.Id == id);
                if (q == null)
                {
                    return NotFound("User Not found");
                }

                return Ok(q);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }



        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            try
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return CreatedAtAction(nameof(Get),new {id =  user.Id},user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, User user)
        {
            try
            {  
                if( id != user.Id)
                {
                    return NotFound("User not found");
                }

                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok("User updated");
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
               var q = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
                if(q == null)
                {
                    return NotFound();
                }

                db.Users.Remove(q);
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
