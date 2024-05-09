namespace Silicon.Blazor.ViewModels.Account
{
    public class NavigationVM
    {
        public string ActiveAction { get; set; }
        public NavigationVM(string activeAction = "Details")
        {
            ActiveAction = activeAction;
        }
    }
}
