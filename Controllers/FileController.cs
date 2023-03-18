using Gallery.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.Controllers;

public class FileController : Controller {
    private readonly BlobService _blobService;

    public FileController(BlobService blobService) {
        _blobService = blobService;
    }

    [HttpGet]
    public async Task<IActionResult> GetImage(Guid id) {
        var blob = await _blobService.GetImage(id);
        return File(blob, "image/png", id.ToString());
    }
}