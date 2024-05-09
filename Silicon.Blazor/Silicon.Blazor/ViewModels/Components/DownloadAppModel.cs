namespace Silicon.Blazor.ViewModels.Components;

public class DownloadAppModel
{
    public string? StoreName { get; set; }
    public string? Title { get; set; }
    public string? Rating { get; set; }
    public string? LinkUrl { get; set; }

    public ImageModel Image { get; set; } = new();
}
