using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketingSystemIT.Core;
using TicketingSystemIT.Entities.Resources;
using Microsoft.AspNetCore.Authorization;
using TicketingSystemIT.Entities.Models;

namespace TicketingSystemIT.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ICategoryRepository repository;
    public CategoryController(IMapper mapper, IUnitOfWork unitOfWork, ICategoryRepository repository)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.repository = repository;
    }

    //[Authorize(Policy = Policies.Employee)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await repository.GetCategory(id);
        if (category == null)
            return NotFound();

        var result = mapper.Map<Category, CategoryResource>(category);
        return Ok(result);
    }
    //[Authorize(Policy = Policies.Employee)]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await repository.GetCategories();
        var result = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
        return Ok(result);
    }

    [Authorize(Policy = Policies.ITEmployee)]
    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory([FromBody] CategorySaveResource categorySaveResource)
    {
        if (!ModelState.IsValid)
            return BadRequest();


        var category = mapper.Map<CategorySaveResource, Category>(categorySaveResource);
        repository.Add(category);

        await unitOfWork.CompleteAsync();

        category = await repository.GetCategory(category.Id);
        var result = mapper.Map<Category, CategoryResource>(category);
        return Created(nameof(GetCategory), result);
    }
    [Authorize(Policy = Policies.ITEmployee)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategorySaveResource categoryResource)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var category = await repository.GetCategory(id);
        if (category == null)
            return NotFound();

        mapper.Map<CategorySaveResource, Category>(categoryResource, category);

        await unitOfWork.CompleteAsync();

        category = await repository.GetCategory(category.Id);
        var result = mapper.Map<Category, CategoryResource>(category);
        return Accepted(result);
    }
    [Authorize(Policy = Policies.ITEmployee)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await repository.GetCategory(id);

        if (category == null)
            return NotFound();

        repository.Remove(category);
        await unitOfWork.CompleteAsync();

        return Accepted();//202
    }
}