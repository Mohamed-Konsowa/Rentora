﻿using FluentValidation;
using Rentora.Application.Features.Product.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Product.Commands.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IProductService _productService;

        public UpdateProductValidator(IProductService productService)
        {
            ApplyValidationRules();
            _productService = productService;
        }
        void ApplyValidationRules()
        {
            RuleFor(p => p.ProductId)
                .MustAsync(async (Key, Can) => await _productService.GetProductById(Key) is not null)
                .WithMessage("Product not found!");

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(p => p.RentalPeriod)
                .NotEmpty().WithMessage("Rental Period is required.");

            RuleFor(p => p.Location)
                .NotEmpty().WithMessage("Location is required.");

            RuleFor(p => p.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

            RuleFor(p => p.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");

            RuleFor(p => p.Status)
                .Must(s => new[] { "Available", "Rented", "Pending" }.Contains(s))
                .WithMessage("Invalid status. Allowed values: Available, Rented, Pending.");

        }
    }
}
