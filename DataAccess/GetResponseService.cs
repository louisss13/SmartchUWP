using DataAccess.Interface;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    class GetResponseService
    {
        public static ResponseObject TraiteResponse(HttpResponseMessage response,IDaoConvertible toObject ,bool isArray)
        {
            ResponseObject contentResponse = new ResponseObject();
            //try
            //{
                String stringResult = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    if (stringResult != null)
                        if (isArray)
                        {
                            var result = JArray.Parse(stringResult);
                            contentResponse.Content = result.Children().Select(t => toObject.ToObjectDao(t).ToObjectModel()).ToList();
                            
                        }
                        else
                        {
                            var result = JObject.Parse(stringResult);
                            contentResponse.Content = toObject.ToObjectDao(result).ToObjectModel();
                        }
                            
                    contentResponse.Success = true;
                }

                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var rawError = JArray.Parse(stringResult);
                    contentResponse.Content = rawError.Children().Select(e => new Error()
                    {
                        Code = e["code"].Value<String>(),
                        Description = e["description"].Value<String>()
                    }).ToList();
                    contentResponse.Success = false;
                }
                else if (response.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    contentResponse.Content = new List<Error>() { new Error() { Code = "ErrorTimeout", Description = "Erreur 504 Timeout" } };
                    contentResponse.Success = false;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    contentResponse.Content = new List<Error>() { new Error() { Code = "ErrorServer", Description = "Erreur 500 " + response.ReasonPhrase } };
                    contentResponse.Success = false;
                }
                else
                {
                    contentResponse.Content = new List<Error>() { new Error() { Code = "ErrorUnknow", Description = "Erreur inconnue (" + response.StatusCode + ")" + response.ReasonPhrase } };
                    contentResponse.Success = false;
                }
           // }
            //catch(Exception e)
            //{
             //   contentResponse.Content = new List<Error>() { new Error() { Code = "ErrorException", Description = "Une exeption est apparue ! ("+e.GetType()+") "+e.Message} };
              //  contentResponse.Success = false;
            //}
            return contentResponse;
        }
    }
}
