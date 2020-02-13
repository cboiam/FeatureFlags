using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FeatureFlag.Api.Controllers
{
    public static class ControllerBaseExtension
    {
        public static StatusCodeResult InternalServerError(this ControllerBase controllerBase) => 
            controllerBase.StatusCode((int)HttpStatusCode.InternalServerError);
        
        public static JsonResult Created(this ControllerBase controllerBase, object result)
        {
            var jsonResult = new JsonResult(result);
            jsonResult.StatusCode = (int)HttpStatusCode.Created;

            return jsonResult;
        }
    }
}
