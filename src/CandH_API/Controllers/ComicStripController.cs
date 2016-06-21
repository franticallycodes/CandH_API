using CandH_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    public IActionResult GET(int? comicStripId, string emotionSearchString)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IQueryable<ComicStrip> comicStrips = from s in _context.Strip
                                           join e in _context.Emotion
                                           on s.ComicStripId equals e.ComicStripId into temp
                                           from substrip in temp.DefaultIfEmpty()
                                           select new ComicStrip
                                           {
                                             ComicStripId = s.ComicStripId,
                                             OriginalPrintDate = s.OriginalPrintDate,
                                             Transcript = s.Transcript,
                                             Image = s.Image,
                                             Emotions = _context.Emotion
                                               .Where(emo => emo.ComicStripId == s.ComicStripId).ToList(),
                                             Comments = (from c in _context.Comment
                                                         join r in _context.Reader
                                                         on c.ComicUserId equals r.ComicUserId
                                                         where c.ComicStripId == s.ComicStripId
                                                         select new ComicStripComment {
                                                           ComicStripCommentId = c.ComicStripCommentId,
                                                           ComicStripId = c.ComicStripId,
                                                           ComicUserId = c.ComicUserId,
                                                           Comment = c.Comment,
                                                           Reader = new ComicUser
                                                           {
                                                             ComicUserId = r.ComicUserId,
                                                             Username = r.Username,
                                                             Email = r.Email
                                                           }
                                                         }).ToList()
                                           };

      if (comicStripId == null && emotionSearchString == null)
      {
        comicStrips = comicStrips.Take(1);
        return Ok(comicStrips);
      }

      if (comicStripId != null)
      {
        comicStrips = comicStrips.Where(comic => comic.ComicStripId == comicStripId);
      }

      if (emotionSearchString != null)
      {
        emotionSearchString = emotionSearchString.ToLower();
        string[] emotions = emotionSearchString.Split(' ');
        //int emotionCount = emotions.Length;
        IQueryable<ComicStripEmotion> comicsWithEmotions = _context.Emotion;

        IQueryable<ComicStripEmotion> concatEmotions = comicsWithEmotions.Where(emo => emo.ComicStripId == 0);
        foreach (string emotion in emotions)
        {
          comicsWithEmotions = comicsWithEmotions.Where(comic => comic.Emotion.Contains(emotion));
          concatEmotions = concatEmotions.Concat(comicsWithEmotions);
        }
        List<ComicStripEmotion> emotionalComicsList = concatEmotions.ToList();

        IQueryable<ComicStrip> matchedComics = comicStrips.Where(comic => comic.ComicStripId == 0);
        foreach (ComicStripEmotion emotionalComic in emotionalComicsList)
        {
          comicStrips = comicStrips.Where(comic => comic.ComicStripId == emotionalComic.ComicStripId);
          matchedComics = matchedComics.Concat(comicStrips);
        }
        comicStrips = matchedComics;
      }
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
