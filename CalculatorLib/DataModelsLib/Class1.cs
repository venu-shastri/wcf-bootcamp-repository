using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelsLib
{
    [System.Runtime.Serialization.DataContract]
    public class Address
    {
        [System.Runtime.Serialization.DataMember]
        public string City { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string State { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Street { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string PinCode { get; set; }

    }

    [System.Runtime.Serialization.DataContract]
    public class PatientInfo
    {
        [System.Runtime.Serialization.DataMember]
        public string MRN { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string ContactNumber { get; set; }
        [System.Runtime.Serialization.DataMember]

        public Address Address { get; set; }
    }
}
