using System.ComponentModel.DataAnnotations;

namespace Demo.Pages.Courses;

public class View : PageModel
{
    private readonly ICourseManagementModule _module;

    public View(ICourseManagementModule module)
    {
        _module = module;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var student = await _module.SendQuery(new GetStudent.Query(id));
        Id = id;
        FirstName = student.FirstName;
        LastName = student.LastName;
        return Page();
    }
    
    public Guid Id { get; set; }
    
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }
}