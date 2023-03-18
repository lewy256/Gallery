using Newtonsoft.Json;

namespace Gallery.Models;

public class Image {

    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "tags")]
    public List<Tag> Tags { get; set; }

    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [JsonProperty(PropertyName = "date")]
    public string Date { get; set; }
}