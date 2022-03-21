using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.Users.Commands.DeleteUser
{
    public class DeleteUserCommandResponse : BaseResponse
    {
        public DeleteUserCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public DeleteUserCommandResponse() : base()
        {
        }
    }
}
