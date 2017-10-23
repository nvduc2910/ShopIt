using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ShopIt.Core;
using ShopIt.Core.Services;

namespace ShopIt.Droid.Services
{
    //public class AzureService : IAzureService
    //{
    //    public Task<string> Upload(byte[] bytes, string imageName)
    //    {
    //        var tcs = new TaskCompletionSource<string>();

    //        if (bytes == null || bytes.Count() == 0 || string.IsNullOrEmpty(imageName))
    //        {
    //            tcs.SetResult("");
    //            return tcs.Task;
    //        }

    //        Log.Debug("Upload Photo", "Size: " + bytes.Length);
    //        var client = GetClient();
    //        client.RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(2), 1);
    //        client.ParallelOperationThreadCount = 1;
    //        client.SingleBlobUploadThresholdInBytes = 1024 * 1024;

    //        var container = client.GetContainerReference(ApiConstants.AzureContainer);
    //        //    container.CreateIfNotExists();
    //        var blob = container.GetBlockBlobReference(imageName + ".jpeg");
    //        blob.StreamWriteSizeInBytes = 256 * 1024;

    //        using (Stream stream = new MemoryStream(bytes))
    //        {
    //            try
    //            {
    //                var requestOpts = new BlobRequestOptions();
    //                requestOpts.MaximumExecutionTime = new TimeSpan(0, 0, 0, 10);
    //                blob.UploadFromStream(stream, null, requestOpts);
    //                bytes = null;
    //                Log.Debug("Upload Photo", "successful");
    //            }
    //            catch (StorageException storageException)
    //            {
    //                Log.Debug("Upload Photo", storageException.Message);
    //            }
    //            catch (System.Net.ProtocolViolationException protocolViolationException)
    //            {
    //                Log.Debug("Upload Photo", protocolViolationException.Message);
    //            }
    //            catch (Exception exception)
    //            {
    //                Log.Debug("Upload Photo", exception.Message);
    //            }
    //        }

    //        tcs.TrySetResult(blob.Uri.AbsolutePath);

    //        return tcs.Task;
    //    }

    //    #region Azure Cloud Blob Client Methods  for Uploading an Image

    //    private CloudBlobClient GetClient()
    //    {
    //        //using storage account for expert app
    //        var creds = new StorageCredentials(ApiConstants.AzureAccountName, ApiConstants.AzureKey);
    //        var client = new CloudStorageAccount(creds, true).CreateCloudBlobClient();

    //        return client;
    //    }

    //    #endregion
    //}
}