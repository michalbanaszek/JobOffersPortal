using System;

namespace JobOffersPortal.Application.Functions.Users.Commands.CreateUser
{
    public class CreateUserCommandResponse
    {
        public Uri Uri { get; set; }


        public CreateUserCommandResponse(Uri uri)
        {
            Uri = uri;
        }
    }
}
