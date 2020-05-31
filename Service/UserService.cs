using AutoMapper;
using ChatApp.Constants;
using ChatApp.Models;
using ChatApp.SRMDbContexts.IRepository;
using ChatApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskQ.BAL.Interface;

namespace ChatApp.Service
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EntityResponseModel> CreateUser(UserDto request)
        {
            try
            {
                if (request == null)
                {
                    return new EntityResponseModel
                    {
                        Message = ConstantString.ValidationError,
                        Status = false
                    };
                }
                var newUser = _mapper.Map<User>(request);
                newUser.CreationDate = DateTime.UtcNow;
                await _unitOfWork.UserRepository.Insert(newUser);
                await _unitOfWork.Save();
                return new EntityResponseModel
                {
                    Message = ConstantString.Success,
                    Status = true                    
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Task<EntityResponseModel> DeleteUser(long id)
        {
            try
            {

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<LeftUserOutput>> LeftUser(int id)
        {
            try
            {
                var leftUser = _unitOfWork.MessageRepository.Find().Where(x => x.UserTo == id || x.UserFrom == id)
                                            .GroupBy(e => e.RelationKey, (e, c) => c.Max(x => x.Id));

                var users = await _unitOfWork.MessageRepository.Find(e => leftUser.Contains(e.Id))
                            .Include(e => e.Sender).Include(e => e.Reciever).OrderByDescending(e=>e.MessageDate).Select(x => new LeftUserOutput
                            {
                                UserId = x.UserFrom == id ? x.Reciever.Id : x.Sender.Id,
                                Name = x.UserFrom == id ? x.Reciever.Name : x.Sender.Name,
                                Message = x.MessageDescription,
                                Date = x.MessageDate.Date
                                
                            }).ToListAsync();
                return users;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<UserDto> SearchUser(SearchUserDto request)
        {
            throw new NotImplementedException();
        }

        public Task<EntityResponseModel> UpdateUserDetail(UserDto request)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}