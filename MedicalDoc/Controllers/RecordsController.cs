using MedicalDoc.DTO;
using MedicalDoc.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MedicalDoc.Controllers
{
    [AuthorizeBySession]

    public class RecordsController : BaseController
    {
        private readonly AppSettings _settings;

        public RecordsController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public IActionResult Index()
        {
            string pathDirectorio;

            if (RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                pathDirectorio = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _settings.path);
            }
            else
            {
                pathDirectorio = _settings.path;
            }

            if (!Directory.Exists(pathDirectorio))
            {
                return View(new List<MedicalRecordDTO>());
            }

            var archivos = Directory.GetFiles(pathDirectorio);


            if (archivos.Length == 0)
            {
                return View(new List<MedicalRecordDTO>());
            }

            var registros = archivos.Select(archivo => new MedicalRecordDTO
            {
                Name = Path.GetFileName(archivo),
                Patient = "Paciente " + Path.GetFileNameWithoutExtension(archivo),
                DateModified = System.IO.File.GetLastWriteTime(archivo)
            }).ToList();

            return View(registros);
        }

        public IActionResult ViewRecord(string name)
        {
            Console.WriteLine(name);
            string basePath;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _settings.path);
            }
            else
            {
                basePath = _settings.path; 
            }
            var fullPath = Path.Combine(basePath, name);

            string result;

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "cmd.exe" : "/bin/bash",
                        Arguments = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                            $"/c type {fullPath}" : $"-c \"cat {fullPath}\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }

            return View("ViewRecord", model: result);
        }



    }
}
