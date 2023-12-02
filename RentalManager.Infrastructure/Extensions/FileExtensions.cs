using Microsoft.AspNetCore.Http;

namespace RentalManager.Infrastructure.Extensions;

public static class FileExtensions
{
    public static byte[]? ToByteArray(this IFormFile? file)
    {
        if (file is null)
        {
            return null;
        }

        using var stream = file.OpenReadStream();
        var buffer = new byte[16 * 1024];
        using var memoryStream = new MemoryStream();
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            memoryStream.Write(buffer, 0, read);
        }

        return memoryStream.ToArray();
    }
}