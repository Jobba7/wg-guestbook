namespace WG.Guestbook.Web.Models.Layout
{
    public class NavigationMenuViewModel
    {
        public bool IsSignedIn { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsRoommate { get; set; }

        public string? UserName { get; set; }
    }
}
