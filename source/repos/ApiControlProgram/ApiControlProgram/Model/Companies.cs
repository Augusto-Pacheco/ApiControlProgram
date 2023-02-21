namespace ApiControlProgram.Model
{
    public class Companies
    {
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public long BankAccount { get; set; }
        public string BankName { get; set; }
        public decimal Money { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
