using CandH_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandH_API.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [EnableCors("AllowDevEnvironment")]
  public class EmotionController : Controller
  {
    private CandH_Context _context;

    public EmotionController(CandH_Context context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult GET()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IQueryable<ComicStripEmotion> distinctEmotions = _context.Emotion.Distinct();

      if (distinctEmotions == null)
      {
        return NotFound();
      }

      return Ok(distinctEmotions);
    }
  }
}
