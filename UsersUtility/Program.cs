using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            UsersActions objusa = new UsersActions();
            objusa.SendPreExpiryNotificationToUser();
            objusa.SendExpiredNotificationToUser();

            //UsersReport objurp = new UsersReport();
            //objurp.GenerateUserRegistrationReport();
            //objurp.GenerateUserLoginReport();
        }

       
    }
}
