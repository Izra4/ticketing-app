using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using NuGet.Common;
using Ticketing.Data;
using Ticketing.Dtos.User;
using Ticketing.Helper;
using Ticketing.Models;

namespace Ticketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<UsersController> _logger;

        public UsersController(AppDBContext context, IConfiguration config, ILogger<UsersController> logger)
        {
            _context = context;
            _config = config;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto regDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == regDto.Email))
                return BadRequest(new ApiResponse<object>
                {
                    Code = 400,
                    Status = "error",
                    Data = null,
                    Message = "email is already registered"
                });

            var user = new User
            {
                Name = regDto.Name,
                Email = regDto.Email,
                Phone = regDto.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(regDto.Password),
                Role = regDto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUser), new { id = user.Id },
                new ApiResponse<object>
                {
                    Code = 201,
                    Status = "success",
                    Data = new User
                    {
                        Id = user.Id,
                        Name = regDto.Name,
                        Email = regDto.Email,
                        Phone = regDto.Phone,
                        Role = user.Role
                    },
                    Message = "register success"
                });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                return Unauthorized(new ApiResponse<object>
                {
                    Code = 401,
                    Status = "error",
                    Data = null,
                    Message = "invalid user / password"
                });

            var token = JwtToken.JwtTokenGenerator(_config, user.Id, user.Email);

            return Ok(new ApiResponse<object>
            {
                Code = 200,
                Status = "error",
                Data = new { Token = token},
                Message = "login success"
            });
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userIdStr = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var email = User.FindFirstValue(JwtRegisteredClaimNames.Email);

            if (userIdStr == null || email == null)
                return Unauthorized(new ApiResponse<object>
                {
                    Code = 401,
                    Status = "error",
                    Data = null,
                    Message = "unauthorized"
                });

            Guid userId = Guid.Parse(userIdStr);

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new ApiResponse<object>
                {
                    Code = 404,
                    Status = "error",
                    Data = null,
                    Message = "user not found"
                });

            return Ok(new ApiResponse<object>
            {
                Code = 200,
                Status = "success",
                Data = user,
                Message = "get profile success"
            });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
