using AutoMapper;
using ECommerce.Core;
using ECommerce.Core.model;
using ECommerce.Core.Repositories;
using ECommerce.Core.Specifications;
using ECommerceApi.DTOs;
using ECommerceApi.Errors;
using ECommerceApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductController : ControllerBase
    {
        public IUnitOfWork UnitOfWork { get; }
        public IMapper _mapper { get; }

        public ProductController(IUnitOfWork unitOfWork ,IMapper mapper )
        {
            UnitOfWork = unitOfWork;
            _mapper = mapper;      
        }

        [HttpGet]

        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAll([FromQuery] ProductSpecParams Params)
        {
            var Spec = new ProductWithBrandAndTypeSpecification(Params);
            var products = await UnitOfWork.Repository<Product>().GetAllWithSpenAsync(Spec); 
            var MappedProducts = _mapper.Map<IReadOnlyList<Product> , IReadOnlyList<ProductToReturnDto>>(products);
            var CountSpec = new ProductWithFilterationForCountSpec(Params);
            var Count = await UnitOfWork.Repository<Product>().GetCountWithSpecAsync(CountSpec);
            return Ok(new Pagination<ProductToReturnDto>(Params.PageSize , Params.PageIndex , MappedProducts ,Count));    

        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllProductBrand()
        {

            var brands = await UnitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);

        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await UnitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await UnitOfWork.Repository<Product>().GetByIdWithSpenAsync(spec);
            if (product is null)
                return NotFound(new ApiResponse(404));
            var MappedProduct = _mapper.Map<Product,ProductToReturnDto>(product);
          
            return Ok(MappedProduct);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductUpdateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            if (productDto.PictureUrl != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + productDto.PictureUrl.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await productDto.PictureUrl.CopyToAsync(fileStream);
                }

                product.img = uniqueFileName;
            }

            await UnitOfWork.Repository<Product>().AddAsync(product);
            var result = await UnitOfWork.CompleteAsync();

            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Failed to create the product"));

            var productToReturn = _mapper.Map<ProductToReturnDto>(product);

            return CreatedAtAction(nameof(GetById), new { id = product.id }, productToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductUpdateDto productDto)
        {
            var product = await UnitOfWork.Repository<Product>().GetByIdAsync(id);
            if (product == null) return NotFound(new ApiResponse(404));

            _mapper.Map(productDto, product);

            if (productDto.PictureUrl != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + productDto.PictureUrl.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await productDto.PictureUrl.CopyToAsync(fileStream);
                }

                // حذف الصورة القديمة إذا وجدت
                if (!string.IsNullOrEmpty(product.img))
                {
                    var oldFilePath = Path.Combine(uploadsFolder, product.img);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                product.img = uniqueFileName;
            }

            UnitOfWork.Repository<Product>().UpdateAsync(product, id);
            var result = await UnitOfWork.CompleteAsync();

            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Failed to update the product"));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                UnitOfWork.Repository<Product>().DeleteAsync(id);
                var result = await UnitOfWork.CompleteAsync();

                if (result <= 0)
                    return BadRequest(new ApiResponse(400, "Failed to delete the product"));

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new ApiResponse(404, ex.Message));
            }
        }
    }
}
