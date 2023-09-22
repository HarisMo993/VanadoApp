using MachineCheckup.Application.Helpers;
using MachineCheckup.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MachineCheckup.Application.Dtos
{
    public class CreateIssueDto
    {
        public string Name { get; set; }
        public int MachineId { get; set; }
        [JsonConverter(typeof(PropertyConverter))]
        public Priority Priority { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
        public bool IsResolved { get; set; }
    }
}
