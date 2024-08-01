using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Interfaces;
using Project1.Models;

using System.Collections;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RegisterAPIDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsersController> _logger;

        //private readonly IEmailStructureBuilder _emailStructure;

        public UsersController(RegisterAPIDbContext context, IUserRepository userRepository,
           ILogger<UsersController> logger)
        {
            _context = context;
            _userRepository = userRepository;
            _logger = logger;

           // _emailStructure = emailStructure;

        }

        //[Route("Users")]
        //public IActionResult Users()
        //{
        //    return Ok(_context.Registration.ToList());
        //}
        //[Route("Login")]
        //public ActionResult Login(string email, string password)//([FromBody] User user)
        //{
        //    Hashtable err = new Hashtable();
        //    try
        //    {
        //        var result = _context.Registration.Where(x => x.email.Equals(email) && x.password.Equals(password)).FirstOrDefault();
        //        if (result != null) return Ok(result);
        //        else
        //        {
        //            err.Add("Status", "Error");
        //            err.Add("Message", "Invalid Credentials");
        //            return Ok(err);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }        
        //}

        // GET: api/Registrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
            // return await _userRepository.GetUsers();

        }

        [HttpGet("{email}/{password}")]
        public async Task<ActionResult<User>> GetUser(string email, string password)
        {
            Hashtable err = new Hashtable();
            try
            {
                var authUser = await _userRepository.GetUser(email, password);
                if (authUser != null)
                {
                    return Ok(authUser);
                }
                else
                {
                    err.Add("Status", "Error");

                    err.Add("Message", "Invalid Credentials");

                    return Ok(err);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        // GET: api/Registrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                return await _userRepository.GetUser(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }
        // GET: api/users/current
        [HttpGet("current")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var email = User.Identity.Name;
            var currentUser = await _userRepository.GetUserByEmail(email);
            if (currentUser == null)
            {
                return NotFound();
            }
            return Ok(currentUser);
        }

        // PUT: api/Registrations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, User users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }
            _context.Entry(users).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Registrations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody]User users)
        {
            await _userRepository.AddUser(users);

          //  _emailStructure.SetMailBody(users.Email.Split("@").First(), MailType.Activation);
          //  _emailStructure.mailToAddress = users.Email;

          //  var _mailSender = new MailSender(_emailStructure);


           // _mailSender.sendMail();

            return CreatedAtAction("GetUser", new { id = users.Id }, users);
        }


        // DELETE: api/Registrations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUsers(int id)
        {
            try
            {
                return await _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            //var registration = await _context.Registration.FindAsync(id);
            //if (registration == null)
            //{
            //    return NotFound();
            //}

            //_context.Registration.Remove(registration);
            //await _context.SaveChangesAsync();

            //return registration;
        }

        private bool UsersExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}