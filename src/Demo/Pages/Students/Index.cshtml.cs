namespace Demo.Pages.Students;

public class Index : PageModel
{
    private readonly ICourseManagementModule _module;

    public Index(ICourseManagementModule module)
    {
        _module = module;
    }

    public async Task OnGetAsync()
    {
        Data = await _module.SendQuery(new ListStudents.Query());
    }

    public ListStudents.Result Data { get; private set; }
}