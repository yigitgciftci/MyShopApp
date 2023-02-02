using Alisveris.Business;
using Alisveris.Data;
using Alisveris.Data.Entities;
using Alisveris.Data.Models.Product;
using Alisveris.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlisverisProjesi.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private DatabaseContext _context;
        private IUnitOfWork _unitOfWork;
        public ProductController(DatabaseContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult List()
        {            
            return Ok(_unitOfWork.Product.GetAll());
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {            
            Product product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound(id);
            }

            return Ok(product);
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(ProductCreateModel model)
        {
            if (model == null)
            {
                return BadRequest(model); 
            }

            Product product = new Product();
            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Picture = model.Picture;
            product.BarCode = model.BarCode;

            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();

            return Created("", ""); 
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult Edit([FromRoute] int id, [FromBody] ProductEditModel model)
        {
            Product product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();

            return Ok(product);   
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            Product product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();

            return Ok();    
        }
    }
}
