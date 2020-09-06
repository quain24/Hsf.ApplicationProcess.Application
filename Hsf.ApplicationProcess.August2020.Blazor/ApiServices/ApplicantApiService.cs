using Hsf.ApplicationProcess.August2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Hsf.ApplicationProcess.August2020.Blazor.Models;

namespace Hsf.ApplicationProcess.August2020.Blazor.ApiServices
{
    public class ApplicantApiService
    {
        private readonly HttpClient _client;

        public ApplicantApiService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:5011/api/applicants");
        }

        public async Task<ApiInfoWithApplicantData> GetApplicantById(int id, CancellationToken token)
        {
            Applicant output;
            HttpResponseMessage result = null;
            try
            {
                result = await _client.GetAsync(_client.BaseAddress + $"/{id}", token);
                result.EnsureSuccessStatusCode();

                try
                {
                    var json = await result.Content.ReadAsStringAsync();
                    output = JsonSerializer.Deserialize<Applicant>(json, new JsonSerializerOptions
                            {PropertyNameCaseInsensitive = false, PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
                    return SuccessGet(output);
                }
                catch (JsonException)
                {
                    var codes = await ExtractErrorCodes(result, token);
                    return FailureDecoding(codes) as ApiInfoWithApplicantData;
                }
            }
            catch (HttpRequestException ex)
            {
                return NetworkConnectionError(ex);
            }
            finally
            {
                result?.Dispose();
            }
        }

        public async Task<ApiInfo> InsertNewApplicant(ApplicantInsertModel newApplicant, CancellationToken token)
        {
            HttpResponseMessage result;
            try
            {
                result = await _client.PostAsJsonAsync(_client.BaseAddress, newApplicant, token);
            }
            catch (Exception ex)
            {
                return NetworkConnectionError(ex);
            }

            if (result.IsSuccessStatusCode)
                return Success();

            var responseCodes = await ExtractErrorCodes(result, token);

            return Failure(responseCodes);
        }

        private ApiInfoWithApplicantData SuccessGet(Applicant retrievedApplicant)
        {
            var response = new ResponseCodes();
            response.AddCode("Get", "success");
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
            var codes = new ResponseCodes().AddCode(ex.Message, ex.StackTrace);
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