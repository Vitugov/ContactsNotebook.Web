using Newtonsoft.Json;
using System.Reflection;

namespace ContactsNotebook.Wpf.Services.EntityManipulations
{
    public class BasicCloner : ICloner
    {
        public void Update<T>(T target, T source)
        {
            if (target == null || source == null)
            {
                throw new ArgumentNullException("Target or source object is null");
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    var value = property.GetValue(source);
                    property.SetValue(target, value);
                }
            }
        }

        public T Clone<T>(T source)
            where T : class, new()
        {
            var json = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
