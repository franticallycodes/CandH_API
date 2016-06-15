﻿using CandH_API.Models;
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
  public class ComicStripController : Controller
  {
    private CandH_Context _context;

    public ComicStripController(CandH_Context context)
    {
      _context = context;
    }

    [HttpGet(Name = "GetComicStrips")]
    public IActionResult GET()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IQueryable<ComicStrip> comicStrips = _context.Strip;

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
  }
}