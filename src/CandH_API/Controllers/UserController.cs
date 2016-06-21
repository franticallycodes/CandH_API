using CandH_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandH_API.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [EnableCors("AllowDevEnvironment")]
  public class UserController : Controller
  {
    private CandH_Context _context;

    public UserController(CandH_Context context)
    {
      _context = context;
    }

    [HttpGet(Name = "GetUsers")]
    // api/User?
    public IActionResult GET(int? userId)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IQueryable<ComicUser> users = _context.Reader;

      if (userId != null)
      {
        users = users.Where(user => user.ComicUserId == userId);
      }

      if (users == null)
      {
        return NotFound();
      }

      return Ok(users);
    }

    [HttpPost]
    public IActionResult POST([FromBody] ComicUser newUser)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IQueryable<ComicUser> checkForUserEmail = _context.Reader.Where(user => user.Email == newUser.Email);

      ComicUser userExists = checkForUserEmail.FirstOrDefault();
      string createdLocation = "http://localhost:5000/api/User?=";
      if (userExists != null)
      {
        // should actually return "Already Created", but I want it to return the user from the DB either way.
         createdLocation += userExists.ComicUserId;
        return Created(createdLocation, userExists);
      }

      _context.Reader.Add(newUser);

      try
      {
        _context.SaveChanges();
      }
      catch (DbUpdateException)
      {
        return new StatusCodeResult(StatusCodes.Status418ImATeapot);
      }

      createdLocation += newUser.ComicUserId;
      return Created(createdLocation, newUser);
    }
  }
}
