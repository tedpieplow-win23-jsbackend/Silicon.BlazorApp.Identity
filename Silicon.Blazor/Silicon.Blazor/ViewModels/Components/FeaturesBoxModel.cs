namespace Silicon.Blazor.ViewModels.Components;

public class FeaturesBoxModel
{
    public string? BoxTitle { get; set; }
    public string? BoxText { get; set; }
    public ImageModel BoxImage { get; set; } = new ImageModel();
    public LinkModel? Link { get; set; }
}
