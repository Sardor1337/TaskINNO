using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskINNO.Application.Models;

namespace TaskINNO.Application.Abstractions
{
    public interface IProductService
    {
        Task<ProductViewModel> GetByIdAsync(int id);
        Task<List<ProductViewModel>> GetPageSizeAsync(int page, int pagesize);
        Task CreateAsync(CreateProductModel model);
        Task<int> UpdateAsync(UpdateProductModel model);
        Task<int> DeleteAsync(int id);
    }
}
