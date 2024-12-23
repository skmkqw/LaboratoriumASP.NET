using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

[PrimaryKey("MovieId", "PersonId")]
[Table("movie_cast")]
public class MovieCastEntity
{
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Person")]
    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("character_name")]
    public string CharacterName { get; set; }

    public MovieEntity Movie { get; set; }
}
