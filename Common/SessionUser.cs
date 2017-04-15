using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SessionUser
    {
        public int  Role { get; set; }

        public List<string> PrivilegeList { get; set; }
    }
}
