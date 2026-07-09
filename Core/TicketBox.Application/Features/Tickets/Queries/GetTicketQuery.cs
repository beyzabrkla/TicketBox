using MediatR;
using TicketBox.Application.Features.Tickets.Results;

namespace TicketBox.Application.Features.Tickets.Queries
{
    public class GetTicketQuery :IRequest<List<GetTicketQueryResult>> //bu sınıf MediatR kütüphanesini kullandığı bir sorgu sınıfıdır.
                                                                      //IRequest arayüzünü implement ederek, bu sorgunun bir yanıt döndüreceğini belirtir.
                                                                      //Yanıt tipi olarak List<GetTicketQueryResult> kullanılmıştır, yani bu sorgu çalıştırıldığında bir GetTicketQueryResult nesnelerinin listesi dönecektir.
    {
    }
}
