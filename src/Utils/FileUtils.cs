namespace MyNihongo.FluentHttp;

internal static class FileUtils
{
	public static string GetFileResponseFilePath(UrlResponse res, string localFolderPath, string? localFileName)
	{
		if (string.IsNullOrEmpty(localFileName))
		{
			var lastSeparatorIndex = res.Url.LastIndexOf(Const.UriSeparator);
			
			localFileName = lastSeparatorIndex != -1
				? res.Url[(lastSeparatorIndex + 1)..]
				: res.Url;
		}
		
		return Path.Combine(localFolderPath, localFileName);
	}
	
	public static FileStream AsyncStream(
		string path,
		FileMode fileMode = FileMode.Create,
		FileAccess fileAccess = FileAccess.ReadWrite,
		FileShare fileShare = FileShare.ReadWrite)
	{
		var directoryName = Path.GetDirectoryName(path);

		if (!string.IsNullOrEmpty(directoryName))
			Directory.CreateDirectory(directoryName);
		
		return new FileStream(
			path: path,
			mode: fileMode,
			access: fileAccess,
			share: fileShare,
			bufferSize: 4096,
			useAsync: true
		);
	}
}
