using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enum;

namespace Common
{

    public class AuthorizationUtility
    {
        private static ApplicaionSession session = new ApplicaionSession();


        public static bool IsPrivileged(UserPrivilege userPrivilage)
        {
            return session.SessionUser != null && session.SessionUser.PrivilegeList.Any(p => p == userPrivilage);
        }


        public static bool IsPrivileged(UserRole userRole)
        {
            return session.SessionUser != null && session.SessionUser.Role == userRole;
        }

    }
}
