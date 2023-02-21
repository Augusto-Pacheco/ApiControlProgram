namespace ApiControlProgram.Model
{
    public class Tasks
    {
        public int? TaskId { get; set; }
        public string NoRegist { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int TypeId { get; set; }
        public Types Types { get; set; }
        public int CategoryId { get; set; }
        public Categories Categories { get; set; }
    }
}
