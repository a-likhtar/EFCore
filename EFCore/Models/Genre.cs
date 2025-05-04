using System.Text.Json.Serialization;

namespace EFCore.Models;

public class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    [JsonIgnore]
    public DateTime CreatedDate { get; set; }

    public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}