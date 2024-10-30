using Asp.Versioning;
using ecms.API.Controllers.Base;
using ecms.Application.Abstractions.Storage;
using ecms.Infrastructure.Storage;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace ecms.API.Controllers;

/// <summary>
/// Includes endpoints for file management
/// </summary>
/// <param name="blobService"></param>
[ApiVersion(EcmsApiVersion.Version1)]
public class FileController(IBlobService blobService) : BaseController
{
    /// <summary>
    /// Retrieves a file based on its unique name
    /// </summary>
    /// <param name="fileGuid"></param>
    /// <param name="ct"></param>
    /// <returns>Returns in the ContentType format returned from the BlobkService</returns>
    [HttpGet("{fileGuid:guid}")]
    [ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFile([FromRoute] Guid fileGuid, CancellationToken ct)
    {
        var fileResponse = await blobService.DownloadAsync(fileGuid, ct);

        return File(fileResponse.Stream, fileResponse.ContentType);
    }

    /// <summary>
    /// Deletes a file from the cloud based on its unique name
    /// </summary>
    /// <param name="fileGuid"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("{fileGuid:guid}")]
    [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFile([FromRoute] Guid fileGuid, CancellationToken ct)
    {
        await blobService.DeleteAsync(fileGuid, ct);

        return NoContent();
    }

    /// <summary>
    /// Uploads the file to the cloud and returns the unique name assigned by the service
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UploadFile([FromBody] AddFileRequest request, CancellationToken ct)
    {
        if (request.File == null || request.File.Length == 0)
        {
            return BadRequest(new ProblemDetails { Title = "No file uploaded." });
        }

        using Stream stream = new MemoryStream(request.File);

        var response = await blobService.UploadAsync(stream, request.ContentType, ct);

        return Created(string.Empty, Result.Success(response));
    }
}