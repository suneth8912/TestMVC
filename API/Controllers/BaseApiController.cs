using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class BaseApiController : ApiController
    {
        // GET: BaseApi
        private IGPSManagement gpsManagement;
        
          public IGPSManagement GPSManagement
        {
            get
            {
                gpsManagement= gpsManagement?? new GPSManagement();
                return gpsManagement;
            }
        }
    }
}
