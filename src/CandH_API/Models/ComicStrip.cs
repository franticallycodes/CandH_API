using System;
using System.Collections.Generic;

namespace CandH_API.Models
{
  public class ComicStrip
  {
    public int ComicStripId { get; set; }
    public string Name { get; set; }
    public DateTime? OriginalPrintDate { get; set; }
    public string Transcript { get; set; }
    public byte[] Image { get; set; }

    public ICollection<ComicStripEmotion> Emotions { get; set; }
    public ICollection<FavoriteComicStrip> Favorited { get; set; }
    public ICollection<ComicStripComment> Comments { get; set; }
  }
}
