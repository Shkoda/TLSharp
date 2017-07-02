using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeleSharp.TL;
namespace TeleSharp.TL
{
    public class ObjectUtils
    {
        public static object DeserializeObject(BinaryReader reader)
        {
            int Constructor = reader.ReadInt32();
            object obj;
            Type t =null;
            try {
                t = TLContext.getType(Constructor);
                obj = Activator.CreateInstance(t);
            }
            catch(Exception ex)
            {
               Console.WriteLine($"Constructor Invalid Or Context.Init Not Called! Constructor={Constructor}", ex);
                return null;
            }
            if (t.IsSubclassOf(typeof(TLMethod)))
            {
                ((TLMethod)obj).deserializeResponse(reader);
                return obj;
            }
            else if (t.IsSubclassOf(typeof(TLObject)))
            {
                ((TLObject)obj).DeserializeBody(reader);
                return obj;
            }
            Console.WriteLine("Weird Type : " + t.Namespace + " | " + t.Name);
            return null;
        }
        public static void SerializeObject(object  obj,BinaryWriter writer)
        {
            ((TLObject)obj).SerializeBody(writer);
        }
        public static TLVector<T> DeserializeVector<T>(BinaryReader reader)
        {
            TLVector<T> t = new TLVector<T>();
            var constructor = reader.ReadInt32();
            if (constructor != 481674261)
            {

                Console.WriteLine($"ObjectDeserializer.DeserializeVector fails with constructor {constructor}. Return empty vector, continue");
                return t;
            }
    
            t.DeserializeBody(reader);
            return t;
        }
    }
}
