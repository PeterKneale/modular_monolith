using Demo.Modules.CourseManagement.Application.Courses.Queries;

namespace Demo.Pages.Courses;

public class Index : PageModel
{
    private readonly ICourseManagementModule _mediator;

    public Index(ICourseManagementModule mediator)
    {
        _mediator = mediator;
    }

    public async Task OnGetAsync()
    {
        Data = await _mediator.SendQuery(new ListCourses.Query());
    }

    public ListCourses.Result Data { get; private set; }
}