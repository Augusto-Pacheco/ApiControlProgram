namespace ApiControlProgram.Model
{
    public class Project
    {
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int CompanyId { get; set; }
        public Companies Companies { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
    }
}
