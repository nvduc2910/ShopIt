using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ShopIt.Core.Services
{
	public interface IAzureService
	{
		Task<string> Upload(byte[] bytes, string imageName);
	}

	public class AzureService : IAzureService
	{
		public async Task<string> Upload(byte[] bytes, string imageName)
		{
			try
			{
				var creds = new StorageCredentials(ApiConstants.AzureSAS);
				var client = new CloudStorageAccount(creds, ApiConstants.AzureAccountName, "", true).CreateCloudBlobClient();

				CloudBlobContainer container = client.GetContainerReference(ApiConstants.AzureContainer);

				//// Create the container if it doesn't already exist.
				await container.CreateIfNotExistsAsync();

				// Retrieve reference to a blob named "myblob".
				CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);

				using (Stream stream = new MemoryStream(bytes))
				{
					try
					{
						await blockBlob.UploadFromStreamAsync(stream);
						bytes = null;
						return blockBlob.Uri.AbsolutePath;
					}
					catch (Microsoft.WindowsAzure.Storage.StorageException storageException)
					{
						int i = 0;
					}
					catch (System.Net.ProtocolViolationException protocolViolationException)
					{
						int i = 0;
					}
					catch (Exception exception)
					{
						int i = 0;
					}
				}
			}
			catch (Exception ex)
			{
				int i = 1;
			}

			return null;
		}
	}
}

