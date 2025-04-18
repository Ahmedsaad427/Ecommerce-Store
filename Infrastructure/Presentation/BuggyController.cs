using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")] // GET: api/buggy/notfound
        public ActionResult GetNotFound()
        {
            return NotFound(); // 404 Not Found
        }

        [HttpGet("servererror")] // GET: api/buggy/servererror
        public ActionResult GetServerError()
        {
            throw new Exception("This is a simulated server error."); // 500 Internal Server Error
        }

        [HttpGet("badrequest")] // GET: api/buggy/badrequest
        public ActionResult GetBadRequestSimple()
        {
            return BadRequest("This is a bad request."); // 400 Bad Request
        }

        [HttpGet("badrequest/{id}")] // GET: api/buggy/badrequest/5
        public ActionResult GetBadRequestWithId(int id)
        {
            return BadRequest($"This is a bad request with id: {id}"); // 400 Bad Request
        }

        [HttpGet("unauthorized")] // GET: api/buggy/unauthorized
        public ActionResult GetUnauthorized()
        {
            return Unauthorized(); // 401 Unauthorized
        }

        [HttpGet("forbidden")] // GET: api/buggy/forbidden
        public ActionResult GetForbidden()
        {
            return Forbid(); // 403 Forbidden
        }
    }
}
