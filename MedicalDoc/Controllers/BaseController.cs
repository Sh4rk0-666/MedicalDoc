using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace MedicalDoc.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        public void SetSessionValue(string key, string value)
        {
            if (HttpContext.Session.IsAvailable)
                HttpContext.Session.SetString(key, value);
        }
        public void SetSessionValueInt(string key, int value)
        {
            if (HttpContext.Session.IsAvailable)
                HttpContext.Session.SetInt32(key, value);
        }

        public string GetSessionValue(string key)
        {
            if (HttpContext.Session.IsAvailable)
            {
                HttpContext.Session.TryGetValue(key, out var value);
                if (value != null)
                    return HttpContext.Session.GetString(key);
            }
            return default;
        }

        public string RemoveSessionValue(string key)
        {
            if (HttpContext.Session.IsAvailable)
            {
                HttpContext.Session.Remove(key);
            }
            return default;
        }

        public int? GetSessionValueInt(string key)
        {
            if (HttpContext.Session.IsAvailable)
            {
                HttpContext.Session.TryGetValue(key, out var value);
                if (value != null)
                    return HttpContext.Session.GetInt32(key);
            }
            return default;
        }
    }
}
