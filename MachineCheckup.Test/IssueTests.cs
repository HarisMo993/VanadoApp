using AutoMapper;
using Castle.Core.Logging;
using MachineCheckup.Api.Controllers;
using MachineCheckup.Application.Dtos;
using MachineCheckup.Application.Interfaces;
using MachineCheckup.Domain.Entities;
using MachineCheckup.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace MachineCheckup.Test
{
    public class IssueTests
    {
        private readonly Mock<IUnitOfWorkService> _mockUnitOfWorkService;
        private readonly ILogger<IssueController> _logger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IssueController _controller;

        public IssueTests()
        {
            _mockUnitOfWorkService = new Mock<IUnitOfWorkService>();
            _logger = Mock.Of<ILogger<IssueController>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new IssueController(_mockUnitOfWorkService.Object, _logger, _mockMapper.Object);
        }

        [Fact]
        public async Task GetIssues_ReturnsOkResult()
        {

            _mockUnitOfWorkService.Setup(repo => repo.Issues.Get(
                It.IsAny<Expression<Func<Issue, bool>>>(),
                It.IsAny<Func<IQueryable<Issue>, IOrderedQueryable<Issue>>>(),
                It.IsAny<List<string>>(),
                It.IsAny<int>(),
                It.IsAny<int>()
            )).ReturnsAsync(new PagedList<Issue>());


            var result = await _controller.GetIssues();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetTopIssues_ReturnsOkResult()
        {

            _mockUnitOfWorkService.Setup(repo => repo.Issues.Get(
                It.IsAny<Expression<Func<Issue, bool>>>(),
                It.IsAny<Func<IQueryable<Issue>, IOrderedQueryable<Issue>>>(),
                It.IsAny<List<string>>(),
                It.IsAny<int>(),
                It.IsAny<int>()
            )).ReturnsAsync((Expression<Func<Issue, bool>> expression, Func<IQueryable<Issue>, IOrderedQueryable<Issue>> orderBy, List<string> includes, int pageSize, int pageNumber) =>
            {
                return new PagedList<Issue>();
            });


            var result = await _controller.GetTopIssues();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetIssue_ReturnsOkResult()
        {
            var issueId = 1;

            _mockUnitOfWorkService.Setup(repo => repo.Issues.Get(
                It.IsAny<Expression<Func<Issue, bool>>>(),
                It.IsAny<Func<IQueryable<Issue>, IOrderedQueryable<Issue>>>(),
                It.IsAny<List<string>>(),
                It.IsAny<int>(),
                It.IsAny<int>()
            )).ReturnsAsync(new PagedList<Issue>());


            var result = await _controller.GetIssue(issueId);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
