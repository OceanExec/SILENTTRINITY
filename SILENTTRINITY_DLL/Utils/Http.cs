﻿using System;
using System.IO;
using System.Net;

namespace Kaliya.Utils
{
    public static class Http
    {
        public static byte[] Get(Uri url)
        {
            using (var wc = new WebClient())
            {
                return wc.DownloadData(url);
            }
        }

        public static byte[] Post(Uri url, byte[] payload)
        {
            var wr = WebRequest.Create(url);
            wr.Method = "POST";
            wr.ContentType = "application/octet-stream";

            wr.ContentLength = payload.Length;

            var requestStream = wr.GetRequestStream();
            requestStream.Write(payload, 0, payload.Length);
            requestStream.Close();

            var response = wr.GetResponse();
            using (var stream = new MemoryStream())
            {
                response.GetResponseStream()?.CopyTo(stream);
                return stream.ToArray();
            }
        }
    }
}