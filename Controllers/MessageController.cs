using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Constants;
using ChatApp.Interface;
using ChatApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class MessageController : BaseController
    {
        public IMessageService _message;
        public MessageController(IMessageService message)
        {
            _message = message;
        }
        [HttpPost("SendMessage")]
        public async Task<ActionResult> SendMessage([FromBody]MessageDto request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    request.UserFrom = UserId;
                    var data = await _message.SendMessage(request);
                    if (data == null)
                    {
                        return NotFound(data);
                    }
                    return Ok(data);
                }
                return UnprocessableEntity(new EntityResponseModel
                {
                    Status = false,
                    Message = ConstantString.ValidationError
                });
            }
            catch (Exception)
            {
                return BadRequest(new EntityResponseModel
                {
                    Status = false,
                    Message = ConstantString.InternalSeverError
                });
            }
        }
        [HttpGet("RecieveMessage")]
        public async Task<ActionResult> RecieveMessage(int id)
         {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _message.RecieveMessage(id,UserId);
                    if (data == null)
                    {
                        return NotFound(data);
                    }
                    return Ok(data);
                }
                return UnprocessableEntity(new EntityResponseModel
                {
                    Status = false,
                    Message = ConstantString.ValidationError
                });
            }
            catch (Exception)
            {
                return BadRequest(new EntityResponseModel
                {
                    Status = false,
                    Message = ConstantString.InternalSeverError
                });
            }
        }
    }
}