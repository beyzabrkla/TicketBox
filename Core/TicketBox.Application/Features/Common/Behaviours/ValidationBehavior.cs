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
            //FluentValidationın ihtiyaç duyduğu context nesnesini oluşturuyoruz.
            //hangi veriyi doğrulayacağım? sorusunun cevabıdır
            var context = new ValidationContext<TRequest>(request);

            //Sistemde kayıtlı olan tüm validatorları aynı anda çalıştırırız.
            //WhenAll ile hepsini tetikleyip performans kazanırız.
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            //Tüm validatorlardan gelen hata listelerini tek bir listeye toplarız.
            //Eğer bir validator hata dönerse, onları filtreleyip düz bir liste haline getiririz.
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            //Eğer listede en az 1 hata bile varsa,
            // İşlemi burada keseriz ve ValidationException fırlatırız. 
            // Bu sayede kod 'Handler' sınıfına (veritabanı işlemlerinin yapıldığı yere) asla ulaşamaz.
            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            //Eğer hata yoksa,
            //next() metodu, sıradaki durak olan 'Handler' sınıfını tetikler.
            //İşlem sorunsuz bir şekilde devam eder.
            return await next();
        }
    }
}
