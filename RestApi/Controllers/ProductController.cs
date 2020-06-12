using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using RestApi.Database;
using RestApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Json(_appDbContext.Products.ToList());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ProductExists(id)) return NotFound();
            return Json(_appDbContext.Products.Find(id));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _appDbContext.Products.Add(product);
                    _appDbContext.SaveChanges();
                    return Json(CreatedAtAction(nameof(Get), new { id = product.Id }, product));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return FormatModelStateErrors(ModelState);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != product.Id)
                    {
                        return BadRequest();
                    }

                    _appDbContext.Entry(product).State = EntityState.Modified;
                    _appDbContext.SaveChanges();
                    return Json(CreatedAtAction(nameof(Get), new { id = product.Id }, product));
                }
                else
                {
                    return FormatModelStateErrors(ModelState);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            return new HttpResponseMessage(HttpStatusCode.NotImplemented);
        }

        private bool ProductExists(long id) =>
            _appDbContext.Products.Any(e => e.Id == id);

        private IActionResult FormatModelStateErrors(ModelStateDictionary model) =>
            Json(model.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
    }
}
