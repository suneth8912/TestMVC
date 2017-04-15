using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enum;

namespace Common
{
    public class SessionUser
    {
        public UserRole Role { get; set; }

        public List<UserPrivilege> PrivilegeList { get; set; }
    }
}
