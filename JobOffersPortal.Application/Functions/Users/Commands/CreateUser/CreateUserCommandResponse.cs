using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.Users.Commands.CreateUser
{
    public class CreateUserCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public CreateUserCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public CreateUserCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
