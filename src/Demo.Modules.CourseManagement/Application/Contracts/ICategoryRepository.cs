using Demo.Modules.CourseManagement.Domain.Categories;

namespace Demo.Modules.CourseManagement.Application.Contracts;

public interface ICategoryRepository
{
    Task Add(Category category);
    Task<Category?> Get(CategoryId courseId);
}