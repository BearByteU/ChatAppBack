using AutoMapper;
using ChatApp.Constants;
using ChatApp.Interface;
using ChatApp.Models;
using ChatApp.SRMDbContexts.IRepository;
using ChatApp.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ChatApp.Service
{
    public class MessageService : IMessageService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        private readonly IHubContext<ChatHub> hubContext;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<ChatHub> hubContext )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.hubContext = hubContext;
        }
        public async Task<List<Message>> RecieveMessage(int id,int currentuserId)
        {
            try
            {
                var reciverUser = _unitOfWork.UserRepository.Find().Where(x => x.Id == id).FirstOrDefault();
                if (reciverUser != null)
                {
                    var getMessage = await _unitOfWork.MessageRepository.Find().Where(e =>
           (e.UserFrom == currentuserId  && e.UserTo == id)
           || (e.UserFrom == id && e.UserTo == currentuserId)).OrderByDescending(e=>e.MessageDate).ToListAsync();
                    if (getMessage!=null)
                    {
                        return getMessage;
                    }
                    return null;
                }
                //var getMessage = await _unitOfWork.MessageRepository.Find().Where(x => x.UserTo == request.UserTo).ToListAsync();
                //if (getMessage != null)
                //{

                //    return getMessage;
                //}
                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EntityResponseModel> SendMessage(MessageDto request)
        {
            try
            {
                if (request == null)
                {
                    return new EntityResponseModel
                    {
                        Status = false,
                        Message = ConstantString.ValidationError,
                    };
                }
                var reciverUser = _unitOfWork.UserRepository.Find().Where(x => x.Id == request.UserFrom).FirstOrDefault();
                if (reciverUser == null)
                {
                    return new EntityResponseModel

                    {
                        Status = false,
                        Message = ConstantString.ValidationError,
                    };
                }
                var uniqueKey = _unitOfWork.UniqueUserRepository.Find(x => x.UserFrom == request.UserFrom && x.UserTo == request.UserTo || x.UserTo == request.UserFrom && x.UserFrom == request.UserTo).FirstOrDefault();
                if (uniqueKey == null)
                {
                    uniqueKey = new UniqueUserKey();
                    uniqueKey.UniqueUserKeys = request.UserFrom + request.UserTo;
                    uniqueKey.UserFrom = request.UserFrom;
                    uniqueKey.UserTo = request.UserTo;
                    await _unitOfWork.UniqueUserRepository.Insert(uniqueKey);
                    await _unitOfWork.Save();
                }
                var sendmsg = _mapper.Map<Message>(request);
                sendmsg.RelationKey = uniqueKey.UniqueUserKeys;
                await _unitOfWork.MessageRepository.Insert(sendmsg);
                await _unitOfWork.Save();
                //string str = request.UserTo.ToString();
                //await _notification.SendNotificationParaller(request.UserTo, "GetNotify", _mapper.Map<MessageDto>(sendmsg));
                //await _notification.SendNotificationParaller(request.UserFrom, "GetNotify", _mapper.Map<MessageDto>(sendmsg));

                await hubContext.Clients.Groups(request.UserTo.ToString()).SendAsync("messageRecieve",request.MessageDescription);

                return new EntityResponseModel
                {
                    Message = ConstantString.Success,
                    Status = true
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
