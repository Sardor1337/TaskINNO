using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskINNO.Application.Abstractions;
using TaskINNO.Application.Models;
using TaskINNO.Domain.Entities;

namespace TaskINNO.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IAppDbContext _appDbContext;

        public CategoryService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateAsync(CreateCategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name,
            };

            _appDbContext.Categories.Add(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return 0;
            }

            _appDbContext.Categories.Remove(entity);
            await _appDbContext.SaveChangesAsync();

            return 1;
        }

        public async Task<CategoryViewModel> GetByIdAsync(int id)
        {
            var entity = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(entity == null)
            {
                return new CategoryViewModel();
            }

            return new CategoryViewModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<List<CategoryViewModel>> GetPageSizeAsync(int page, int pagesize)
        {

            return await _appDbContext.Categories
                .Select(entity => new CategoryViewModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                })
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();
        }

        public async Task<int> UpdateAsync(UpdateCategoryModel model)
        {
            var entity = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
            {
                return 0;
            }

            entity.Name = model.Name ?? entity.Name;

            _appDbContext.Categories.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return 1;
        }
    }
}
