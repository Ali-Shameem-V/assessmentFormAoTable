using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace assessmentFormAoTable.Models;

public partial class Aotable
{
    [JsonIgnore]

    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]

    public string Type { get; set; } = null!;
    [JsonIgnore]

    public string? Description { get; set; }
    [JsonIgnore]

    public string? Comment { get; set; }
    [JsonIgnore]

    public int? History { get; set; }
    [JsonIgnore]

    public int? Boundary { get; set; }
    [JsonIgnore]

    public int? Log { get; set; }
    [JsonIgnore]

    public int? Cache { get; set; }
    [JsonIgnore]

    public int? Notify { get; set; }
    [JsonIgnore]

    public int? Identifier { get; set; }
    [JsonIgnore]
    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}
