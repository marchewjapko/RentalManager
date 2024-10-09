using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;

namespace TerrytLookup.Infrastructure.Services.FeedDataService;

public class TerrytReader : IDisposable
{
    private static readonly CsvConfiguration CsvConfig = new(CultureInfo.InvariantCulture)
    {
        Delimiter = ";",
        Mode = CsvMode.NoEscape
    };

    private readonly CsvReader _csvReader;

    private readonly StreamReader _streamReader;

    public TerrytReader(IFormFile terrytCsvFile)
    {
        _streamReader = new StreamReader(terrytCsvFile.OpenReadStream());
        _csvReader = new CsvReader(_streamReader, CsvConfig);
    }

    public void Dispose()
    {
        _streamReader.Dispose();
        _csvReader.Dispose();
    }

    /// <summary>
    ///     Asynchronously reads records of type <typeparamref name="T" /> from a CSV source.
    /// </summary>
    /// <typeparam name="T">The type of the records to read.</typeparam>
    /// <returns>
    ///     A <c>Task&lt;List&lt;T&gt;&gt;</c> that represents the asynchronous operation.
    ///     The task result contains a list of records of type <typeparamref name="T" />.
    /// </returns>
    /// <remarks>
    ///     This method retrieves records asynchronously and disposes of the resources
    ///     once the operation is complete.
    /// </remarks>
    public Task<HashSet<T>> ReadAsync<T>()
    {
        var records = _csvReader.GetRecordsAsync<T>();

        // var result = records.ToHashSetAsync();
        var result = records.ToHashSetAsync();

        result.AsTask()
            .ContinueWith(_ => Dispose());

        return result.AsTask();
    }
}