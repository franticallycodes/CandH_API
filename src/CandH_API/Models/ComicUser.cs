using System.Collections.Generic;

namespace CandH_API.Models
{
  public class ComicUser
  {
    public int ComicUserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }

    public ICollection<ComicStripEmotion> Emotions { get; set; }
    public ICollection<FavoriteComicStrip> Favorites { get; set; }
    public ICollection<ComicStripComment> Comments { get; set; }
  }
}
