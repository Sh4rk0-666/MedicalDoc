using MedicalDoc.Models;

namespace MedicalDoc.DAL.Services
{
    public class BaseService
    {
        public readonly MDContext _context;

        public BaseService(MDContext context) 
        {
            _context = context;
        }
    }
}
