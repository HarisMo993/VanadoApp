using AutoMapper;
using MachineCheckup.Api.Controllers;
using MachineCheckup.Application.Dtos;
using MachineCheckup.Application.Interfaces;
using MachineCheckup.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace MachineCheckup.Test
{
    public class MachineTests
    {
        private readonly Mock<IUnitOfWorkService> _mockUnitOfWorkService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly MachineController _controller;
        private readonly ILogger<MachineController> _logger;

        public MachineTests()
        {
            _mockUnitOfWorkService = new Mock<IUnitOfWorkService>();
            _mockMapper = new Mock<IMapper>();
            _logger = Mock.Of<ILogger<MachineController>>();
            _controller = new MachineController(_mockUnitOfWorkService.Object, _logger, _mockMapper.Object);
        }

        [Fact]
        public async Task GetMachines_ReturnsOkResult()
        {
            var machines = new List<Machine>();
            var machineDtos = new List<MachineDto>();
            _mockUnitOfWorkService
            .Setup(repo => repo.Machines.Get(It.IsAny<Expression<Func<Machine, bool>>>(), null, null, 10, 1))
            .ReturnsAsync(new PagedList<Machine>
            {
                Items = machines,
                PageSize = 10,
                PageNumber = 1,
                TotalCount = machines.Count
            });
            _mockMapper.Setup(mapper => mapper.Map<IList<MachineDto>>(machines)).Returns(machineDtos);

            var result = await _controller.GetMachines();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateMachine_WithValidMachineDto_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var createMachineDto = new CreateMachineDto { Name = "Test Machine" };
            var expectedMachine = new Machine { Id = 1, Name = createMachineDto.Name };

            _mockMapper.Setup(mapper => mapper.Map<Machine>(createMachineDto)).Returns(expectedMachine);

            _mockUnitOfWorkService
            .Setup(repo => repo.Machines.Add(It.IsAny<Machine>()))
            .Returns((Machine machine) =>
            {
                return Task.FromResult(machine);
            });


            var logger = new Mock<ILogger<MachineController>>();
            var controller = new MachineController(_mockUnitOfWorkService.Object, logger.Object, _mockMapper.Object);

            // Act
            var result = await controller.CreateMachine(createMachineDto);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("GetMachine", createdAtRouteResult.RouteName);
            Assert.Equal(expectedMachine.Id, createdAtRouteResult.RouteValues["id"]);
        }

        [Fact]
        public async Task DeleteMachine_WithValidId_ReturnsNoContentResult()
        {
            int validId = 1;
            var existingMachine = new Machine { Id = validId, Name = "Existing Machine" };

            _mockUnitOfWorkService
            .Setup(repo => repo.Machines.GetById(
                It.IsAny<Expression<Func<Machine, bool>>>(),
                It.IsAny<List<string>>()))
            .ReturnsAsync(existingMachine);

            var result = await _controller.DeleteMachine(validId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}