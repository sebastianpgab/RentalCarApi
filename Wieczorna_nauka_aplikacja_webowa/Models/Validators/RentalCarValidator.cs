using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;

namespace Wieczorna_nauka_aplikacja_webowa.Models.Validators
{
    public class RentalCarValidator : AbstractValidator<RentalCarQuery>
    {
        private int[] allowPagesSize = new[] { 5, 10, 15 };
        private string[] allowedSortByColumnNames =
        {
            nameof(RentalCar.Name),
            nameof(RentalCar.Description),
        };
        public RentalCarValidator(RentalCarDbContext dbContext)
        {
            RuleFor(k => k.PageNumber).GreaterThan(0);
            RuleFor(k => k.SortBy).Custom((value, context) =>
            {
                if(!string.IsNullOrEmpty(value))
                {
                    if (!allowedSortByColumnNames.Contains(value))
                    {
                        context.AddFailure("SortBy", $"SortBy is optional or must in [{string.Join(",", allowedSortByColumnNames)}]");
                    }
                }
            });
            RuleFor(k => k.PageSize).Custom((value/*wartość pageSize*/, context /*konteks walidacji*/) =>
            {
                if (!allowPagesSize.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowPagesSize)}]");
                }
            });

        }
    }
}
