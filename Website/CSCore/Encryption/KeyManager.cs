using CSCore.DataHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSCore.Encryption
{
    class KeyManager
    {
        public static string GetEncryptionKey()
        {
            //string encryptionKeyPath = @"C:\Windows\System32\oem\debug\debugInfo.txt";//  ConfigHelper.ReadAppSetting("EncryptionKeyPath", "");
            return "eIvQ/95MdZdqN1KvEhnRoWm5o/Ets/RwX11ORZZBOaF73iT4fAURobwsP9YSHA5N";
        }
    }
}
