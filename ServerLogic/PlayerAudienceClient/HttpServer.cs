// MIT License - Copyright (c) 2016 Can Güney Aksakalli
// https://aksakalli.github.io/2014/02/24/simple-http-server-with-csparp.html

/**
 * 
 * 1) Get HTTPS to work (SSL or X.509 certificate???) - Elias will do it
 * 2) Fix JS 
 * 3) Create dynmaic HTTP/JS site for the votes
 * 4) Server can send JSON to website which then reads/displays it
 * 5) Website can send JSON to the server for validation 
 * 6) Cookies ????
 * 
 **/


using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;

namespace PlayerAudienceClient
{
    public class HttpServer : IDisposable
    {
        private object _lockObjectDispose = new object();
        private CancellationTokenSource _cancellationTokenSourceDisposed = new CancellationTokenSource();
        private readonly string[] _indexFiles = {
            "index.html",
            "index.htm",
            "default.html",
            "default.htm"
        };

        private static IDictionary<string, string> _mimeTypeMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
            #region extension to MIME type list
            {".asf", "video/x-ms-asf"},
            {".asx", "video/x-ms-asf"},
            {".avi", "video/x-msvideo"},
            {".bin", "application/octet-stream"},
            {".cco", "application/x-cocoa"},
            {".crt", "application/x-x509-ca-cert"},
            {".css", "text/css"},
            {".deb", "application/octet-stream"},
            {".der", "application/x-x509-ca-cert"},
            {".dll", "application/octet-stream"},
            {".dmg", "application/octet-stream"},
            {".ear", "application/java-archive"},
            {".eot", "application/octet-stream"},
            {".exe", "application/octet-stream"},
            {".flv", "video/x-flv"},
            {".gif", "image/gif"},
            {".hqx", "application/mac-binhex40"},
            {".htc", "text/x-component"},
            {".htm", "text/html"},
            {".html", "text/html"},
            {".ico", "image/x-icon"},
            {".img", "application/octet-stream"},
            {".iso", "application/octet-stream"},
            {".jar", "application/java-archive"},
            {".jardiff", "application/x-java-archive-diff"},
            {".jng", "image/x-jng"},
            {".jnlp", "application/x-java-jnlp-file"},
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".js", "application/x-javascript"},
            {".mml", "text/mathml"},
            {".mng", "video/x-mng"},
            {".mov", "video/quicktime"},
            {".mp3", "audio/mpeg"},
            {".mpeg", "video/mpeg"},
            {".mpg", "video/mpeg"},
            {".msi", "application/octet-stream"},
            {".msm", "application/octet-stream"},
            {".msp", "application/octet-stream"},
            {".pdb", "application/x-pilot"},
            {".pdf", "application/pdf"},
            {".pem", "application/x-x509-ca-cert"},
            {".pl", "application/x-perl"},
            {".pm", "application/x-perl"},
            {".png", "image/png"},
            {".prc", "application/x-pilot"},
            {".ra", "audio/x-realaudio"},
            {".rar", "application/x-rar-compressed"},
            {".rpm", "application/x-redhat-package-manager"},
            {".rss", "text/xml"},
            {".run", "application/x-makeself"},
            {".sea", "application/x-sea"},
            {".shtml", "text/html"},
            {".sit", "application/x-stuffit"},
            {".swf", "application/x-shockwave-flash"},
            {".tcl", "application/x-tcl"},
            {".tk", "application/x-tcl"},
            {".txt", "text/plain"},
            {".war", "application/java-archive"},
            {".wbmp", "image/vnd.wap.wbmp"},
            {".wmv", "video/x-ms-wmv"},
            {".xml", "text/xml"},
            {".xpi", "application/x-xpinstall"},
            {".zip", "application/zip"},
            #endregion
        };
        private Thread _serverThread;
        private bool _allowCors;
        private Action<Exception> _handleException;

        public int Port { get; }

        public string RootDirectory { get; }

        public HttpServer(string directoryPath, int port, bool allowCors = true, Action<Exception> handleException = null)
        {
            RootDirectory = directoryPath;
            Port = port;
            _allowCors = allowCors;
            _handleException = handleException;
            _serverThread = new Thread(Listen);
            _serverThread.Start();
        }

        public void Dispose()
        {
            lock (_lockObjectDispose)
            {
                if (_cancellationTokenSourceDisposed.IsCancellationRequested)
                    return;
                _cancellationTokenSourceDisposed.Cancel();
            }
        }

        private void Listen()
        {
            using (HttpListener httpListener = new HttpListener())
            {
                httpListener.Prefixes.Add($"http://localhost:" + Port + "/"); // Once for normal HTTP
                //httpListener.Prefixes.Add($"https://localhost:" + Port + "/"); // Once for HTTPS
                httpListener.Start();
                using (CancellationTokenRegistration cancellationTokenRegistration = _cancellationTokenSourceDisposed.Token.Register(
                    httpListener.Abort))
                {
                    while (!_cancellationTokenSourceDisposed.IsCancellationRequested)
                    {
                        try
                        {
                            //Using this method because the GetContext method will not exit cleanly even when Stop or Abort are called.
                            Task<HttpListenerContext> task = httpListener.GetContextAsync();
                            task.Wait(_cancellationTokenSourceDisposed.Token);
                            Process(task.Result);
                        }
                        catch (Exception ex)
                        {
                            _handleException?.Invoke(ex);
                        }
                    }
                }
            }
        }

        private void Process(HttpListenerContext httpListenerContext)
        {
            HttpListenerResponse httpListenerResponse = httpListenerContext.Response;
            //Console.WriteLine(httpListenerContext.Response.OutputStream);
            //Console.WriteLine(httpListenerContext.Request.InputStream);
            string fileName = null;
            try
            {
                fileName = GetRequestedFileName(httpListenerContext.Request);
                string filePath = fileName == null ? null : Path.Combine(RootDirectory, fileName);
                if (filePath == null || !File.Exists(filePath))
                {
                    httpListenerResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    return;
                }
                ReturnFile(filePath, httpListenerContext);
            }
            catch (Exception ex)
            {
                httpListenerResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                string errorMessage = $"Error processing request";
                if (fileName != null)
                    errorMessage += $" for file \"{fileName}\"";
                throw new Exception(errorMessage, ex);
            }
            finally
            {
                httpListenerResponse.OutputStream.Close();
            }
        }

        private void ReturnFile(string filePath, HttpListenerContext httpListenerContext)
        {

            using (Stream input = new FileStream(filePath, FileMode.Open))
            {
                HttpListenerResponse httpListenerResponse = httpListenerContext.Response;
                httpListenerResponse.ContentType = GetContentType(filePath);
                httpListenerResponse.ContentLength64 = input.Length;
                httpListenerResponse.AddHeader("Date", DateTime.Now.ToString("r"));
                httpListenerResponse.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filePath).ToString("r"));
                if (_allowCors)
                    AddCorsHeaders(httpListenerResponse);
                WriteInputStreamToResponse(input, httpListenerResponse.OutputStream);
                httpListenerResponse.StatusCode = (int)HttpStatusCode.OK;
                httpListenerResponse.OutputStream.Flush();
            }
        }

        private void AddCorsHeaders(HttpListenerResponse httpListenerResponse)
        {
            httpListenerResponse.AddHeader("Access-Control-Allow-Origin", "*");
        }

        private string GetRequestedFileName(HttpListenerRequest httpListenerRequest)
        {

            string fileName = httpListenerRequest.Url.AbsolutePath.Substring(1);
            if (string.IsNullOrEmpty(fileName))
                fileName = GetExistingIndexFileName();
            return fileName;
        }

        private void WriteInputStreamToResponse(Stream inputStream, Stream outputStream)
        {

            byte[] buffer = new byte[1024 * 16];
            int nbytes;
            while ((nbytes = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                outputStream.Write(buffer, 0, nbytes);
        }

        private string GetContentType(string filePath)
        {
            string mime;
            if (_mimeTypeMappings.TryGetValue(Path.GetExtension(filePath), out mime))
                return mime;
            return "application/octet-stream";
        }

        private string GetExistingIndexFileName()
        {
            foreach (string indexFile in _indexFiles)
            {
                if (File.Exists(Path.Combine(RootDirectory, indexFile)))
                {
                    return indexFile;
                }
            }
            return null;
        }
    }
}