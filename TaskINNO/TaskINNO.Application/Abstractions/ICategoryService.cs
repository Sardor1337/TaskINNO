using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskINNO.Application.Models;

namespace TaskINNO.Application.Abstractions
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> GetByIdAsync(int id);
        Task<List<CategoryViewModel>> GetPageSizeAsync(int page, int pagesize);
        Task CreateAsync(CreateCategoryModel model);
        Task UpdateAsync(UpdateCategoryModel model);
        Task DeleteAsync(int id);
    }
}
