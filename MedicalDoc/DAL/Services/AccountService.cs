using MedicalDoc.Helpers;
using MedicalDoc.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace MedicalDoc.DAL.Services
{
    public class AccountService : BaseService
    {
        public AccountService(MDContext context) : base(context)
        {
        }

        public async Task<AppResult<Account>> CheckAccount(string user_or_email, string password)
        {
            AppResult<Account> result = new AppResult<Account>();

            try
            {

                result.MRObject = await _context.Accounts
                    .Where(x => (x.Email.ToLower() == user_or_email.ToLower() || x.Username == user_or_email.ToLower())).SingleOrDefaultAsync();


                if (result.MRObject != null)
                {
                    var hashPassword = ConvertToMD5(password);
                    bool isMatch = hashPassword.ToLower() == result.MRObject.PasswordHash.ToLower();
                    if (!isMatch)
                    {
                        result.success = false;
                        result.message = "Invalid username or password.";
                    }
                }
                else
                {
                    result.success = false;
                    result.message = "Invalid username or password.";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }

        private string ConvertToMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convertimos el array de bytes a una cadena hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
