﻿using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetProductsWithCategory(int categoryId)
        {
            return await _context.Products.
                Where(p => p.CategoryId == categoryId).
                Include(p => p.Category)
                .ToListAsync();
        }
    }
}
