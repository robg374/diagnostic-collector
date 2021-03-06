﻿namespace KLoad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Kiroku;

    /// <summary>
    /// Collect all Files in Azure Storage Blob.
    /// </summary>
    public static class BlobFileCollector
    {
        public static void Execute()
        {
            using (KLog collectorLog = new KLog("ClassBlobFileCollector-MethodExecute"))
            {
                try
                {
                    var token = new BlobContinuationToken();

                    var blobCollection = BlobClient.BlobContainer.ListBlobsSegmentedAsync(null, token).GetAwaiter().GetResult();

                    List<string> blobPrefixNames = blobCollection.Results.OfType<CloudBlobDirectory>().Select(b => b.Prefix).ToList();

                    collectorLog.Info($"Collector => Prefix Count: {blobPrefixNames.Count()}");

                    // flush static object
                    BlobFileCollection.StaticFlush();

                    // for each dir in the blob container
                    foreach (var blobPrefixName in blobPrefixNames)
                    {
                        var prefixblobCollection = BlobClient.BlobContainer.ListBlobsSegmentedAsync(blobPrefixName, token).GetAwaiter().GetResult();

                        List<string> prefixblobFileNames = prefixblobCollection.Results.OfType<CloudBlockBlob>().Select(b => b.Name).ToList();

                        collectorLog.Info($"Collector => Parsing Prefix: {blobPrefixName}");

                        BlobFileParser.Execute(blobPrefixName, prefixblobFileNames);
                    }
                }
                catch (Exception ex)
                {
                    collectorLog.Error($"BlobFileCollector Exception: {ex.ToString()}");
                }
            }
        }
    }
}
