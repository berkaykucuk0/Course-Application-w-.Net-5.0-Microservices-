﻿using Course.Web.Models.Orders;
using Course.Web.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.GetAsync();

            ViewBag.basket = basket;
            return View(new CheckOutModel());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckOutModel checkOutModel)
        {
            //First way
              var orderStatus = await _orderService.CreateOrder(checkOutModel);
            if (!orderStatus.IsSuccess)
            {
                var basket = await _basketService.GetAsync();
                ViewBag.basket = basket;
                ViewBag.error = orderStatus.Error;
                return View();
            }
            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = orderStatus.OrderId });
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }
    }
}
