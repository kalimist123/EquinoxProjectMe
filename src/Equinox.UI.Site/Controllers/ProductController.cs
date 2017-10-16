using System;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.Site.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService, 
                                  INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("product-management/list-all")]
        public IActionResult Index()
        {
            return View(_productAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("product-management/product-details/{id:guid}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = _productAppService.GetById(id.Value);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanWriteProductData")]
        [Route("product-management/register-new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteProductData")]
        [Route("product-management/register-new")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(productViewModel);
            _productAppService.Register(productViewModel);

            if (IsValidOperation())
                ViewBag.Sucesso = "Product Registered!";

            return View(productViewModel);
        }
        
        [HttpGet]
        [Authorize(Policy = "CanWriteProductData")]
        [Route("product-management/edit-product/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = _productAppService.GetById(id.Value);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteProductData")]
        [Route("product-management/edit-product/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(productViewModel);

            _productAppService.Update(productViewModel);

            if (IsValidOperation())
                ViewBag.Sucesso = "Product Updated!";

            return View(productViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanRemoveProductData")]
        [Route("product-management/remove-product/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = _productAppService.GetById(id.Value);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "CanRemoveProductData")]
        [Route("product-management/remove-product/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _productAppService.Remove(id);

            if (!IsValidOperation()) return View(_productAppService.GetById(id));

            ViewBag.Sucesso = "Product Removed!";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("product-management/product-history/{id:guid}")]
        public JsonResult History(Guid id)
        {
            var productHistoryData = _productAppService.GetAllHistory(id);
            return Json(productHistoryData);
        }
    }
}
