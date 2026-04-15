using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace RecipeManager.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        [BindProperty]
        public string ProductName { get; set; } = "";

        [BindProperty]
        public decimal Price { get; set; }

        public string? UserEmail { get; set; }
        public string Message { get; set; } = "";

        public bool IsLoggedIn => !string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail"));

        public void OnGet()
        {
            UserEmail = HttpContext.Session.GetString("UserEmail");
        }

        public IActionResult OnPostLogin()
        {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                HttpContext.Session.SetString("UserEmail", Email);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("UserEmail");
            return RedirectToPage();
        }

        public IActionResult OnPostCreateProduct()
        {
            var loggedInUser = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(loggedInUser))
            {
                return RedirectToPage();
            }

            UserEmail = loggedInUser;
            Message = $"Product created: {ProductName} (${Price})";
            return Page();
        }
    }
}