using System.ComponentModel.DataAnnotations;

namespace Demo.Pages.Students;

public class Create : PageModel
{
    private readonly ICourseManagementModule _module;

    public Create(ICourseManagementModule module)
    {
        _module = module;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _module.SendCommand(new CreateStudent.Command(Guid.NewGuid(), FirstName, LastName));
        
        return RedirectToPage(nameof(Index));
    }

    [Display(Name = "First Name")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    public string LastName { get; set; }
}