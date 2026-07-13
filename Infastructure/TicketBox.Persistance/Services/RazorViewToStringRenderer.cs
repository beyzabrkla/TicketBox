using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using TicketBox.Application.Interfaces;

namespace TicketBox.Persistance.Services
{
    public class RazorViewToStringRenderer : IRazorViewToStringRenderer
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public RazorViewToStringRenderer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            // Kendi Scope'umuzu burada oluşturuyoruz.
            // Bu sayede renderer, ana isteğin DbContext'inden bağımsız çalışır.
            using (var scope = _scopeFactory.CreateScope())
            {
                var viewEngine = scope.ServiceProvider.GetRequiredService<IRazorViewEngine>();
                var tempDataProvider = scope.ServiceProvider.GetRequiredService<ITempDataProvider>();

                // HTTP Context'i bu yeni scope'tan türetiyoruz
                var httpContext = new DefaultHttpContext { RequestServices = scope.ServiceProvider };
                var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

                using var sw = new StringWriter();
                var viewResult = viewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                    throw new ArgumentNullException($"'{viewName}' bulunamadı.");

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model };
                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(httpContext, tempDataProvider), sw, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}