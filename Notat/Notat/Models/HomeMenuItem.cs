namespace Notat.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Preferences,
        GoogleLogin
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
