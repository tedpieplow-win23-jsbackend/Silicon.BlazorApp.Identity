namespace Silicon.Blazor.ViewModels.Home;

public class ManageWorkModel
{
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public List<string> Features { get; set; } = null!;
    public string ButtonText { get; set; }
    public string ButtonIcon { get; set; }

    public ManageWorkModel()
    {
        Title = "Manage your work";
        ImageUrl = "images/manage_work.svg";
        Features =
        [
            "Powerful project management",
                "Transparent work management",
                "Manage work & focus on the most important tasks",
                "Track your progress with interactive charts",
                "Easiest way to track time spent on tasks"
        ];

        ButtonText = "Learn more";
        ButtonIcon = "fa-solid fa-arrow-right";
    }
}
