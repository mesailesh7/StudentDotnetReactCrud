using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    //this api route can also be hardcoded like [Route("api/people")] 
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        //All the endpoints
        // POST /api/student {body}
        // GET /api/student
        // GET /api/student/2
        // PUT /api/student/2 {body}
        // DELETE /api/student/2
        private readonly AppDbContext _context;

        // In this context is the database connection
        //setter injection which is the part of dependency injection
        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost] //Post /api/people
        public async Task<IActionResult> AddPerson(Student person)
        {
            try
            {
                // At this points of time data is added to the context(context is like a cached data in the memory) but its not added to the database
                _context.Students.Add(person);
                //saving the changes to the database
                await _context.SaveChangesAsync();
                return CreatedAtRoute("GetStudent", new { id = person.Id }, person); // this will return 201 (Created status code) ok status code + person object in the body ** 201 created status code + location of the resource (http://localhost:3000/api/people/{id}) + person object in the response body

                // Example response from the api
                //                 HTTP/1.1 201 Created
                // Connection: close    
                // Content-Type: application/json; charset=utf-8
                // Date: Sat, 20 Sep 2025 19:27:49 GMT
                // Server: Kestrel
                // Location: http://localhost:3000/api/People/3
                // Transfer-Encoding: chunked

                // {
                //   "id": 3,
                //   "firstName": "Sun",
                //   "lastName": "T"
                // }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            ; // 500 internal server error + message in the body response body
        }

        [HttpGet] //GET /api/people
        public async Task<IActionResult> GetPeople()
        {
            try
            {
                // At this points of time data is added to the context(context is like a cached data in the memory) but its not added to the database
                var people = await _context.Students.ToListAsync();
                //if we want to select only certain people
                // var people = await _context.People.select(c => new {
                // c.id, c.firstName,
                // }).ToListAsync();

                //saving the changes to the database
                return Ok(people); // this will return 200 ok status code + person object in the body

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           ; // 500 internal server error + message in the body response body
        }


        [HttpGet("{id:int}", Name = "GetStudent")] //GET /api/people/1
        public async Task<IActionResult> GetPerson(int id)
        {
            try
            {
                // At this points of time data is added to the context(context is like a cached data in the memory) but its not added to the database
                var person = await _context.Students.FindAsync(id);

                if (person is null)
                {
                    return NotFound(); // 404 not found status code
                }
                //if we want to select only certain people
                // var people = await _context.People.select(c => new {
                // c.id, c.firstName,
                // }).ToListAsync();

                //saving the changes to the database
                return Ok(person); // this will return 200 ok status code + person object in the body

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
                ; // 500 internal server error + message in the body response body
        }


        [HttpPut("{Id:int}")] //Put /api/people/1
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] Student person)
        {
            try
            {

                if (id != person.Id)
                {
                    return BadRequest("Id in url and body mismatches"); //400 + message in body
                }

                if (!await _context.Students.AnyAsync(p => p.Id == id))
                {
                    return NotFound();
                }
                // At this points of time data is added to the context(context is like a cached data in the memory) but its not added to the database
                _context.Students.Update(person);
                //saving the changes to the database
                await _context.SaveChangesAsync();
                return NoContent(); //204 status code
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            // 500 internal server error + message in the body response body
        }


        [HttpDelete("{id:int}")] //Delete /api/people/1
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var person = await _context.Students.FindAsync(id);

                if (person is null)
                {
                    return NotFound();
                }

                _context.Students.Remove(person);

                // At this points of time data is added to the context(context is like a cached data in the memory) but its not added to the database

                //saving the changes to the database
                await _context.SaveChangesAsync();
                return NoContent(); //204 status code
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            // 500 internal server error + message in the body response body
        }



    }


}

//CORS error (Cross origin resource sharing)
//server app : http://localhost:3000
//client app (js app) : http://localhost:5173