﻿using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using LibroMind_BE.Services.Models.Get;
using LibroMind_BE.Services.Models.Put;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserGetDTO>> FindUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserGetDTO>>(await _unitOfWork.UserRepository.FindAllAsync());
        }

        public async Task<UserGetDTO> FindUserByIdAsync(int id)
        {
            var existingUser = await _unitOfWork.UserRepository.FindByIdAsync(id);

            if (existingUser is null)
            {
                throw new BadHttpRequestException("User not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<UserGetDTO>(existingUser);
        }

        public async Task<UserProfileGetDTO> FindUserProfileByIdAsync(int id)
        {
            var existingUser = await _unitOfWork.UserRepository.FindUserProfileByIdAsync(id);

            if (existingUser is null)
            {
                throw new BadHttpRequestException("User not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<UserProfileGetDTO>(existingUser);
        }

        public async Task UpdateUser(int id, UserPutDTO userToUpdate)
        {
            var existingUser = await _unitOfWork.UserRepository.FindByIdAsync(id);

            if (existingUser is null)
            {
                throw new BadHttpRequestException("User not found", StatusCodes.Status404NotFound);
            }

            var updatedUser = _mapper.Map<User>(userToUpdate);

            updatedUser.AddressId = existingUser.AddressId;
            updatedUser.LibraryId = existingUser.LibraryId;

            _unitOfWork.UserRepository.Update(existingUser);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteUser(int id)
        {
            var existingUser = await _unitOfWork.UserRepository.FindByIdAsync(id);

            if (existingUser is null)
            {
                throw new BadHttpRequestException("User not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.UserRepository.Remove(existingUser);

            await _unitOfWork.CommitAsync();
        }
    }
}
