using DSEDFinal.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DSEDFinal.ViewModels
{
    public class ValidUser : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var email = Convert.ToString(value);
            var context = new ApplicationDbContext();
            var isValid = context.Users.Any(u => u.Email == email);
            return (isValid);
        }
    }
}