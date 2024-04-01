using MedicalDoc.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalDoc.DAL.Services
{
    public class PatientsService : BaseService
    {
        public PatientsService(MDContext context) : base(context) 
        {
        }

        public async Task<List<Patient>> GetPatientsList()
        {
            return _context.Patients.ToList();
        }

        public async Task<Patient> GetPatientWithPrescribedMedications(int id)
        {
            return await _context.Patients
                .Include(p => p.PrescribedMedications)
                .Include(h => h.MedicalHistories)
                .FirstOrDefaultAsync(p => p.IdPatient == id);
        }
    }
}
