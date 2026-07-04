using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Tickets.Results;

namespace TicketBox.Application.Features.Mediator.Tickets.Queries
{
    public class GetTicketQuery :IRequest<List<GetTicketQueryResult>> //bu sınıf MediatR kütüphanesini kullandığı bir sorgu sınıfıdır.
                                                                      //IRequest arayüzünü implement ederek, bu sorgunun bir yanıt döndüreceğini belirtir.
                                                                      //Yanıt tipi olarak List<GetTicketQueryResult> kullanılmıştır, yani bu sorgu çalıştırıldığında bir GetTicketQueryResult nesnelerinin listesi dönecektir.
    {
    }
}
