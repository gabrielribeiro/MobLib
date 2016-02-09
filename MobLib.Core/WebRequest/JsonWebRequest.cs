using Newtonsoft.Json;
using System.IO;

namespace MobLib.WebRequest
{
    public class JsonWebRequest : BaseWebRequest
    {
        public JsonWebRequest(string url)
            : base(url)
        {
        }

        public override T DownloadDeserialized<T>()
        {
            T deserializedObj;

            using (StreamReader streamReader = base.GetStreamReader())
            {
                string json = streamReader.ReadToEnd();

                deserializedObj = JsonConvert.DeserializeObject<T>(json);
            }

            return deserializedObj;
        }

        public override sealed void Dispose()
        {
        }
    }
}