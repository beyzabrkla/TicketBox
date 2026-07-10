using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Categories.Commands;
using TicketBox.Application.Features.Categories.Queries;

namespace TicketBox.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        public IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            // Mediator ile listeyi çekiyoruz
            var values = await _mediator.Send(new GetCategoryQuery());
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                await _mediator.Send(command);
                return RedirectToAction("Index");
            }
            catch (FluentValidation.ValidationException ex)
            {
                // ValidationBehavior'dan gelen hataları ModelState'e ekleyelim
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(command);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var value = await _mediator.Send(new GetByIdCategoryQuery { CategoryId = id });
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryCommand command)
        {
            if (!ModelState.IsValid) return View(command);

            try
            {
                await _mediator.Send(command);
                return RedirectToAction("Index");
            }
            catch (FluentValidation.ValidationException ex)
            {
                foreach (var error in ex.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(command);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
                return View(command);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await _mediator.Send(new RemoveCategoryCommand { CategoryId = id });
            return RedirectToAction("Index", "Categories", new { area = "Admin" });
        }
    }
}
