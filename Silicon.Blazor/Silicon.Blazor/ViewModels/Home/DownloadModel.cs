using Silicon.Blazor.ViewModels.Components;

namespace Silicon.Blazor.ViewModels.Home;

public class DownloadModel
{
    public string? Id { get; set; }
    public ImageModel Image { get; set; } = new();
    public SectionTitleModel SectionTitle { get; set; } = new();

    public List<DownloadAppModel> DownloadBox { get; set; } = new();

    public DownloadModel()
    {
        Id = "download";
        Image.ImageUrl = "/images/iphone.svg";
        Image.AltText = "image of a mobile phone";
        SectionTitle.Title = "Download Our App for Any Devices:";


        DownloadBox =
        [
            new DownloadAppModel() { StoreName = "App Store", Title = "Editor's Choice", Rating = "rating 4.7, 187K+ reviews", LinkUrl= "https://www.apple.com/se/app-store/", Image = { ImageUrl = "/images/appstore.svg", AltText ="Go to appstore" } },
            new DownloadAppModel() { StoreName = "Google Play", Title = "App of the Day", Rating = "rating 4.8, 30K+ reviews", LinkUrl= "https://play.google.com/", Image = { ImageUrl = "/images/googleplay.svg", AltText = "Go to google play" } }
        ];
    }
}
