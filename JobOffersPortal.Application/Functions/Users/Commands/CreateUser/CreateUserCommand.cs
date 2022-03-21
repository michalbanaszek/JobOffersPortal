﻿using MediatR;

namespace JobOffersPortal.Application.Functions.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
