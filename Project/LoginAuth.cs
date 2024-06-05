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
        using (var context = new sales_systemEntities2())
        {
            return context.Korisnici.Any(k => k.Username == username && k.Password == password);
        }
       }

    }
}
