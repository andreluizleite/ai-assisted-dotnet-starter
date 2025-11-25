using FluentValidation;

namespace CleanArchitecture.Application.Commands.RequestPostGeneration
{
    public class RequestPostGenerationCommandValidator : AbstractValidator<RequestPostGenerationCommand>
    {
        public RequestPostGenerationCommandValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Topic).NotEmpty().MaximumLength(200);
        }
    }
}
