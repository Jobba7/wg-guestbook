using Microsoft.AspNetCore.Mvc;
using WG.Guestbook.Web.Controllers;

namespace WG.Guestbook.Tests
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexReturnsViewResult()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}