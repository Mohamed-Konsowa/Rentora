﻿
using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Favorite.Commands.Models
{
    public class RemoveFromCartCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
