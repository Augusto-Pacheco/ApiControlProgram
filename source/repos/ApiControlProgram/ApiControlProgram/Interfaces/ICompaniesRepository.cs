using ApiControlProgram.Model;

namespace ApiControlProgram.Interfaces
{
    public interface ICompaniesRepository
    {
        ICollection<Companies> GetCompanies();
        Companies GetCompany(int CompanyId);
        Companies GetCompany(string Name);
        bool CompanyExist(int CompanyId);
    }
}
