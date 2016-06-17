using CandH_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CandH_API.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [EnableCors("AllowDevEnvironment")]
  public class ComicStripController : Controller
  {
    private CandH_Context _context;

    public ComicStripController(CandH_Context context)
    {
      _context = context;
    }

    [HttpGet(Name = "GetComicStrips")]
    // api/ComicStrip?comicStripId={1}
    public IActionResult GET(int? comicStripId)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IQueryable<ComicStrip> comicStrips = _context.Strip;

      if (comicStripId == null)
      {
        // insert random number if argument parameter is not provided. 1 for now
        comicStripId = 1;
      }
      comicStrips.Where(comic => comic.ComicStripId == comicStripId);

      if (comicStrips == null)
      {
        return NotFound();
      }

      return Ok(comicStrips);
    }

    [HttpPost]
    public IActionResult POST([FromBody] ComicStrip newComicStrip)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Strip.Add(newComicStrip);

      try
      {
        _context.SaveChanges();
      }
      catch (DbUpdateException)
      {

        return new StatusCodeResult(StatusCodes.Status418ImATeapot);
      }

      // research this more
      string createdLocation = "http://localhost:5000/api/ComicStrip?stripId=" + newComicStrip.ComicStripId;
      return Created(createdLocation, newComicStrip);
    }

    // Do I need to PUT specifically to this path (/api/ComicStrip/{value}) or is the below just fine?
    [HttpPut("{id}")]
    public IActionResult PUT(int id, [FromBody]ComicStrip comicToUpdate)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      //ComicStrip comic = _context.Strip.Single(com => com.ComicStripId == id);

      //comicToUpdate.ComicStripId = comic.ComicStripId;

      _context.Entry(comicToUpdate).State = EntityState.Modified;

      try
      {
        _context.SaveChanges();
      }
      catch (DbUpdateException)
      {

        return new StatusCodeResult(StatusCodes.Status418ImATeapot);
      }

      string createdLocation = "http://localhost:5000/api/ComicStrip?stripId=" + comicToUpdate.ComicStripId;
      return Created(createdLocation, comicToUpdate);
    }
  }
}
