using System.Runtime.Serialization;

namespace SilverlightAsyncRestWcf.Common
{
    [DataContract]
    public class Car
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Make { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public int Year { get; set; }
    }
}
