using MedicalDoc.Models;

namespace MedicalDoc.DAL.Services
{
    public class InsurancesService : BaseService
    {
        public InsurancesService(MDContext context) : base(context) 
        {
        }

        public async Task<List<Healthinsurance>> GetHealthinsuranceList()
        {
            return _context.Healthinsurances.ToList();
        }
    }
}
