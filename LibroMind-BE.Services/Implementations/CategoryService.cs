using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryGetDTO>> FindCategoryesAsync()
        {
            return _mapper.Map<IEnumerable<CategoryGetDTO>>(await _unitOfWork.CategoryRepository.FindAllAsync());
        }

        public async Task<CategoryGetDTO> FindCategoryByIdAsync(int id)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.FindByIdAsync(id);

            if (existingCategory is null)
            {
                throw new BadHttpRequestException("Category not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<CategoryGetDTO>(existingCategory);
        }

        public async Task AddCategory(CategoryPostDTO categoryToAdd)
        {
            var newCategory = _mapper.Map<Category>(categoryToAdd);

            _unitOfWork.CategoryRepository.Add(newCategory);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateCategory(int id, CategoryPostDTO categoryToUpdate)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.FindByIdAsync(id);

            if (existingCategory is null)
            {
                throw new BadHttpRequestException("Category not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(categoryToUpdate, existingCategory);

            _unitOfWork.CategoryRepository.Update(existingCategory);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.FindByIdAsync(id);

            if (existingCategory is null)
            {
                throw new BadHttpRequestException("Category not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.CategoryRepository.Remove(existingCategory);

            await _unitOfWork.CommitAsync();
        }
    }
}
