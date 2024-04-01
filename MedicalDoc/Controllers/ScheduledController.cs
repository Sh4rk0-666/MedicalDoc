using MedicalDoc.DAL.Services;
using MedicalDoc.Models;
using MedicalDoc.Security;
using Microsoft.AspNetCore.Mvc;

namespace MedicalDoc.Controllers
{
    [AuthorizeBySession]
    public class ScheduledController : BaseController
    {
        private readonly ScheduledService _service;

        public ScheduledController(ScheduledService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Appointment> model = await _service.GetAppointmentsList();
            return View(model);
        }
    }
}
