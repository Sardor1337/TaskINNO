using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task DeleteAsync(int id)
        {
            var entity = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception("Not Found");
            }

            _appDbContext.Categories.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<CategoryViewModel> GetByIdAsync(int id)
        {
            var entity = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if(entity == null)
            {
                throw new Exception("Not Found");
            }

            return new CategoryViewModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<List<CategoryViewModel>> GetPageSizeAsync(int page, int pagesize)
        {
            if(page < 1)
            {
                throw new Exception("Page Exeption");
            }

            return await _appDbContext.Categories
                .Select(entity => new CategoryViewModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                })
                .Skip((page - 1) * 10)
                .Take(pagesize)
                .ToListAsync();
        }

        public async Task UpdateAsync(UpdateCategoryModel model)
        {
            var entity = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
            {
                throw new Exception("Not found");
            }

            entity.Name = model.Name ?? entity.Name;

            _appDbContext.Categories.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
