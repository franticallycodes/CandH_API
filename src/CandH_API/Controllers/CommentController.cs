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
  public class CommentController :Controller
  {
    private CandH_Context _context;

    public CommentController(CandH_Context context)
    {
      _context = context;
    }

    [HttpGet(Name = "GetComments")]
    // api/Comment
    public IActionResult GET(int? commentId)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IQueryable<ComicStripComment> comments = _context.Comment;

      if (commentId != null)
      {
        IQueryable<ComicStripComment> singleComment = comments.Where(comment => comment.ComicStripCommentId == commentId);
        return Ok(singleComment);
      }

      if (comments == null)
      {
        return NotFound();
      }

      return Ok(comments);
    }

    [HttpPost]
    public IActionResult POST([FromBody] ComicStripComment newComment)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Comment.Add(newComment);

      try
      {
        _context.SaveChanges();
      }
      catch (DbUpdateException)
      {
        return new StatusCodeResult(StatusCodes.Status418ImATeapot);
      }

      string createdLocation = "http://localhost:5000/api/Emotion?commentId=" + newComment.ComicStripCommentId;
      return Created(createdLocation, newComment);
    }
  }
}
