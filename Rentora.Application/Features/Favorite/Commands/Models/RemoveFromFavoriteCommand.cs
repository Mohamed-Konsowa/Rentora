﻿
using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Favorite.Commands.Models
{
    public class RemoveFromFavoriteCommand : IRequest<Response<string>>
    {
        public Guid? UserId { get; set; }
        public int? ProductId { get; set; }
    }
}
