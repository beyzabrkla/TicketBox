using FluentValidation;
using MediatR;

namespace TicketBox.Application.Features.Common.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>//where TRequest : IRequest<TResponse>, bu sınıfın sadece MediatR komutları/sorguları için çalıştığını garanti eder.
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        //Gelen isteği (TRequest) ve dönecek sonucu (TResponse) MediatR'a tanımlıyoruz.
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            // Hata listesini en başta hazırlıyoruz
            var failures = new List<FluentValidation.Results.ValidationFailure>();

            foreach (var validator in _validators)
            {
                var result = await validator.ValidateAsync(context, cancellationToken);

                // Sadece hata varsa listeye ekle
                if (!result.IsValid)
                {
                    failures.AddRange(result.Errors);
                }
            }

            // Eğer listede en az 1 hata bile varsa işlemi kesiyoruz.
            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            // Hata yoksa, veritabanı işlemlerinin yapılacağı Handler'a geçiş yap.
            return await next();
        }
    }
}
