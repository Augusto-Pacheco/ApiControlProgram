namespace ApiControlProgram.Dto
{
    public class CompaniesDto
    {
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public long BankAccount { get; set; }
        public string BankName { get; set; }
        public decimal Money { get; set; }
    }
}
