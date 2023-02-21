using ApiControlProgram.Model;

namespace ApiControlProgram.Dto
{
    public class ProjectDto
    {
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int CompanyId { get; set; }
    }
}
