using System.Collections.Generic;
using System.Linq;

namespace Flatter.Serializer.Models
{
    public class FlatterResult
    {
        public FlatterResult(List<string> keys, List<object> values)
        {
            Keys = keys;
            Values = values;
        }

        public FlatterResult(List<KeyValuePair<string, object>> data)
        {
            Keys = new List<string>();
            Values = new List<object>();

            data.ForEach(element =>
            {
                Keys.Add(element.Key);
                Values.Add(element.Value);
            });
        }

        public List<KeyValuePair<string, object>> GetListElements()
        {
            var index = 0;
            return Keys.Select(key => new KeyValuePair<string, object>(key, Values[index++])).ToList();
        }

        public List<string> Keys { get; }
        public List<object> Values { get; }
    }
}
