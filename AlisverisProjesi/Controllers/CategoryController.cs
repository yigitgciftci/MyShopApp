using Alisveris.Business;
using Alisveris.Business.Models;
using Alisveris.Business.Models.Category;
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
    public class CategoryController : ControllerBase
    {
        private ICategoryManager _categoryManager;
        private IUnitOfWork _unitOfWork;

        public CategoryController(ICategoryManager categoryManager, IUnitOfWork unitOfWork)
        {
            _categoryManager = categoryManager;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult List()
        {            
            return Ok(_unitOfWork.Category.GetAll());
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Category category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return NotFound(id);
            }

            return Ok(category);
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(CategoryCreateModel model)
        {
            if (model == null)
            {
                return BadRequest(model);
            }

            Category category = new Category();
            category.Name = model.Name;

            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();

            return Created("", category);
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult Edit([FromRoute] int id, [FromBody] CategoryEditModel model)
        {
            Category category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = model.Name;
            category.SubcategoryId = model.SubcategoryId;

            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();

            return Ok();
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {            
            Category category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();

            return Ok();
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult AddSubCategory(int id, SubCategoryCreateModel model)
        {
            _categoryManager.AddSubCategory(id, model);
            return Ok();
        }
    }
}
