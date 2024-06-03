using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public static class LoginAuth
    {
       public static bool IsValidUser(string username, string password)
       {
        using (var context = new SALES_SYSTEMEntities2())
        {
            return context.Users.Any(u => u.Username == username && u.Password == password);
        }
       }

    }
}
