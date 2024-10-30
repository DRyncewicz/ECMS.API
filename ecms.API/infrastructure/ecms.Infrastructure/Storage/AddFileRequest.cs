namespace ecms.Infrastructure.Storage;

public record AddFileRequest(byte[] File, string ContentType);