using AutoMapper;
using MachineCheckup.Application.Dtos;
using MachineCheckup.Application.Interfaces;
using MachineCheckup.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MachineCheckup.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        private readonly ILogger<MachineController> _logger;
        private readonly IMapper _mapper;

        public MachineController(IUnitOfWorkService unitOfWorkService, ILogger<MachineController> logger, IMapper mapper)
        {
            _unitOfWorkService = unitOfWorkService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMachines()
        {
            try
            {
                var machines = await _unitOfWorkService.Machines.Get();
                var results = _mapper.Map<IList<MachineDto>>(machines.Items);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetMachines)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        [HttpGet("{id:int}", Name = "GetMachine")]
        public async Task<IActionResult> GetMachine(int id)
        {
            try
            {
                var machine = await _unitOfWorkService.Machines.GetById(q => q.Id == id);
                if (machine == null)
                    return NotFound();

                var machineDto = _mapper.Map<MachineDto>(machine);

                var issues = await _unitOfWorkService.Issues.Get(q => q.MachineId == machine.Id);

                if (issues.Items != null && issues.Items.Any())
                {
                    var totalDuration = issues.Items
                        .Where(i => i.EndTime.HasValue && i.StartTime.HasValue)
                        .Sum(i => (i.EndTime.Value - i.StartTime.Value).TotalHours);

                    if (totalDuration > 0 && issues.Items.Count() > 0)
                    {
                        machineDto.AverageIssueDuration = totalDuration / issues.Items.Count();
                    }
                    else
                    {
                        machineDto.AverageIssueDuration = 0;
                    }
                }

                machineDto.Issues = _mapper.Map<List<IssueDto>>(issues.Items);

                return Ok(machineDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetMachine)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateMachine([FromBody] CreateMachineDto machineDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateMachine)}");
                return BadRequest(ModelState);
            }

            try
            {
                var existingMachine = await _unitOfWorkService.Machines.GetById(q => q.Name == machineDto.Name);
                if (existingMachine != null)
                {
                    ModelState.AddModelError("Name", "Machine with the same name already exists.");
                    return BadRequest(ModelState);
                }

                var machine = _mapper.Map<Machine>(machineDto);
                await _unitOfWorkService.Machines.Add(machine);
                await _unitOfWorkService.Save();

                return CreatedAtRoute("GetMachine", new { id = machine.Id }, machine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateMachine)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMachine(int id, [FromBody] CreateMachineDto machineDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMachine)}");
                return BadRequest(ModelState);
            }

            try
            {
                var existingMachine = await _unitOfWorkService.Machines.GetById(q => q.Name == machineDto.Name);
                if (existingMachine != null)
                {
                    ModelState.AddModelError("Name", "Machine with the same name already exists.");
                    return BadRequest(ModelState);
                }
                var machine = await _unitOfWorkService.Machines.GetById(q => q.Id == id);
                if (machine == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMachine)}");
                    return BadRequest(ModelState);
                }

                _mapper.Map(machineDto, machine);
                _unitOfWorkService.Machines.Update(machine);
                await _unitOfWorkService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateMachine)}");
                return StatusCode(500, "Interval server error. Please try again later.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMachine)}");
                return BadRequest();
            }

            try
            {
                var machine = await _unitOfWorkService.Machines.GetById(q => q.Id == id);
                if (machine == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMachine)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWorkService.Machines.Delete(id);
                await _unitOfWorkService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteMachine)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }
    }
}
