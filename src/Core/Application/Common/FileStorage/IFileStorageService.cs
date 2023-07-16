namespace FSH.WebApi.Application.Common.FileStorage;

public interface IFileStorageService : ITransientService
{
    public Task<string> UploadAsync<T>(FileUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    where T : class;

    public void Remove(string? path);

    public string? UnZip(string relativePath);

    public void RemoveFolder(string relativePath);
    public void RemoveFile(string relativePath);
}