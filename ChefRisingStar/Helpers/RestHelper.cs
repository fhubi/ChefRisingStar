using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChefRisingStar.Helpers
{
    internal class RestHelper
    {
        private const string RemoteUrl = "http://someendpoint.com";
        internal async static void MakePost<T>(T obj, string endpoint)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri($"{RemoteUrl}{endpoint}");

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Initialization
                    HttpResponseMessage response = new HttpResponseMessage();

                    var objAsJson = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");

                    // HTTP POST  
                    response = await client.PostAsync($"{RemoteUrl}{endpoint}", content);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body
                        var jsonStr = response.Content.ReadAsStringAsync().Result;

                        if (string.IsNullOrEmpty(jsonStr))
                        {
                            throw new ArgumentNullException("jsonStr", "Unable to post");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending PushBullet post:{ex}");
                }
            }
        }

        internal async static Task<TR> MakePost<T, TR>(T obj, string endpoint)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri($"{RemoteUrl}{endpoint}");

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Initialization
                    HttpResponseMessage response = new HttpResponseMessage();

                    var objAsJson = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");

                    // HTTP POST  
                    response = await client.PostAsync($"{RemoteUrl}{endpoint}", content);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body
                        var jsonStr = response.Content.ReadAsStringAsync().Result;

                        if (string.IsNullOrEmpty(jsonStr))
                        {
                            throw new ArgumentNullException("jsonStr", "Unable to post");
                        }

                        TR jsonObject = JsonConvert.DeserializeObject<TR>(jsonStr);
                        return jsonObject;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending post:{ex}");
                }
            }

            return default(TR);
        }

        internal async static Task<T> Get<T>(string subType, string urlParameters)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri($"{RemoteUrl}{subType}");

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(urlParameters);  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body
                    var jsonStr = response.Content.ReadAsStringAsync().Result;

                    T jsonObject = JsonConvert.DeserializeObject<T>(jsonStr);
                    return jsonObject;
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetDevices: {ex}");
            }

            return default(T);
        }
    }
}
