﻿using MediatR;
using Rentora.Application.Base;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Application.Features.Product.Commands.Models
{
    public class AddProductCommand : IRequest<Response<ProductDTO>>
    {
        public string ApplicationUserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
        public string RentalPeriod { get; set; }
        public string Location { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Status { get; set; } = "Available";

        //Electronic & Sport
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Condition { get; set; } // Travel


        //Transportation
        public string Transmission { get; set; }
        public string Body_Type { get; set; }
        public string Fuel_Type { get; set; }
    }
}
