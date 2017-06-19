using System;

namespace ConsoleApplication
{
    [Serializable]
    public struct AppCredentials
    {      
        public int AppId;
        public string AppHash;
        public string PhoneNumber;
    }
}