using MachineCheckup.Domain.Enums;
using MachineCheckup.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineCheckup.Domain.Entities
{
    public class Issue : BaseEntity
    {
        public string Name { get; set; }
        public Priority Priority { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [Required]
        public string Description { get; set; }

        public bool IsResolved { get; set; }

        public int MachineId { get; set; }
        [ForeignKey("MachineId")]
        public Machine Machine { get; set; }
    }
}
