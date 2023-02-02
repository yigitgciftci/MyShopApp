using Alisveris.Business;
using Alisveris.Data;
using Alisveris.Data.Entities;
using Alisveris.Data.Models.Order;
using Alisveris.Data.Models.Product;
using Alisveris.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlisverisProjesi.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private DatabaseContext _context;
        private IUnitOfWork _unitOfWork;
        public OrderController(DatabaseContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult List()
        {            
            return Ok(_unitOfWork.Order.GetAll());
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Order order = _unitOfWork.Order.GetFirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                return NotFound(id);
            }

            return Ok(order);
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(int MemberId, OrderCreateModel model)
        {
            if (model == null)
            {
                return BadRequest(model);
            }
            Guid guid = Guid.NewGuid();
            string orderNumber = guid.ToString().Substring(0, 6);

            Order order = new Order();
            
            order.OrderNumber = orderNumber;
            order.OrderDate = DateTime.Now;
            order.ShoppingCard = model.ShoppingCard;
            order.MemberId = MemberId;
            order.Member = _context.Members.Where(x => x.Id == MemberId).FirstOrDefault();

            _unitOfWork.Order.Add(order);
            _unitOfWork.Save();

            return Created("", "");  
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult Edit([FromRoute] int id, [FromBody] OrderEditModel model)
        {
            Order order = _unitOfWork.Order.GetFirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                return NotFound(); 
            }

            _unitOfWork.Order.Update(order);
            _unitOfWork.Save();

            return Ok(order);  
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            Order order = _unitOfWork.Order.GetFirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            _unitOfWork.Order.Remove(order);
            _unitOfWork.Save();

            return Ok();  
        }
    }
}
