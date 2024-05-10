namespace Silicon.Blazor.Models;

public class Subscribers
{
    public string Email { get; set; } = null!;
    public bool IsSubscribed { get; set; }

    // URI till Toggle sub
    // https://subscriptionprovider-silicon.azurewebsites.net/api/ToggleSubscriptionFunction?code=eBz-AH-lVbIir3LGga3ky_zdNHUoYDUfcKnNGXc-ejH1AzFuw1UUhg==
}
