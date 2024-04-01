using MedicalDoc.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalDoc.DAL.Services
{
    public class ScheduledService : BaseService
    {
        public ScheduledService(MDContext context) : base(context) 
        {
        }

        public async Task<List<Appointment>> GetAppointmentsList()
        {
            return await _context.Appointments.Include(a => a.IdPatientNavigation).ToListAsync();
        }

    }
}
