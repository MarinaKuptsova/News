using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News_List.Models
{
    public class JsonResponse
    {
        public JsonResponse()
        {
                
        }

        public JsonResponse(bool success, object result)
        {
            this.Success = success;
            this.Result = result;
        }

        public bool Success { get; set; }
        public object Result { get; set; }
    }
}