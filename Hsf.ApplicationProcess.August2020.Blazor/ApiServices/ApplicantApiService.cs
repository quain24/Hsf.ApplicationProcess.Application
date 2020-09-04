using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Hsf.ApplicationProcess.August2020.Domain.Models;

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

        public async Task<Applicant> GetApplicantById(int id, CancellationToken token)
        {
            Applicant output;
            try
            {
                output = await _client.GetFromJsonAsync<Applicant>(_client.BaseAddress + $"/{id}",
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}, token);
            }
            catch (Exception)
            {
                return new Applicant();
            }
            return output;
        }

        public async Task<PostInfo> InsertNewApplicant(ApplicantInsertDTO newApplicant, CancellationToken token)
        {
            HttpResponseMessage result;
            try
            {
                result = await _client.PostAsJsonAsync(_client.BaseAddress, newApplicant, token);
            }
            catch(Exception ex)
            {
                return NetworkConnectionError(ex);
            }

            if (result.IsSuccessStatusCode)
                return Success();

            var responseCodes = await ExtractErrorCodes(result, token);

            return Failure(responseCodes);
        }

        private PostInfo Success() => new PostInfo(true, new ResponseCodes());

        private PostInfo Failure(ResponseCodes responseCodes) => new PostInfo(false, responseCodes);

        private PostInfo NetworkConnectionError(Exception ex)
        {
            var codes = new ResponseCodes();
            codes.AddCode("Connection Error", ex.Message, ex.StackTrace);
            return new PostInfo(false, codes);
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