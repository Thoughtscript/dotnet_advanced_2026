using System.ComponentModel.DataAnnotations.Schema;

namespace asp_entity.Models;

public class Example
{
    public int Id { get; set; }

    [Column("text")]
    public string? Text { get; set; }
}