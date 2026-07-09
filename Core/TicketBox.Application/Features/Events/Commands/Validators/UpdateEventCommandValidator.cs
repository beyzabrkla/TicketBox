using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Events.Commands;

namespace TicketBox.Application.Features.Events.Commands.Validators
{
    public class UpdateEventCommandValidator :AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200).WithMessage("Başlık boş geçilemez.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
            RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Kapasite en az 1 olmalıdır.");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori seçimi zorunludur.");

            //Etkinlik tarihini geçmişe çekilemez
            RuleFor(x => x.EventDate).GreaterThan(DateTime.Now).WithMessage("Geçmiş bir tarihe etkinlik güncellenemez.");
        }
    }
}
