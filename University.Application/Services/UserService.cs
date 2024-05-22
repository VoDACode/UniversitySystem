﻿using University.Domain.Entity.User;
using University.Domain.Entity.User.Requests;
using University.Domain.Entity.User.Responses;
using University.Domain.Exceptions;
using University.Domain.Repositores;
using University.Domain.Requests;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.Application.Services
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserResponse> CreateUser(CreateUserRequest request)
        {
            if(userRepository.GetUserByEmail(request.Email) != null)
            {
                throw new BadRequestException("Email already exists");
            }

            if (userRepository.GetUserByPhone(request.Phone) != null)
            {
                throw new BadRequestException("Phone already exists");
            }

            if (userRepository.GetUserByTaxId(request.TaxId) != null)
            {
                throw new BadRequestException("TaxId already exists");
            }

            var user = new UserEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                TaxId = request.TaxId,
                Email = request.Email,
                Phone = request.Phone,
                Role = request.Role
            };

            return await userRepository.CreateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            if(!await userRepository.ExistsById(id))
            {
                throw new NotFoundException("User not found");
            }

            await userRepository.DeleteUser(id);
        }

        public async Task<DetailedUserResponse> GetDetailedUserById(int id)
        {
            if (!await userRepository.ExistsById(id))
            {
                throw new NotFoundException("User not found");
            }
            return await userRepository.GetUserById(id) ?? throw new NotFoundException("User not found");
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            if (!await userRepository.ExistsById(id))
            {
                throw new NotFoundException("User not found");
            }
            return await userRepository.GetUserById(id) ?? throw new NotFoundException("User not found");
        }

        public async Task<PageResponse<UserResponse>> GetUsers(PageRequest request)
        {
            IQueryable<UserEntity> query = await userRepository.GetAllUsers();
            IQueryable<UserResponse> usersResponseQuery = query.Select(u => new UserResponse(u));
            return await PageResponse<UserResponse>.Create(usersResponseQuery, request);
        }

        public async Task<UserResponse> UpdateUser(int id, UpdateUserRequest request)
        {
            if(request.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new BadRequestException("Date of birth cannot be in the future");
            }

            var user = await userRepository.GetUserById(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.DateOfBirth = request.DateOfBirth;
            user.TaxId = request.TaxId;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.Role = request.Role;

            return await userRepository.UpdateUser(user);
        }
    }
}
