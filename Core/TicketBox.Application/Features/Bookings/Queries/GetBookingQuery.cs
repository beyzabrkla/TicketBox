using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Bookings.Results;

namespace TicketBox.Application.Features.Bookings.Queries
{
    public class GetBookingQuery :IRequest<List<GetBookingQueryResult>> //bu sınıf MediatR kütüphanesini kullandığı bir sorgu sınıfıdır.
                                                                      //IRequest arayüzünü implement ederek, bu sorgunun bir yanıt döndüreceğini belirtir.
                                                                      //Yanıt tipi olarak List<GetBookingQueryResult> kullanılmıştır, yani bu sorgu çalıştırıldığında bir GetBookingQueryResult nesnelerinin listesi dönecektir.
    {
    }
}
