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

        public async Task<Dictionary<string, List<string>>> InsertNewApplicant(ApplicantInsertDTO newApplicant, CancellationToken token)
        {
            var result = await _client.PostAsJsonAsync<ApplicantInsertDTO>(_client.BaseAddress, newApplicant, token);
            if (result.IsSuccessStatusCode)
                return new Dictionary<string, List<string>> {{"ok", new List<string>{"ok"}}};

            var errors = new Dictionary<string, List<string>>();
            await using var str = await result.Content.ReadAsStreamAsync();
            errors = await JsonSerializer.DeserializeAsync<Dictionary<string, List<string>>>(str, new JsonSerializerOptions(), token);
            return errors;
        }
    }
}