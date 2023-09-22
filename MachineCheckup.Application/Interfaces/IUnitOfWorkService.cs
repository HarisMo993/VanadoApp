using MachineCheckup.Application.Interfaces.Generics;
using MachineCheckup.Domain.Entities;

namespace MachineCheckup.Application.Interfaces
{
    public interface IUnitOfWorkService : IDisposable
    {
        IGenericService<Machine> Machines { get; }
        IGenericService<Issue> Issues { get; }

        Task Save();
    }
}
