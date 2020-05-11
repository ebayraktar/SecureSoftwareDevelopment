using SSDMobileApp.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SSDMobileApp.REST
{
    public class RestService : IRest
    {
        readonly HttpClient client;
        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<MobileResult> Authors(string id = "")
        {
            var uri = new Uri(Constants.AUTHORS_URL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<MobileResult> Books(string id = "")
        {
            var uri = new Uri(Constants.BOOKS_URL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<MobileResult> Books(Books data, string id = "")
        {
            var uri = new Uri(Constants.BOOKS_URL + id);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            if (string.IsNullOrEmpty(id))
            {
                return await BasePostRequest(uri, json);
            }
            else
            {
                return await BasePutRequest(uri, json);
            }
        }

        public async Task<MobileResult> Borrows(string id = "")
        {
            var uri = new Uri(Constants.BORROWS_URL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<MobileResult> LoginAsStudentAsync(LoginModel data)
        {
            var uri = new Uri(Constants.LOGINSTUDENT_URL);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return await BasePostRequest(uri, json);
        }

        public async Task<MobileResult> LoginAsync(LoginModel data)
        {
            var uri = new Uri(Constants.LOGIN_URL);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return await BasePostRequest(uri, json);
        }

        public async Task<MobileResult> RegisterAsync(RegisterModel data)
        {
            var uri = new Uri(Constants.REGISTER_URL);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return await BasePostRequest(uri, json);
        }

        public async Task<MobileResult> Requests(string id = "")
        {
            var uri = new Uri(Constants.REQUESTS_URL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<MobileResult> Requests(Requests data, string id = "")
        {
            var uri = new Uri(Constants.REQUESTS_URL + id);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            if (string.IsNullOrEmpty(id))
            {
                return await BasePostRequest(uri, json);
            }
            else
            {
                return await BasePutRequest(uri, json);
            }
        }

        public async Task<MobileResult> Roles(string id = "")
        {
            var uri = new Uri(Constants.ROLES_URL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<MobileResult> Students(string id = "")
        {
            var uri = new Uri(Constants.STUDENTS_URL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<MobileResult> Types(string id = "")
        {
            var uri = new Uri(Constants.TYPES_URL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<MobileResult> Users(string id = "")
        {
            var uri = new Uri(Constants.USERS_URL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<MobileResult> Users(Users data, string id = "")
        {
            var uri = new Uri(Constants.USERS_URL + id);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            if (string.IsNullOrEmpty(id))
            {
                return await BasePostRequest(uri, json);
            }
            else
            {
                return await BasePutRequest(uri, json);
            }
        }

        private async Task<MobileResult> BaseGetRequest(Uri uri)
        {
            try
            {
                if (!string.IsNullOrEmpty(Constants.Token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Constants.Token);
                }
                HttpResponseMessage response = await client.GetAsync(uri);
                return await BaseResponse(response);

            }
            catch (Exception ex)
            {
                return new MobileResult { Message = "Error", OpCode = -3, Result = ex.Message };
            }
        }

        private async Task<MobileResult> BasePostRequest(Uri uri, string json)
        {
            try
            {
                if (!string.IsNullOrEmpty(Constants.Token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Constants.Token);
                }
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);
                return await BaseResponse(response);

            }
            catch (Exception ex)
            {
                return new MobileResult { Message = "Error", OpCode = -3, Result = ex.Message };
            }
        }

        private async Task<MobileResult> BasePutRequest(Uri uri, string json)
        {
            try
            {
                if (!string.IsNullOrEmpty(Constants.Token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Constants.Token);
                }
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri, content);
                return await BaseResponse(response);

            }
            catch (Exception ex)
            {
                return new MobileResult { Message = "Error", OpCode = -3, Result = ex.Message };
            }
        }

        private async Task<MobileResult> BaseResponse(HttpResponseMessage response)
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var readAS = await response.Content.ReadAsStringAsync();
                    var mobileResult = Newtonsoft.Json.JsonConvert.DeserializeObject<MobileResult>(readAS);
                    return mobileResult;
                }
                else
                {
                    return new MobileResult { Message = "Fail", OpCode = -2, Result = response.StatusCode };
                }

            }
            catch (Exception ex)
            {
                return new MobileResult { Message = "Error", OpCode = -3, Result = ex.Message };
            }
        }
    }
}