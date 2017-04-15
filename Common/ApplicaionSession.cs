using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class ApplicaionSession
    {
        public SessionUser SessionUser
        {

            get
            {
                //if session user is not null return user
                if (HttpContext.Current.Session[Constant.UserSession] != null)
                {
                    return (SessionUser)HttpContext.Current.Session[Constant.UserSession];
                }
                else return null;
            }

            set
            {
                HttpContext.Current.Session[Constant.UserSession] = value;
            }

        }

        /// <summary>
        /// Clear the session
        /// </summary>
        public void Clear()
        {
            //clear session items
            HttpContext.Current.Session.RemoveAll();
        }

    }
}
