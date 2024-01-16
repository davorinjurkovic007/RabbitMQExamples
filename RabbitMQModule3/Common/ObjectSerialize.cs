using System.Text;
using System.Text.Json;

namespace Common
{
    public static class ObjectSerialize
    {
        public static byte[] Serialize(this object obj)
        {
            if(obj == null) throw new ArgumentNullException(nameof(obj));

            var json = JsonSerializer.Serialize(obj);
            return Encoding.ASCII.GetBytes(json);
        }
    }
}
