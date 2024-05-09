using Silicon.Blazor.ViewModels.Components;

namespace Silicon.Blazor.ViewModels.Home;

public class NewsletterModel
{
    public SectionTitleModel NewsletterTitle { get; set; } = new SectionTitleModel();
    public List<CheckboxModel> Checkbox { get; set; } = [];
    public ImageModel NewsletterImage { get; set; } = new ImageModel();

    //Use this to send data from input-field. 
    //public SubscribeModel Subscriber { get; set; } = new SubscribeModel();

    public NewsletterModel()
    {
        NewsletterTitle.Title = "Don't Want To Miss Anything?";
        NewsletterTitle.Subtitle = "Sign up for Newsletters";
        NewsletterImage.ImageUrl = "images/arrows.svg";
        NewsletterImage.AltText = "Squiggly arrow";
        Checkbox =
            [
                new CheckboxModel() { Id = "daily-newsletter", Text = "Daily Newsletter" },
                new CheckboxModel() { Id = "advertising", Text = "Advertising Updates" },
                new CheckboxModel() { Id = "review", Text = "Week in Review" },
                new CheckboxModel() { Id = "events", Text = "Event Updates" },
                new CheckboxModel() { Id = "startup", Text = "Startups Weekly" },
                new CheckboxModel() { Id = "podcasts", Text = "Podcasts" }
            ];
    }
}
