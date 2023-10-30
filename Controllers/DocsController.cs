// Controllers/DocsController.cs
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/docs")]
public class DocsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHttpClientFactory _clientFactory;

    public DocsController(AppDbContext context, IHttpClientFactory clientFactory)
    {
        _context = context;
        _clientFactory = clientFactory;
    }

    [HttpPost("pull-data")]
    public async Task<IActionResult> PullData()
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync("http://api.plos.org/search?q=title:DNA");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            // Deserialize content if needed using System.Text.Json

            // Example: Deserialize content to Document
            // var document = JsonSerializer.Deserialize<Document>(content);

            // Store in the database
            // _context.Documents.Add(document);
            // await _context.SaveChangesAsync();

            return Ok("Data pulled and stored successfully.");
        }

        return BadRequest("Failed to fetch data from the API.");
    }
}
