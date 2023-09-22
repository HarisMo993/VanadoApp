using MachineCheckup.Application.Interfaces;
using MachineCheckup.Application.Interfaces.Generics;
using MachineCheckup.Domain.Entities;
using MachineCheckup.Infrastructure.Data.Contexts;
using MachineCheckup.Infrastructure.Data.Services.Generics;

namespace MachineCheckup.Infrastructure.Data.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly ApplicationDbContext _context;
        private IGenericService<Machine> _machineService;
        private IGenericService<Issue> _issueService;

        public UnitOfWorkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericService<Machine> Machines => _machineService ??= new GenericService<Machine>(_context);
        public IGenericService<Issue> Issues => _issueService ??= new GenericService<Issue>(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
