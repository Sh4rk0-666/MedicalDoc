using MedicalDoc.DAL.Services;
using MedicalDoc.Models;
using MedicalDoc.Security;
using Microsoft.AspNetCore.Mvc;

namespace MedicalDoc.Controllers
{
    [AuthorizeBySession]
    public class InsurancesController : BaseController
    {
        private readonly InsurancesService _service;

        public InsurancesController(InsurancesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Healthinsurance> model = await _service.GetHealthinsuranceList();
            return View(model);
        }
    }
}
