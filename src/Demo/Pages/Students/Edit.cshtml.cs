using System.ComponentModel.DataAnnotations;

namespace Demo.Pages.Students;

public class Edit : PageModel
{
    private readonly ICourseManagementModule _module;

    public Edit(ICourseManagementModule module)
    {
        _module = module;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var student = await _module.SendQuery(new GetStudent.Query(id));
        Id = student.Id;
        FirstName = student.FirstName;
        LastName = student.LastName;
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _module.SendCommand(new UpdateStudentName.Command(Id, FirstName, LastName));
        
        return RedirectToPage(nameof(Index));
    }

    [Required]
    [BindProperty]
    public Guid Id { get; set; }
    
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