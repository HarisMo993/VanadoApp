namespace MachineCheckup.Domain.Entities
{
    public class PagedList<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
    }
}
