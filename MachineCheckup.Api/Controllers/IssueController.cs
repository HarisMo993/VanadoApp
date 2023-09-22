using AutoMapper;
using MachineCheckup.Application.Dtos;
using MachineCheckup.Application.Interfaces;
using MachineCheckup.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace MachineCheckup.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        private readonly ILogger<IssueController> _logger;
        private readonly IMapper _mapper;

        public IssueController(IUnitOfWorkService unitOfWorkService, ILogger<IssueController> logger, IMapper mapper)
        {
            _unitOfWorkService = unitOfWorkService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetIssues()
        {
            try
            {
                var issues = await _unitOfWorkService.Issues.Get();
                var results = _mapper.Map<IList<IssueDto>>(issues.Items);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetIssues)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        [HttpGet("machine/{machineId}")]
        public async Task<IActionResult> GetIssuesByMachineId(int machineId, int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                Expression<Func<Issue, bool>> filter = issue => issue.MachineId == machineId;

                var pagedList = await _unitOfWorkService.Issues.Get(filter, pageSize: pageSize, pageNumber: pageNumber);

                var issueDtos = pagedList.Items.Select(issue => _mapper.Map<IssueDto>(issue));
                return Ok(issueDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(GetIssuesByMachineId)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }


        [HttpGet("top")]
        public async Task<IActionResult> GetTopIssues(int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                var sortedIssues = await _unitOfWorkService.Issues.Get(
                    pageSize: pageSize,
                    pageNumber: pageNumber,
                    orderBy: q => q.OrderBy(i => i.Priority).ThenByDescending(i => i.StartTime));

                var issueDtos = _mapper.Map<IEnumerable<IssueDto>>(sortedIssues.Items);

                return Ok(issueDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(GetTopIssues)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [HttpGet("{id:int}", Name = "GetIssue")]
        public async Task<IActionResult> GetIssue(int id)
        {
            try
            {
                var issue = await _unitOfWorkService.Issues.GetById(q => q.Id == id);
                var result = _mapper.Map<IssueDto>(issue);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetIssues)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueDto issueDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateIssue)}");
                return BadRequest(ModelState);
            }

            try
            {
                var issue = _mapper.Map<Issue>(issueDto);
                await _unitOfWorkService.Issues.Add(issue);
                await _unitOfWorkService.Save();

                return CreatedAtRoute("GetIssue", new { id = issue.Id }, issue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateIssue)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateIssue(int id, [FromBody] CreateIssueDto issueDto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateIssue)}");
                return BadRequest(ModelState);
            }

            try
            {
                var issue = await _unitOfWorkService.Issues.GetById(q => q.Id == id);
                if (issue == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateIssue)}");
                    return BadRequest(ModelState);
                }

                _mapper.Map(issueDto, issue);
                _unitOfWorkService.Issues.Update(issue);
                await _unitOfWorkService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateIssue)}");
                return StatusCode(500, "Interval server error. Please try again later.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteIssue)}");
                return BadRequest();
            }

            try
            {
                var issue = await _unitOfWorkService.Issues.GetById(q => q.Id == id);
                if (issue == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteIssue)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWorkService.Issues.Delete(id);
                await _unitOfWorkService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteIssue)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }
    }
}
