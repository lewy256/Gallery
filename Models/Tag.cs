using Newtonsoft.Json;

namespace Gallery.Models;

public class Tag {

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
}