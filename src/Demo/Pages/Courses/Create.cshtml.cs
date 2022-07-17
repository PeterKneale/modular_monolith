using System.ComponentModel.DataAnnotations;
using Demo.Modules.CourseManagement.Application.Courses.Commands;

namespace Demo.Pages.Courses;

public class Create : PageModel
{
    private readonly ICourseManagementModule _mediator;

    public Create(ICourseManagementModule mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _mediator.SendCommand(new CreateCourse.Command(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Title, Description));

        return RedirectToPage(nameof(Students.Index));
    }

    [Display(Name = "Title")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    public string Title { get; set; }

    [Display(Name = "Description")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    public string Description { get; set; }
}