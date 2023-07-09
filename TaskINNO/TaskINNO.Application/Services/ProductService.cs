﻿using Microsoft.EntityFrameworkCore;
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
    public class ProductService : IProductService
    {
        private readonly IAppDbContext _appDbContext;

        public ProductService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateAsync(CreateProductModel model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                CreateAt = DateTime.UtcNow,
                CategoryId = model.CategoryId
            };

            _appDbContext.Products.Add(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception("Not found");
            }

            _appDbContext.Products.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            var entity = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if(entity == null)
            {
                throw new Exception("Not Found");
            }

            return new ProductViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                CreateAt = entity.CreateAt,
                CategoryId = entity.CategoryId
            };
        }

        public async Task<List<ProductViewModel>> GetPageSizeAsync(int page, int pagesize)
        {
            if (page < 1)
            {
                throw new Exception("Page Exeption");
            }

            return await _appDbContext.Products
                .Select(entity => new ProductViewModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Price = entity.Price,
                    CreateAt = entity.CreateAt,
                    CategoryId = entity.CategoryId
                })
                .Skip((page - 1) * 10)
                .Take(pagesize)
                .ToListAsync();
        }

        public async Task UpdateAsync(UpdateProductModel model)
        {
            var entity = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == model.Id);

            if(entity == null)
            {
                throw new Exception("Not found");
            }

            entity.Name = model.Name ?? entity.Name;
            entity.Price = model.Price ?? entity.Price;
            entity.CreateAt = DateTime.UtcNow;
            entity.CategoryId = model.CategoryId ?? entity.CategoryId;

            _appDbContext.Products.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}