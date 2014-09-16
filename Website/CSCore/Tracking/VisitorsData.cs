using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSCore.Utils;

namespace CSCore.Tracking
{
    [Serializable]
    public class VisitorData
    {
        public string Domain { get; set; }
        public string VisitDate { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public int DaysIdIsUnique { get; set; }

        public static string Encrypt(VisitorData visitorData)
        {
            return CommonHelper.Encrypt(Serializer.SerializeJS(visitorData)); 
        }

        public static VisitorData Decrypt(string encryptedVisitorData)
        {
            try
            {
                return Serializer.DeserializeJS<VisitorData>(CommonHelper.Decrypt(encryptedVisitorData));
            }
            catch
            {
                return null;
            }
        }
    }
}
