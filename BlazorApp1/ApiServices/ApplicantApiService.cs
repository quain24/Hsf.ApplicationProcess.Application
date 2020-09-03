using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Hsf.ApplicationProcess.August2020.Domain.Models;

namespace BlazorApp1.ApiServices
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
    }
}