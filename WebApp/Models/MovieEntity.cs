using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

[Table("movie")]
public class MovieEntity
{
    [Key]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("budget")]
    public int? Budget { get; set; }

    [Column("popularity")]
    public float? Popularity { get; set; }

    [Column("homepage")]
    public string Homepage { get; set; }
}
