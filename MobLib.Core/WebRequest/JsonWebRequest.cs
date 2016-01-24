using Newtonsoft.Json;
using System.IO;

namespace MobLib.WebRequest
{
    public class JsonWebRequest : BaseWebRequest
    {
        public JsonWebRequest(string url) : base(url)
        {
        }

        public override T DownloadDeserialized<T>()
        {
            T deserializedObj;

            using (StreamReader streamReader = base.GetStreamReader())
            {
                string json = streamReader.ReadToEnd();

                deserializedObj = JsonConvert.DeserializeObject<T>(json);

                streamReader.Close();
            }

            return deserializedObj;
        }

        public override void Dispose()
        {
            
        }
    }
}