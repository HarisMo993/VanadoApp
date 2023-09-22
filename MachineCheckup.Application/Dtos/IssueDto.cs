using MachineCheckup.Application.Helpers;
using MachineCheckup.Domain.Enums;
using System.Text.Json.Serialization;

namespace MachineCheckup.Application.Dtos
{
    public class IssueDto : CreateIssueDto
    {
        public int Id { get; set; }
    }
}
