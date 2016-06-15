using Microsoft.EntityFrameworkCore;

namespace CandH_API.Models
{
  public class CandH_Context : DbContext
  {
    public CandH_Context(DbContextOptions<CandH_Context> options) : base (options)
    {

    }

    public DbSet<ComicStrip> Strip { get; set; }
    public DbSet<ComicUser> Reader { get; set; }
    public DbSet<ComicStripEmotion> Emotion { get; set; }
    public DbSet<FavoriteComicStrip> Favorite { get; set; }
    public DbSet<ComicStripComment> Comment { get; set; }
  }
}
