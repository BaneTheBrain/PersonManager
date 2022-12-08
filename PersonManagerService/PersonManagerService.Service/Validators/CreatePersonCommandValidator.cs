using FluentValidation;
using PersonManagerService.Application.Commands.CreatePerson;

namespace PersonManagerService.Application.Validators;

public class CreatePersonCommandValidator : AbstractValidator<CreateOrUpdatePersonCommand>
{
	public CreatePersonCommandValidator()
	{
        string lettersPattern = "^[a-zA-Z ]*$";

        RuleFor(x => x.FirstName).NotEmpty().WithMessage($"Person fist name can not be empty");
        RuleFor(x => x.FirstName).MinimumLength(3).WithMessage($"Person first name must be at least 3 characters long");
        RuleFor(x => x.FirstName).Matches(lettersPattern).WithMessage("Person first name should only be contained of letters");

        RuleFor(x => x.LastName).NotEmpty().WithMessage($"Person last name can not be empty");
        RuleFor(x => x.LastName).MinimumLength(3).WithMessage($"Person last name must be at least 3 characters long");
        RuleFor(x => x.LastName).Matches(lettersPattern).WithMessage("Person last name should only be contained of letters");
            
        RuleForEach(x => x.Skills).NotEmpty().NotNull().WithMessage("All person skills must not be empty/null");
        RuleForEach(x => x.SocialMediaAccounts).NotEmpty().NotNull().WithMessage("All social media accounts must not be empty/null");
        RuleForEach(x => x.SocialMediaAccounts).Must(x => !string.IsNullOrWhiteSpace(x.Type)).WithMessage("Social media account types must have a value");
        RuleForEach(x => x.SocialMediaAccounts).Must(x => !string.IsNullOrWhiteSpace(x.Address)).WithMessage("Social media account addresses must have a value");
    }
}
