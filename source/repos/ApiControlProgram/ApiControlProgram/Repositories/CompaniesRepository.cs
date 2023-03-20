using ApiControlProgram.Data;
using ApiControlProgram.Interfaces;
using ApiControlProgram.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace ApiControlProgram.Repositories
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly DataContext _context;
        public CompaniesRepository(DataContext context)
        {
            _context = context;
        }

        public bool CompanyExist(int CompanyId)
        {
            return _context.Companies.Any(p => p.CompanyId == CompanyId);
        }

        public Companies GetCompany(int CompanyId)
        {
            try
            {
                var company = _context.Companies.Where(p => p.CompanyId == CompanyId).FirstOrDefault();
                if (company == null)
                {
                    throw new Exception("No se encontró una empresa con el ID proporcionado");
                }
                return company;
            }
            catch (Exception ex)
            {
                // Manejar la excepción y responder con un mensaje personalizado
                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }

        public ICollection<Companies> GetCompanies()
        {
            try
            {
                return _context.Companies.OrderBy(p => p.CompanyId).ToList();
            }
            catch (Exception ex)
            {
                // Manejar la excepción y responder con un mensaje personalizado
                throw new Exception("No se pudo completar la operación de red. Intente de nuevo más tarde");
            }
        }

        public Companies GetCompanyByName(string Name)
        {
            return _context.Companies.Where(c => c.Name == Name).FirstOrDefault();
        }

        public bool CreateCompany(Companies companies)
        {
             _context.Add(companies);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCompany(Companies companies)
        {
           _context.Update(companies);
            return Save();
        }

        public bool DeleteCompanies(Companies company)
        {
            _context.Remove(company);
            return Save();
        }
    }
}
