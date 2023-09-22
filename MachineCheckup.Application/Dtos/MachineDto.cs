namespace MachineCheckup.Application.Dtos
{
    public class MachineDto : CreateMachineDto
    {
        public int Id { get; set; }
        public List<IssueDto> Issues { get; set; }
        public double AverageIssueDuration { get; set; }
    }
}
