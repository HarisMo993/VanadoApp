using System.ComponentModel.DataAnnotations;

namespace MachineCheckup.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
