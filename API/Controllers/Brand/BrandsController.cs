using Application.Features.Commands.BrandCommands.CreateBrand;
using Application.Features.Commands.BrandCommands.DeleteBrand;
using Application.Features.Commands.BrandCommands.UpdateBrand;
using Application.Features.Queries.BrandQueries.GetAllBrand;
using Application.Features.Queries.BrandQueries.GetByIDBrand;
using Application.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Brand
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandCommandRequest createBrandCommandRequest)
        {
            Result response = await _mediator.Send(createBrandCommandRequest);
            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteBrandCommandRequest deleteBrandCommandRequest)
        {
            Result response = await _mediator.Send(deleteBrandCommandRequest);
            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBrandCommandRequest updateBrandCommandRequest)
        {
            Result response = await _mediator.Send(updateBrandCommandRequest);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllBrandQueryRequest getAllBrandsQueryRequest)
        {
            var response = await _mediator.Send(getAllBrandsQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(GetByIDBrandQueryRequest getByIDBrandQueryRequest)
        {
            var response = await _mediator.Send(getByIDBrandQueryRequest);
            return Ok(response);
        }
    }
}
