using ApiControlProgram.Model;

namespace ApiControlProgram.Interfaces
{
    public interface ICompaniesRepository
    {
        ICollection<Companies> GetCompanies();
        Companies GetCompany(int CompanyId);
        Companies GetCompanyByName(string Name);
        bool CompanyExist(int CompanyId);
        bool CreateCompany(Companies companies);
        bool UpdateCompany(Companies companies);
        bool DeleteCompanies(Companies company);
        bool Save();
    }
}
