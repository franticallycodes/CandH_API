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
  public class EmotionController : Controller
  {
    private CandH_Context _context;

    public EmotionController(CandH_Context context)
    {
      _context = context;
    }

    [HttpGet(Name = "GetEmotions")]
    public IActionResult GET(int? emotionId)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IQueryable<ComicStripEmotion> emotions = _context.Emotion;

      if (emotionId != null)
      {
        IQueryable<ComicStripEmotion> singleEmotion = emotions.Where(emotion => emotion.ComicStripEmotionId == emotionId);
        return Ok(singleEmotion);
      }

      IQueryable<ComicStripEmotion> distinctEmotions = emotions.Distinct();

      if (distinctEmotions == null)
      {
        return NotFound();
      }

      return Ok(distinctEmotions);
    }

    [HttpPost]
    public IActionResult POST([FromBody] ComicStripEmotion newEmotion)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Emotion.Add(newEmotion);

      try
      {
        _context.SaveChanges();
      }
      catch (DbUpdateException)
      {
        return new StatusCodeResult(StatusCodes.Status418ImATeapot);
      }

      string createdLocation = "http://localhost:5000/api/Emotion?emotionId=" + newEmotion.ComicStripEmotionId;
      return Created(createdLocation, newEmotion);
    }
  }
}
