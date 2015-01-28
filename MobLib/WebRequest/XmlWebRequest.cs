using System.IO;
using System.Xml.Serialization;

namespace MobLib.WebRequest
{
    public class XmlWebRequest : BaseWebRequest
    {
        public XmlWebRequest(string url) : base(url)
        {
        }

        public override T DownloadDeserialized<T>()
        {
            T xmlData;

            var deserializer = new XmlSerializer(typeof(T));

            using (TextReader reader = base.GetStreamReader())
            {
                object deserializedObj = deserializer.Deserialize(reader);

                xmlData = (T) deserializedObj;

                reader.Close();
            }

            return xmlData;
        }

        public override void Dispose()
        {
            
        }
    }
}