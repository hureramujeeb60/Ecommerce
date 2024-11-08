[ApiController]
[Route("api/[controller]")]
public class GenericController<T, TDto> : ControllerBase where T : class where TDto : class
{
    private readonly IGenericService<T> _service;
    private readonly IMapper _mapper;  // For mapping between models and DTOs

    public GenericController(IGenericService<T> service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null) return NotFound();

        var dto = _mapper.Map<TDto>(entity);
        return Ok(dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entities = await _service.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<TDto>>(entities);
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var entity = _mapper.Map<T>(dto);
        await _service.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = (entity as dynamic).Id }, dto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var entity = _mapper.Map<T>(dto);
        if ((entity as dynamic).Id != id) return BadRequest("ID mismatch");

        _service.Update(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null) return NotFound();

        _service.Delete(entity);
        return NoContent();
    }
}
