namespace Silicon.Blazor.ViewModels.Home;

public class LightAndDarkModel
{
    public string TitleWhite { get; set; } = null!;
    public string TitleBlack { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string ImageAlt { get; set; } = null!;
    public string IconUrl { get; set; } = null!;
    public string IconAlt { get; set; } = null!;

    public LightAndDarkModel()
    {
        TitleWhite = "Switch Between";
        TitleBlack = "Light and Dark";
        ImageUrl = "images/macbook_test.png";
        ImageAlt = "Laptop";
        IconUrl = "images/arrows_laptop.png";
        IconAlt = "Macbook";
    }
}

