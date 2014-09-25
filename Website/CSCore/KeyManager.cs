using CSCore.DataHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSCore.Utils
{
    class KeyManager
    {
        public static string GetEncryptionKey()
        {
            string encryptionKeyPath =@"C:\EncryptionKeys\KeyContainer.txt";//  ConfigHelper.ReadAppSetting("EncryptionKeyPath", "");
            return File.ReadAllLines(encryptionKeyPath)[1];
        }
    }
}
