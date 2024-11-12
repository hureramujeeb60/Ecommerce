using Microsoft.AspNetCore.Mvc;
using Ecommerce.Service;
using Ecommerce.Helper;
using AutoMapper;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T, TDto> : ControllerBase, IGenericController<T, TDto> where T : class where TDto : class
    {
        private readonly IGenericService<T> _genericService;
        private readonly IMapper _mapper;

        public GenericController(IGenericService<T> genericService, IMapper mapper)
        {
            _genericService = genericService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TDto>>> GetAllAsync()
        {
            try
            {
                var result = await _genericService.GetAllAsync();
                if (!result.Success)
                {
                    return NotFound(result.Message);
                }

                var dtos = _mapper.Map<IEnumerable<TDto>>(result.Data);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, MessageHelper.ErrorOccured(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TDto>> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("The id is invalid");
                }

                var result = await _genericService.GetByIdAsync(id);
                if (!result.Success)
                {
                    return NotFound(result.Message);
                }

                var dto = _mapper.Map<TDto>(result.Data);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, MessageHelper.ErrorOccured(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TDto>> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("The id is invalid");
                }

                var result = await _genericService.Delete(id);
                if (!result.Success)
                {
                    return NotFound(result.Message);
                }

                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, MessageHelper.ErrorOccured(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<TDto>> AddAsync([FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var mapEntity = _mapper.Map<T>(entity);
                var result = await _genericService.AddAsync(mapEntity);

                if (!result.Success)
                {
                    return Conflict(result.Message);
                }

                var createdDto = _mapper.Map<TDto>(result.Data);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = (result.Data as dynamic).Id }, createdDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, MessageHelper.ErrorOccured(ex.Message));
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TDto>> Update(int id, [FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (id <= 0)
                {
                    return BadRequest("The id is invalid");
                }

                var mapEntity = _mapper.Map<T>(entity);
                var result = await _genericService.Update(id, mapEntity);

                if (!result.Success)
                {
                    return NotFound(result.Message);
                }

                var updatedDto = _mapper.Map<TDto>(result.Data);
                return Ok(updatedDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, MessageHelper.ErrorOccured(ex.Message));
            }
        }
    }
}
