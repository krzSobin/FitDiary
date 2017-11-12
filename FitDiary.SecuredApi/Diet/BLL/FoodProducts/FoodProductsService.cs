using AutoMapper;
using FitDiary.Contracts.DTOs.Diet;
using FitDiary.Contracts.DTOs.Diet.FoodProducts;
using FitDiary.SecuredApi.Diet.DAL.FoodProductCategories;
using FitDiary.SecuredApi.Diet.DAL.FoodProducts;
using FitDiary.SecuredApi.Models.Diet;
using System.Collections.Generic;
using System.Configuration;

namespace FitDiary.SecuredApi.Diet.BLL.FoodProducts
{
    public class FoodProductsService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString;
        private readonly IFoodProductRepository _foodRepository;
        private readonly IFoodProductCategoryRepository _categoryRepository;

        public FoodProductsService(IFoodProductRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        #region GetProducts
        public IEnumerable<FoodProductDTO> GetFoodProducts()
        {
            var foodProductEntities = _foodRepository.GetFoodProducts();

            return Mapper.Map<IEnumerable<FoodProductDTO>>(foodProductEntities);
        }
        #endregion
        #region GetProducts(params)
        public IEnumerable<FoodProductDTO> GetFoodProducts(FoodProductQueryParams queryParams)//TODO check if null
        {
            IEnumerable<FoodProduct> foodProducts;
            if (queryParams == null)
            {
                foodProducts = _foodRepository.GetFoodProducts();
            }
            else
            {
                foodProducts = _foodRepository.GetFoodProducts(queryParams);
            }

            return Mapper.Map<IEnumerable<FoodProductDTO>>(foodProducts);
        }
        #endregion
        #region GetProduct
        public FoodProductDTO GetFoodProduct(int id)
        {
            var foodProduct = _foodRepository.GetFoodProductByID(id);

            return Mapper.Map<FoodProductDTO>(foodProduct);
        }
        #endregion
        #region PostProduct
        public AddFoodProductResultDTO PostFoodProduct(ProductInsertDTO productDTO)
        {
            var createResult = new AddFoodProductResultDTO
            {
                Added = false,
                FoodProduct = null
            };

            var product = Mapper.Map<FoodProduct>(productDTO);

            var addedProduct = _foodRepository.InsertFoodProduct(product);
            if (_foodRepository.Save())
            {
                createResult.Added = true;
                createResult.FoodProduct = Mapper.Map<FoodProductDTO>(addedProduct);
            }

            return createResult;
        }
        #endregion

        #region PutProduct
        public UpdateFoodProductResultDTO UpdateFoodProduct(UpdateProductDTO productDTO)
        {
            var result = new UpdateFoodProductResultDTO
            {
                Updated = false,
                FoodProduct = null
            };

            var foodProductEntity = _foodRepository.GetFoodProductByID(productDTO.Id);
            if (foodProductEntity == null)
                return null;

            foodProductEntity.KCalPer100g = productDTO.KCalPer100g;
            foodProductEntity.ProteinsPer100g = productDTO.ProteinsPer100g;
            foodProductEntity.FatsPer100g = productDTO.KCalPer100g;
            foodProductEntity.CarbsPer100g = productDTO.ProteinsPer100g;
            foodProductEntity.SugarPer100g = productDTO.SugarPer100g;
            foodProductEntity.CategoryId = productDTO.CategoryId;
            foodProductEntity.Category = _categoryRepository.GetCategoryById(productDTO.CategoryId);
            foodProductEntity.Name = productDTO.Name;

            result.FoodProduct = Mapper.Map<FoodProductDTO>(foodProductEntity);

            _foodRepository.UpdateFoodProduct(foodProductEntity);

            result.Updated = _foodRepository.Save();

            return result;
        }
        #endregion

        #region DeleteProduct
        public DeleteFoodProductResultDTO DeleteFoodProduct(int id)
        {
            var foodProduct = _foodRepository.GetFoodProductByID(id);
            if (foodProduct == null)
                return null;

            var result = new DeleteFoodProductResultDTO
            {
                Deleted = false,
                FoodProduct = Mapper.Map<FoodProductDTO>(foodProduct)
            };

            _foodRepository.DeleteFoodProduct(foodProduct);

            result.Deleted = _foodRepository.Save();

            return result;
        }
        #endregion
    }
}