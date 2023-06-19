using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private ApiResponse _response;
        private readonly legoShopContext _context;

        public ProductController(IProductRepository repo, IMapper mapper, ApiResponse response, legoShopContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _response = response;
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            List<Product> products = _repo.GetProduct();
            if (products == null || products.Count == 0)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Can't found any product!");
                return NotFound(_response);
            }
            _response.Result = products;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Product> Get(int id)
        {
            Product pro = _repo.GetProductById(id);
            if (pro == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Can't found any product!");
                return NotFound(_response);
            }
            _response.Result = pro;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductCreateDTO p, IFormFile uploadimg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (uploadimg == null || uploadimg.Length == 0)
                    {
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("Image is required!");
                        return BadRequest(_response);
                    }

                // Lưu trữ file ảnh vào thư mục hoặc dịch vụ lưu trữ của bạn
                string imagePath = "wwwroot/images/showroom" + Guid.NewGuid().ToString() + "_" + uploadimg.FileName;
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await uploadimg.CopyToAsync(stream);
                }

                // Tạo đối tượng Image và lưu thông tin ảnh vào cơ sở dữ liệu
                BusinessObject.Models.Image image = new BusinessObject.Models.Image
                {
                    Img = imagePath.Replace("wwwroot", ""),
                    ProductId = p.ProductId
                };
                _context.Images.Add(image);
                await _context.SaveChangesAsync();
        
                var newProduct = _mapper.Map<Product>(p);
                    newProduct.Images = (ICollection<BusinessObject.Models.Image>)image;
                    _repo.CreateNewProduct(newProduct);
                    _response.Result = newProduct;
                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add($"{ModelState.Values.Select(e => e.Errors).ToList()}");
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return BadRequest(_response);

        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var p = _repo.GetProductById(id);
                if (p == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Not Found");
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                _repo.DeleteProduct(p);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = p;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return BadRequest(_response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] ProductUpdateDTO p, IFormFile uploadimg)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var product = _repo.GetProductById(p.ProductId);
                    if (product == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.ErrorMessages.Add("Not Found");
                        return NotFound(_response);
                    }
                    product.ProductId = p.ProductId;
                    product.Name = p.Name;
                    product.Price = p.Price;
                    product.Rating = p.Rating;
                    product.Status = p.Status;
                    product.Price = p.Price;
                    product.Quantity = p.Quantity;
                    product.Piece = p.Piece;
                    product.Age = p.Age;
                    if (uploadimg != null && uploadimg.Length > 0)
                    {
                        var oldImage = await _context.Images.FirstOrDefaultAsync(img => img.ProductId == p.ProductId);
                        if (oldImage != null)
                        {
                            _context.Images.Remove(oldImage);
                        }
                        // Lưu trữ file ảnh vào thư mục hoặc dịch vụ lưu trữ của bạn
                        // Ví dụ: sử dụng thư viện FileHelper để lưu trữ ảnh trong thư mục "wwwroot/images/showroom"
                        string imagePath = "wwwroot/images/showroom" + Guid.NewGuid().ToString() + "_" + uploadimg.FileName;
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await uploadimg.CopyToAsync(stream);
                        }

                        // Tạo đối tượng ImageShowroom và lưu thông tin ảnh vào cơ sở dữ liệu
                        BusinessObject.Models.Image image = new BusinessObject.Models.Image
                        {
                            Img = imagePath.Replace("wwwroot", ""),
                            ProductId = p.ProductId
                        };
                        _context.Images.Add(image);
                        _repo.UpdateProduct(product);
                        _response.IsSuccess = true;
                        _response.StatusCode = HttpStatusCode.OK;
                        _response.Result = p;
                        return Ok(_response);
                    }
                } 
                else 
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add($"{ModelState.Values.Select(e => e.Errors).ToList()}");
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return BadRequest(_response);
            }
            return BadRequest(_response);
        }
    }
}
