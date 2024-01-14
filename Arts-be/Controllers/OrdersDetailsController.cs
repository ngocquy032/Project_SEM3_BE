using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arts_be.Models;

namespace Arts_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersDetailsController : ControllerBase
    {
        private readonly EProjectContext _context;

        public OrdersDetailsController(EProjectContext context)
        {
            _context = context;
        }

        // GET: api/OrdersDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersDetail>>> GetOrdersDetails()
        {
          if (_context.OrdersDetails == null)
          {
              return NotFound();
          }
            return await _context.OrdersDetails.ToListAsync();
        }

        // GET: api/OrdersDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersDetail>> GetOrdersDetail(int id)
        {
          if (_context.OrdersDetails == null)
          {
              return NotFound();
          }
            var ordersDetail = await _context.OrdersDetails.FindAsync(id);

            if (ordersDetail == null)
            {
                return NotFound();
            }

            return ordersDetail;
        }

        // PUT: api/OrdersDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdersDetail(int id, OrdersDetail ordersDetail)
        {
            if (id != ordersDetail.OrderDetailsId)
            {
                return BadRequest();
            }

            _context.Entry(ordersDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrdersDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdersDetail>> PostOrdersDetail(OrdersDetail ordersDetail)
        {
          if (_context.OrdersDetails == null)
          {
              return Problem("Entity set 'EProjectContext.OrdersDetails'  is null.");
          }
            _context.OrdersDetails.Add(ordersDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdersDetail", new { id = ordersDetail.OrderDetailsId }, ordersDetail);
        }

        // DELETE: api/OrdersDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdersDetail(int id)
        {
            if (_context.OrdersDetails == null)
            {
                return NotFound();
            }
            var ordersDetail = await _context.OrdersDetails.FindAsync(id);
            if (ordersDetail == null)
            {
                return NotFound();
            }

            _context.OrdersDetails.Remove(ordersDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersDetailExists(int id)
        {
            return (_context.OrdersDetails?.Any(e => e.OrderDetailsId == id)).GetValueOrDefault();
        }
    }
}
