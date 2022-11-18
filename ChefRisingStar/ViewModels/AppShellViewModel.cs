namespace ChefRisingStar.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public AppShellViewModel()
        {
            CurrentUser = new Models.User
            {
                Username = "Fallout",
                EmailAddress = "fallout99@hotmail.com",
                IsAdmin = true
            };
        }
    }
}