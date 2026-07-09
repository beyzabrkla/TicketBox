using Microsoft.AspNetCore.Mvc;
using TicketBox.Application.Features.Categories.Commands;
using TicketBox.Application.Features.Categories.Handlers;

namespace TicketBox.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler;
        private readonly UpdateCategoryCommandHandler _updateCategoryCommandHandler;
        private readonly RemoveCategoryCommandHandler _removeCategoryCommandHandler;
        private readonly GetCategoryQueryHandler _getCategoryQueryHandler;
        private readonly GetByIdCategoryQueryHandler _getByIdCategoryQueryHandler;

        public CategoryController(CreateCategoryCommandHandler createCategoryCommandHandler,
                                  UpdateCategoryCommandHandler updateCategoryCommandHandler,
                                  RemoveCategoryCommandHandler removeCategoryCommandHandler,
                                  GetCategoryQueryHandler getCategoryQueryHandler, 
                                  GetByIdCategoryQueryHandler getByIdCategoryQueryHandler)
        {
            _createCategoryCommandHandler = createCategoryCommandHandler;
            _updateCategoryCommandHandler = updateCategoryCommandHandler;
            _removeCategoryCommandHandler = removeCategoryCommandHandler;
            _getCategoryQueryHandler = getCategoryQueryHandler;
            _getByIdCategoryQueryHandler = getByIdCategoryQueryHandler;
        }

        public async Task<IActionResult> CategoryList()
        {
            var values = await _getCategoryQueryHandler.Handle();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand createCategoryCommand)
        {
            await _createCategoryCommandHandler.Handle(createCategoryCommand);
            return RedirectToAction("CategoryList");
        }
    }
}
