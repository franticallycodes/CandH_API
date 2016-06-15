namespace CandH_API.Models
{
  public class FavoriteComicStrip
  {
    public int FavoriteComicStripId { get; set; }
    public int ComicStripId { get; set; }
    public int ComicUserId { get; set; }

    public ComicStrip Strip { get; set; }
    public ComicUser Reader { get; set; }
  }
}
