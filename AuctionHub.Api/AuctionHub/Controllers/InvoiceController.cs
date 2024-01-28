﻿using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHub.Controllers
{
    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("generate-invoice")]
        public async Task<IActionResult> GenerateInvoiceAsync([FromBody] BiddingRoom biddingRoom)
        {
            var response = await _invoiceService.GenerateInvoiceAsync(biddingRoom);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
