using FitDiary.Api.DAL;
using FitDiary.Api.Models;
using FitDiary.Api.Models.QueryModels;
using FitDiary.Contracts.DTOs.Diet;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace FitDiary.Api.Services
{
    public interface IFoodProductService
    {
        IEnumerable<FoodProductDTO> GetFoodProducts(FoodProductQuery query);
        Task<FoodProduct> DeleteFoodProductAsync(int id);
        Task<FoodProduct> PostFoodProductAsync(FoodProduct foodProduct);
        Task<FoodProductDTO> GetFoodProductAsync(int id);
        Task PutFoodProductAsync(int id, FoodProduct foodProduct);
    }
    public class FoodProductService : IFoodProductService
    {
        private readonly FitDiaryApiContext _db;

        public FoodProductService(FitDiaryApiContext dbcontext)
        {
            _db = dbcontext;
        }

        public IEnumerable<FoodProductDTO> GetFoodProducts(FoodProductQuery query)
        {
            var products = _db.FoodProducts.Select(product =>
            new FoodProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                CarboPer100g = product.CarboPer100g,
                ProteinsPer100g = product.ProteinsPer100g,
                FatsPer100g = product.FatsPer100g,
                SugarPer100g = product.SugarPer100g,
                KCalPer100g = product.KCalPer100g
            });
            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                products = products.Where(p => p.Category == query.Category);
            }
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(p => p.Name.Contains(query.Name));
            }
            if (query.MaxSugar.HasValue)
            {
                products = products.Where(p => p.SugarPer100g <= query.MaxSugar);
            }

            var orderedProd = products.OrderBy(p => p.Name); //TODO zrobic sortowanie po query.sortfield

            return orderedProd.ToList<FoodProductDTO>();
        }

        public async Task<FoodProductDTO> GetFoodProductAsync(int id)
        {
            var product = await _db.FoodProducts.Select(p =>
            new FoodProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category.Name,
                CarboPer100g = p.CarboPer100g,
                ProteinsPer100g = p.ProteinsPer100g,
                FatsPer100g = p.FatsPer100g,
                SugarPer100g = p.SugarPer100g,
                KCalPer100g = p.KCalPer100g
            }).SingleOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<FoodProduct> PostFoodProductAsync(FoodProduct foodProduct)
        {
            _db.FoodProducts.Add(foodProduct);
            await _db.SaveChangesAsync();

            return foodProduct;
        }

        public async Task<FoodProduct> DeleteFoodProductAsync(int id)
        {
            var foodProduct = await _db.FoodProducts.FindAsync(id);
            if (foodProduct == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var prod = _db.FoodProducts.Remove(foodProduct);
            await _db.SaveChangesAsync();

            return prod;
        }

        public async Task PutFoodProductAsync(int id, FoodProduct foodProduct)
        {
            _db.Entry(foodProduct).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodProductExists(id))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }
        }

        private bool FoodProductExists(int id)
        {
            return _db.FoodProducts.Count(e => e.Id == id) > 0;
        }

    }
}