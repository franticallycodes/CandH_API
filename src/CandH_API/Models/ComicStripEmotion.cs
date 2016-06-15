namespace CandH_API.Models
{
  public class ComicStripEmotion
  {
    public int ComicStripEmotionId { get; set; }
    public int ComicStripId { get; set; }
    public int ComicUserId { get; set; }
    public string Emotion { get; set; }

    public ComicStrip Strip { get; set; }
    public ComicUser Reader { get; set; }
  }
}
