using Gallery.Models;
using Gallery.Services;
using Gallery.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.Controllers;

public class GalleryController : Controller {
    private readonly CosmosService _cosmosService;
    private readonly BlobService _blobService;

    public GalleryController(CosmosService cosmosService, BlobService blobService) {
        _cosmosService = cosmosService;
        _blobService = blobService;
    }

    [HttpGet]
    public async Task<IActionResult> Index() {
        var imagesList = await _cosmosService.GetMultipleAsync($"SELECT * FROM c");

        return View(new GalleryViewModel() {
            Images = imagesList.ToList(),
        });
    }

    [HttpPost]
    public async Task<IActionResult> Index(string searchString) {
        var images = await _cosmosService.GetMultipleAsync($"SELECT * FROM c WHERE EXISTS(SELECT VALUE n FROM n IN c.tags WHERE n.name like \"{searchString}%\")");

        return View(new GalleryViewModel() {
            Images = images.ToList()
        });
    }

    [HttpGet]
    public async Task<ActionResult> Upload() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(ImageViewModel imageViewModel) {
        var guid = Guid.NewGuid();

        await _blobService.Upload(imageViewModel.File, guid);

        var image = new Image() {
            Id = guid.ToString(),
            Name = imageViewModel.Name,
            Tags = imageViewModel.Tags,
            Description = imageViewModel.Description,
            Date = DateTime.Now.ToString()
        };

        await _cosmosService.AddAsync(image);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id) {
        await _cosmosService.DeleteAsync(id);
        await _blobService.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}