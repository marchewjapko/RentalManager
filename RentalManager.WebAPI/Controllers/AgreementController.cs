using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.AgreementCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[Controller]")]
public class AgreementController(IAgreementService agreementService, IConfiguration configuration)
    : Controller
{
    [HttpPost]
    public async Task<IActionResult> AddAgreement([FromBody] CreateAgreement createAgreement)
    {
        await agreementService.AddAsync(createAgreement, User);

        return Ok();
    }

    [ProducesResponseType(typeof(IEnumerable<AgreementDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllAgreements(
        [FromQuery] QueryAgreements queryAgreements)
    {
        var result = await agreementService.BrowseAllAsync(queryAgreements);

        return Json(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAgreement(int id)
    {
        await agreementService.DeleteAsync(id);

        return Ok();
    }

    [ProducesResponseType(typeof(AgreementDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAgreement(int id)
    {
        var result = await agreementService.GetAsync(id);

        return Json(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAgreement(
        [FromBody] UpdateAgreement updateAgreement,
        int id)
    {
        await agreementService.UpdateAsync(updateAgreement, id);

        return Ok();
    }

    [Route("Deactivate/{id}")]
    [HttpPatch]
    public async Task<IActionResult> DeactivateAgreement(int id)
    {
        await agreementService.Deactivate(id);

        return Ok();
    }

    [Route("Document/{id}")]
    [HttpGet]
    public async Task<IActionResult> GenerateDocument(int id)
    {
        var address = configuration.GetSection("DocumentServiceURL");

        if (address.Value is null)
        {
            throw new ConfigurationNotFoundException("DocumentServiceURL");
        }

        var agreement = await agreementService.GetAsync(id);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        using StringContent jsonContent = new(
            JsonSerializer.Serialize(agreement.ToDocumentRequest(), options),
            Encoding.UTF8,
            "application/json");

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(address.Value)
        };

        using var response = await httpClient.PostAsync(
            "/documents/generate_document",
            jsonContent);

        var fileName =
            $"Umowa wypożyczenia - {agreement.Client.Name} {agreement.Client.Surname}.pdf";

        fileName = string.Join(
            "/",
            fileName.Split("/")
                .Select(WebUtility.UrlEncode)
        );
        fileName = fileName.Replace('+', ' ');

        Response.Headers.Append("Content-Type", "application/pdf");
        Response.Headers.Append("Content-Disposition", $"attachment; filename={fileName}");

        var pdfBytes = await response.Content.ReadAsByteArrayAsync();

        return File(pdfBytes, "application/pdf");
    }
}