using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.PizzaApi.Database;
using MTKDotNetCore.PizzaApi.Queries;
using MTKDotNetCore.Shared;

namespace MTKDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly DapperService _dapperService;

        private string GenerateInvoiceNumber ()
        {
            DateTime now = DateTime.Now;
            // generate invoice number from year, month, hour, minute and second of Date.now
            string invoiceNum = $"{now:yyyyMMddHHmmss}";
            return invoiceNum;
        }

        public PizzaController ()
        {
            _context = new AppDbContext();
            _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync ()
        {
            var lst = await _context.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtrasAsync ()
        {
            var lst = await _context.PizzaExtras.ToListAsync();
            return Ok(lst);
        }

        //[HttpGet("Order/{invoiceNum}")]
        //public async Task<IActionResult> GetOrder (string invoiceNum)
        //{
        //    var item = await _context.PizzaOrders.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNum);
        //    var lst = await _context.PizzaOrderDetails.Where(x => x.PizzaOrderInvoiceNo == invoiceNum).ToListAsync();

        //    return Ok(new
        //    {
        //        Order = item,
        //        OrderDetail = lst
        //    });
        //}

        [HttpGet("Order/{invoiceNum}")]
        public IActionResult GetOrder (string invoiceNum)
        {
            var item = _dapperService.QueryFirstOrDefault<PizzaOrderInvoiceHeadModel>(PizzaQuery.PizzaOrderQuery, new { PizzaOrderInvoiceNo = invoiceNum} );
            var lst = _dapperService.Query<PizzaOrderDetailInvoiceHeadModel>(PizzaQuery.PizzaOrderDetailQuery, new { PizzaOrderInvoiceNo = invoiceNum });

            var model = new PizzaOrderInvoiceResponse
            {
                Order = item,
                OrderDetail = lst
            };

            return Ok(model);
        }

        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync (OrderRequest orderRequest)
        {
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            var total = pizza!.Price;

            if (orderRequest.ExtraIds.Length > 0)
            {
                // select * from Tbl_PizzaExtra where PizzaExtraId in (1,2,3,4);
                var lstExtra = await _context.PizzaExtras.Where(x => orderRequest.ExtraIds.Contains(x.Id)).ToListAsync();
                total += lstExtra.Sum(x => x.Price);
            }

            var invoiceNum = GenerateInvoiceNumber();

            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            {
                PizzaId = orderRequest.PizzaId,
                PizzaOrderInvoiceNo = invoiceNum,
                TotalAmount = total
            };

            List<PizzaOrderDetailModel> pizzaExtraModels = orderRequest.ExtraIds.Select(extraId => new PizzaOrderDetailModel
            {
                PizzaExtraId = extraId,
                PizzaOrderInvoiceNo = invoiceNum
            }).ToList();

            await _context.PizzaOrders.AddAsync(pizzaOrderModel);
            await _context.PizzaOrderDetails.AddRangeAsync(pizzaExtraModels);
            await _context.SaveChangesAsync();

            OrderResponse response = new OrderResponse()
            {
                Message = "Thank you for your order! Enjoy your pizza!",
                InvoiceNo = invoiceNum,
                TotalAmount = total
            };

            return Ok(response);
        }
    }
}
