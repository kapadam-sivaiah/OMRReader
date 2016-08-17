using OMRManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OMRReader.BL
{
  public  class LoginBL
    {
        public static bool CheckUser(string userName,string password)
        {
            bool user=false;
            user = LoginDAL.GetUser(userName,password);
            return user;

        }
    }
}
