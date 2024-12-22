using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

[Table("movie_cast")]
[Keyless]
public class MovieCastEntity
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Person")]
    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("character_name")]
    public string CharacterName { get; set; }

    public MovieEntity Movie { get; set; }
}
