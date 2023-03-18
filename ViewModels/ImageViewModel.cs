using Gallery.Models;

namespace Gallery.ViewModels;

public class ImageViewModel {
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<Tag>? Tags { get; set; }
    public IFormFile File { get; set; }
}