using EPSPaymentGateway.Models;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Net;

namespace EPSPaymentGateway.Repo
{
    public class EPSRepository
    {
        public EPSTokenResponse GetEPSToken()
        {
            var tokenResponse = new EPSTokenResponse();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(60);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("x-hash", GenerateHash("test@eps.com.bd"));

                    var data = new
                    {
                        userName = "userName",
                        password = "password"
                    };
                    var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var httpResponse = httpClient.PostAsync("https://localhost:5001/v1/Auth/GetToken", httpContent).Result;

                    if (httpResponse.Content != null)
                    {
                        var response = httpResponse.Content.ReadAsStringAsync().Result;
                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            tokenResponse = JsonConvert.DeserializeObject<EPSTokenResponse>(response);
                        }
                        else
                        {
                            tokenResponse.ErrorMessage = response;
                        }
                    }
                    else
                    {
                        tokenResponse.ErrorMessage = "No Data Received From EPS Token";
                    }
                }
            }
            catch (Exception ex)
            {
                tokenResponse.ErrorMessage = ex.Message;
            }
            return tokenResponse;
        }

        public EPSInitializeResponse InitializeEPS(string token, decimal amount)
        {
            var epsResponse = new EPSInitializeResponse();
            try
            {
                var transactionId = @DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(60);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("x-hash", GenerateHash(transactionId));
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    var data = new EPSInitializeModel
                    {
                        StoreId = "228C769D-3177-43A7-8728-D60F907D8F12",
                        MerchantTransactionId = transactionId,
                        TransactionTypeId = 1,
                        TotalAmount = amount,
                        DeviceTypeId = 1,
                        SuccessUrl = "https://localhost:44387/Home/EPSSuccessCallBack",
                        FailUrl = "https://localhost:44387/Home/EPSFailCallBack",
                        CancelUrl = "https://localhost:44387/Home/EPSCancelCallBack"
                    };
                    var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var httpResponse = httpClient.PostAsync("https://localhost:5001/v1/EPSEngine/InitializeEPS", httpContent).Result;

                    if (httpResponse.Content != null)
                    {
                        var response = httpResponse.Content.ReadAsStringAsync().Result;
                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            epsResponse = JsonConvert.DeserializeObject<EPSInitializeResponse>(response);
                        }
                        else
                        {
                            epsResponse.ErrorMessage = response;
                        }
                    }
                    else
                    {
                        epsResponse.ErrorMessage = "No Data Received From EPS Initialize";
                    }
                }
            }
            catch (Exception ex)
            {
                epsResponse.ErrorMessage = ex.Message;
            }
            return epsResponse;
        }

        public EPSPayemntStatus EPSCheckPaymentStatus(string transactionID, string token)
        {
            var epsResponse = new EPSPayemntStatus();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(60);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("x-hash", GenerateHash(transactionID));
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    var httpResponse = httpClient.GetAsync("https://localhost:5001/v1/EPSEngine/CheckMerchantTransactionStatus?merchantTransactionId=" + transactionID).Result;

                    if (httpResponse.Content != null)
                    {
                        var response = httpResponse.Content.ReadAsStringAsync().Result;
                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            epsResponse = JsonConvert.DeserializeObject<EPSPayemntStatus>(response);
                        }
                        else
                        {
                            epsResponse.ErrorMessage = response;
                        }
                    }
                    else
                    {
                        epsResponse.ErrorMessage = "No Data Received From EPS";
                    }
                }
            }
            catch (Exception ex)
            {
                epsResponse.ErrorMessage = ex.Message;
            }
            return epsResponse;
        }

        public string GenerateHash(string payload, string hashkey = "SFNLQHJlY2lwZXdhbGEjYTc3Zi1mOTQ5NWZhY2M2ZTZuZXQ=")
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(hashkey)))
            {
                byte[] data = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                return Convert.ToBase64String(data);
            }
        }
    }
}