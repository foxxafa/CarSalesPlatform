using Application.Features.Commands.CategoryCommands.CreateCategory;
using Application.Features.Commands.CategoryCommands.DeleteCategory;
using Application.Features.Commands.CategoryCommands.UpdateCategory;
using Application.Features.Queries.CategoryQueries.GetAllCategory;
using Application.Features.Queries.CategoryQueries.GetByID;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Category
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommandRequest createCategoryCommandRequest)
        {
            var response = await _mediator.Send(createCategoryCommandRequest);
            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteCategoryCommandRequest deleteCategoryCommandRequest)
        {
            var response = await _mediator.Send(deleteCategoryCommandRequest);
            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryCommandRequest updateCategoryCommandRequest)
        {
            var response = await _mediator.Send(updateCategoryCommandRequest);
            return Ok(response);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllCategoryQueryRequest getAllCategoryQueryRequest)
        {
            var response = await _mediator.Send(getAllCategoryQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(GetByIDCategoryQueryRequest getByIDCategoryQueryRequest)
        {
            var response = await _mediator.Send(getByIDCategoryQueryRequest);
            return Ok(response);
        }
    }
}
