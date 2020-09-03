using System.Collections.Generic;
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
        }

        public async Task<Applicant> GetApplicantById(int id, CancellationToken token)
        {
            var output = await _client.GetFromJsonAsync<Applicant>(_client.BaseAddress + $"/{id}", new JsonSerializerOptions { PropertyNameCaseInsensitive = true }, token);
            return output;
        }

        public async Task<PostInfo> InsertNewApplicant(ApplicantInsertDTO newApplicant, CancellationToken token)
        {
            var result = await _client.PostAsJsonAsync(_client.BaseAddress, newApplicant, token);
            if (result.IsSuccessStatusCode)
                return Success();

            var responseCodes = await ExtractErrorCodes(result, token);

            return Failure(responseCodes);
        }

        private PostInfo Success() => new PostInfo(true, new ResponseCodes());

        private PostInfo Failure(ResponseCodes responseCodes) => new PostInfo(false, responseCodes);

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