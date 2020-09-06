using Hsf.ApplicationProcess.August2020.Blazor.Models;
using Hsf.ApplicationProcess.August2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Hsf.ApplicationProcess.August2020.Blazor.ApiServices
{
    public class ApplicantApiService
    {
        private readonly HttpClient _client;

        public ApplicantApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiInfoWithApplicantData> GetApplicantById(int id, CancellationToken token)
        {
            HttpResponseMessage result = null;
            try
            {
                result = await _client.GetAsync(_client.BaseAddress + $"/{id}", token);

                try
                {
                    if (result.StatusCode == HttpStatusCode.NotFound)
                        return NotFound(id);
                    var json = await result.Content.ReadAsStringAsync();
                    var output = JsonSerializer.Deserialize<Applicant>(json, new JsonSerializerOptions
                        { PropertyNameCaseInsensitive = false, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    return SuccessGet(output);
                }
                catch (JsonException)
                {
                    var codes = await ExtractErrorCodes(result, token);
                    return FailureDecoding(codes) as ApiInfoWithApplicantData;
                }
            }

            // There is no public more specific exception to catch in case of cors / connection failure not by http client
            catch (Exception ex)
            {
                return NetworkConnectionError(ex);
            }
            finally
            {
                result?.Dispose();
            }
        }

        private ApiInfoWithApplicantData NotFound(int id)
        {
            var response = new ResponseCodes().AddCode("NotFound", id.ToString());
            return new ApiInfoWithApplicantData(Status.NotFound, response);
        }

        public async Task<ApiInfo> InsertNewApplicant(ApplicantInsertModel newApplicant, CancellationToken token)
        {
            HttpResponseMessage result = null;
            try
            {
                result = await _client.PostAsJsonAsync(_client.BaseAddress, newApplicant, token);
                if (result.IsSuccessStatusCode)
                    return Success();

                var responseCodes = await ExtractErrorCodes(result, token);

                return Failure(responseCodes);
            }
            catch (Exception ex)
            {
                return NetworkConnectionError(ex);
            }
            finally
            {
                result?.Dispose();
            }
        }

        private ApiInfoWithApplicantData SuccessGet(Applicant retrievedApplicant)
        {
            var response = new ResponseCodes().AddCode("Get", "success");
            return new ApiInfoWithApplicantData(Status.Success, response, retrievedApplicant);
        }

        private ApiInfo Success()
        {
            var response = new ResponseCodes();
            response.AddCode("Insert", "success");
            return new ApiInfo(Status.Success, response);
        }

        private ApiInfo Failure(ResponseCodes responseCodes) => new ApiInfo(Status.ParameterError, responseCodes);

        private ApiInfo FailureDecoding(ResponseCodes responseCodes) => new ApiInfo(Status.InputFormatError, responseCodes);

        private ApiInfoWithApplicantData NetworkConnectionError(Exception ex, Applicant applicant = null)
        {
            var codes = new ResponseCodes().AddCode(ex.Message, "");
            return new ApiInfoWithApplicantData(Status.ConnectionError, codes, applicant);
        }

        private async Task<ResponseCodes> ExtractErrorCodes(HttpResponseMessage fromMessage, CancellationToken token)
        {
            await using var str = await fromMessage.Content.ReadAsStreamAsync();
            var errors = await JsonSerializer.DeserializeAsync<Dictionary<string, List<string>>>(str, new JsonSerializerOptions(), token);

            var responseCodes = new ResponseCodes();
            foreach (var (key, value) in errors)
                responseCodes.AddCode(key, value.ToArray());

            return responseCodes;
        }
    }
}