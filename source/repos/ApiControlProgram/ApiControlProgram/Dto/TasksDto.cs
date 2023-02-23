using ApiControlProgram.Model;

namespace ApiControlProgram.Dto
{
    public class TasksDto
    {
        public int? TaskId { get; set; }
        public string Name { get; set; }
        public string NoRegis { get; set; }
        public string Description { get; set; }
        public DateTime TodayDate { get; set; }
        public decimal Price { get; set; }
        public int ProjectId { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }
    }
}
