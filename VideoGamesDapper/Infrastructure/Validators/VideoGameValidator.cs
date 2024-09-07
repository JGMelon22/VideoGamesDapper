using FluentValidation;
using VideoGamesDapper.DTOs;

namespace VideoGamesDapper.Infrastructure.Validators;

public class VideoGameValidator : AbstractValidator<VideoGameInput>
{
    public VideoGameValidator()
    {
        RuleFor(vg => vg.Title)
            .NotEmpty()
            .WithMessage("Title can not be emtpy!")
            .NotNull()
            .WithMessage("Title can not be null!")
            .MinimumLength(3)
            .WithMessage("Title must be at least 3 characters!")
            .MaximumLength(100)
            .WithMessage("Title can not exceed 100 charactes!");

        RuleFor(vg => vg.Publisher)
            .NotEmpty()
            .WithMessage("Publisher can not be emtpy!")
            .NotNull()
            .WithMessage("Publisher can not be null!")
            .MinimumLength(3)
            .WithMessage("Publisher must be at least 3 characters!")
            .MaximumLength(100)
            .WithMessage("Publisher can not exceed 100 charactes!");

        RuleFor(vg => vg.Developer)
            .NotEmpty()
            .WithMessage("Developer can not be emtpy!")
            .NotNull()
            .WithMessage("Developer can not be null!")
            .MinimumLength(3)
            .WithMessage("Developer must be at least 3 characters!")
            .MaximumLength(100)
            .WithMessage("Developer can not exceed 100 charactes!");
    }
}
