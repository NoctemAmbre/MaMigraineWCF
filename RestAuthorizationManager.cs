﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

namespace MigraineCSMiddleware
{
    public class RestAuthorizationManager: ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            
            //Extract the Authorization header, and parse out the credentials converting the Base64 string:  
            var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            //if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            //{
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Max-Age", "1728000");
            //}

            HttpContext httpContext = HttpContext.Current;
            NameValueCollection headerList = httpContext.Request.Headers;
            var authorizationField = headerList.Get("Authorization");


            //if ((authHeader != null) && (authHeader != string.Empty))
            //{
            //    var svcCredentials = System.Text.ASCIIEncoding.ASCII
            //        .GetString(Convert.FromBase64String(authHeader.Substring(6)))
            //        .Split(':');
            //    var user = new
            //    {
            //        Name = svcCredentials[0],
            //        Password = svcCredentials[1]
            //    };
            //    if ((user.Name == "testuser" && user.Password == "testpassword"))
            //    {
            //        //User is authrized and originating call will proceed  
            //        return true;
            //    }
            //    else
            //    {
            //        //not authorized  
            //        return false;
            //    }
            //}
            //else
            //{
            //    //No authorization header was provided, so challenge the client to provide before proceeding:  
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"MyWCFService\"");
            //    //Throw an exception with the associated HTTP status code equivalent to HTTP status 401  
            //    throw new WebFaultException(HttpStatusCode.Unauthorized);
            //}

            return true;
        }
    }
}