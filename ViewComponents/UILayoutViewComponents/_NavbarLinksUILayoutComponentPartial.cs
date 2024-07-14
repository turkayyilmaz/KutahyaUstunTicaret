using Microsoft.AspNetCore.Mvc;

namespace KutahyaUstunTicaret.ViewComponents.UILayoutViewComponents
{
    public class _NavbarLinksUILayoutComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
