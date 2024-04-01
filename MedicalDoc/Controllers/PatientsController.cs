using MedicalDoc.DAL.Services;
using MedicalDoc.Models;
using MedicalDoc.Security;
using Microsoft.AspNetCore.Mvc;

namespace MedicalDoc.Controllers
{
    [AuthorizeBySession]
    public class PatientsController : BaseController
    {
        private readonly PatientsService _service;

        public PatientsController(PatientsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Patient> model = new List<Patient>();
            model = await _service.GetPatientsList();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var patient = await _service.GetPatientWithPrescribedMedications(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
    }
}
