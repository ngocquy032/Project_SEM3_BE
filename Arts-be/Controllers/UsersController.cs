﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arts_be.Models;
using Arts_be.Models.DTO;

namespace Arts_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EProjectContext _context;

        public UsersController(EProjectContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // POST: api/Users/Login
        [HttpPost("LoginUsers")]
        public async Task<ActionResult<User>> LoginUser(LoginUserDTO loginUserDTO)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'UsersContext.Users' is null.");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginUserDTO.email && u.Password == loginUserDTO.password);

            if (user == null)
            {
                return NotFound();
            }
            // Kiểm tra level của người dùng
            if (user.Level != "user")
            {
                // Nếu level không phải là "user", trả về lỗi hoặc thực hiện xử lý phù hợp
                return BadRequest("Access denied. Only users with level 'user' are allowed.");
            }

            return user;
        }

        // POST: api/Users/Login
        [HttpPost("LoginAdmin")]
        public async Task<ActionResult<User>> LoginAdmin(LoginAdminDTO loginAdminDTO)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'UsersContext.Users' is null.");
            }

            var admin = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginAdminDTO.email && u.Password == loginAdminDTO.password);

            if (admin == null)
            {
                return NotFound();
            }
            // Kiểm tra level của người dùng
            if (admin.Level != "admin")
            {
                // Nếu level không phải là "user", trả về lỗi hoặc thực hiện xử lý phù hợp
                return BadRequest("Access denied. Only users with level 'user' are allowed.");
            }

            return admin;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'EProjectContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
