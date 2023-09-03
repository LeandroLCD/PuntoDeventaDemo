using Newtonsoft.Json;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository
{
    internal class BaseRepository
    {
        public async Task<ResultType<T>> MakeCallNetwork<T>(Func<Task<HttpResponseMessage>> apiCallFunction)
        {
            ResultType<T> resultType = new ResultType<T>();
            try
            {
                var resp = await Task.Run( () =>  apiCallFunction());

                string jsonResult = await resp.Content.ReadAsStringAsync();

                resultType.Success = resp.StatusCode.Equals(HttpStatusCode.OK);

                if (resp.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var data = JsonConvert.DeserializeObject<T>(jsonResult);
                    if (data.IsNotNull())
                    {
                        resultType.Data = data;
                    }
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.Unauthorized))
                {
                    //TODO
                    //CallTokenRefress if()else()
                    await MakeCallNetwork<T>(apiCallFunction);

                }
                else
                {
                    resultType.Errors.Add(new ErrorMessage("Error No Controlado", jsonResult));
                }

            }
            catch (Exception e)
            {
                resultType.Success = false;
                resultType.Errors.Add(new ErrorMessage(e.GetHashCode().ToString(), e.Message));
            }

            return resultType;
        }
    }
}
