namespace CandH_API.Models
{
  public class ComicStripComment
  {
    public int ComicStripCommentId { get; set; }
    public int ComicStripId { get; set; }
    public int ComicUserId { get; set; }
    public string Comment { get; set; }

    public ComicStrip Strip { get; set; }
    public ComicUser Reader { get; set; }
  }
}
