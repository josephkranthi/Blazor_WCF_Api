using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp1.Models;

namespace BlazorApp1.Services
{
    public class EmployeeApiService
    {
        private readonly HttpClient _http;
        public EmployeeApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<List<Employee>>("api/Employee");
            return res ?? new List<Employee>();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Employee>($"api/Employee/{id}");
        }

        public async Task<int> CreateAsync(Employee employee)
        {
            var resp = await _http.PostAsJsonAsync("api/Employee", employee);
            resp.EnsureSuccessStatusCode();
            var created = await resp.Content.ReadFromJsonAsync<Employee>();
            return created?.Id ?? 0;
        }

        public async Task<bool> UpdateAsync(int id, Employee employee)
        {
            var resp = await _http.PutAsJsonAsync($"api/Employee/{id}", employee);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/Employee/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
