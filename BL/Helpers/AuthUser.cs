using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public class AuthUser
    {
        public string uid;
        public string username;
        public AuthUser(string id, string uname)
        {
            uid = id;
            username = uname;
        }
    }
}
