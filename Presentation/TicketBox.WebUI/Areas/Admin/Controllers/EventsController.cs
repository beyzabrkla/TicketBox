using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Categories.Queries;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.ViewModels;

namespace TicketBox.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index (string? searchTerm, int? categoryId, bool? isActive, decimal? minPrice, decimal? maxPrice, bool upcoming = false, bool soldOut = false, int pageNumber = 1, int pageSize = 5)
        {
            // Filtre alanlarında kullanılacak kategori listesini getiriyoruz.
            ViewBag.Categories = await _mediator.Send(new GetCategoryQuery());
            ViewBag.Stats = await _mediator.Send(new GetEventStatsQuery()); // bu 

            // Kullanıcının seçtiği filtre bilgilerini FilterEventsQuery içerisine gönderiyoruz.
            // Eğer hiçbir filtre seçilmezse tüm etkinlikler listelenir, seçilen filtreler varsa yalnızca o kriterlere uyan etkinlikler getirilir.
            // Mediator sorgusunu sayfalama ve arama kriterleri ile güncelle
            var result = await _mediator.Send(new FilterEventsQuery
            {
                SearchTerm = searchTerm,
                CategoryId = categoryId,
                IsActive = isActive,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Upcoming = upcoming,
                SoldOut = soldOut,
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            var viewModel = new EventListViewModel
            {
                Events = result.Items,          // FilterEventsQuery'den dönen liste
                TotalCount = result.TotalCount, // Sayfalama ve sayaçlar için toplam sayı
                PageNumber = pageNumber,
                PageSize = pageSize,
                CreateEventCommand = new CreateEventCommand()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventListViewModel model)
        {
            var command = model.CreateEventCommand;

            try
            {
                await _mediator.Send(command);
                return RedirectToAction("Index");
            }
            catch (FluentValidation.ValidationException ex)
            {
                ModelState.Clear();

                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(
                        "CreateEventCommand." + error.PropertyName,
                        error.ErrorMessage
                    );
                }

                ViewBag.Categories = await _mediator.Send(new GetCategoryQuery());
                ViewBag.Stats = await _mediator.Send(new GetEventStatsQuery());

                var result = await _mediator.Send(new FilterEventsQuery());
                model.Events = result.Items;
                model.TotalCount = result.TotalCount; // Sayfa yapısının bozulmaması için 
                model.PageNumber = 1;
                model.PageSize = model.PageSize > 0 ? model.PageSize : 5;

                return View("Index", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var eventData = await _mediator.Send(new GetByIdEventQuery
            {
                Id = id
            });
            return Json(eventData);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EventListViewModel model)
        {
            var command = model.CreateEventCommand;
            // Formdaki hidden input'tan gelen EventId'yi alıyoruz
            int eventId = int.Parse(Request.Form["EventId"]);

            try
            {
                // Gelen veriyi UpdateEventCommand'e manuel mapliyoruz
                var updateCommand = new UpdateEventCommand
                {
                    EventId = eventId,
                    Title = command.Title,
                    Description = command.Description,
                    CategoryId = command.CategoryId,
                    Capacity = command.Capacity,
                    Price = command.Price,
                    Location = command.Location,
                    ImageUrl = command.ImageUrl,
                    IsActive = command.IsActive,
                    EventDate = command.EventDate ?? DateTime.Now
                };

                await _mediator.Send(updateCommand);
                return RedirectToAction("Index");
            }
            catch (FluentValidation.ValidationException ex)
            {
                ModelState.Clear();
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError("CreateEventCommand." + error.PropertyName, error.ErrorMessage);
                }

                ViewBag.Categories = await _mediator.Send(new GetCategoryQuery());
                ViewBag.Stats = await _mediator.Send(new GetEventStatsQuery());

                // Mediator'dan dönen result'ın .Items özelliğini atıyoruz.
                var result = await _mediator.Send(new FilterEventsQuery());
                model.Events = result.Items;
                model.TotalCount = result.TotalCount; // Sayfa yapısının bozulmaması için bunu da ekleyin
                model.PageNumber = 1;
                model.PageSize = model.PageSize > 0 ? model.PageSize : 5;

                return View("Index", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await _mediator.Send(new RemoveEventCommand { EventId = id });
            return RedirectToAction("Index");
        }
    }
}