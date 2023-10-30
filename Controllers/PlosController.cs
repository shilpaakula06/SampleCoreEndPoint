using Microsoft.AspNetCore.Mvc; // Include this namespace
using Microsoft.Extensions.Logging; // (if you are using ILogger)
using System.Threading.Tasks;
using System.Net.Http; // (if you're directly working with HttpClient)
// ... other necessary using directives


[ApiController]
[Route("api/docs")]
public class PlosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;

    public PlosController(AppDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost("pull-data")]
    public async Task<IActionResult> PullDataFromApi()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("http://api.plos.org/search?q=title:DNA");

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            // Deserialize and process the data as needed
            // Example: var documents = JsonConvert.DeserializeObject<List<PlosDocument>>(data);
            // _context.PlosDocuments.AddRange(documents);
            // await _context.SaveChangesAsync();
            return Ok("Data pulled and stored successfully.");
        }

        return StatusCode((int)response.StatusCode, "Failed to pull data.");
    }
}
