namespace ChefRisingStar.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public AppShellViewModel()
        {
            CurrentUser = new Models.User
            {
                Id = 1,
                Username = "Fallout",
                EmailAddress = "fallout99@hotmail.com",
                IsAdmin = true
            };
        }
    }
}