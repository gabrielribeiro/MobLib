using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net;

namespace MobLib.WebRequest
{
    public abstract class BaseWebRequest : IDisposable
    {
        protected System.Net.WebRequest WebRequest { get; private set; }

        protected BaseWebRequest(string url)
        {
            this.WebRequest = System.Net.WebRequest.Create(url);
        }

        protected StreamReader GetStreamReader()
        {
            HttpWebResponse response = (HttpWebResponse)this.WebRequest.GetResponse();

            Stream resStream = response.GetResponseStream();

            return new StreamReader(resStream, Encoding.Default);
        }

        public abstract T DownloadDeserialized<T>();

        public abstract void Dispose();
    }
}
