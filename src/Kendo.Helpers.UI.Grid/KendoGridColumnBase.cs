using Kendo.Helpers.Core;
using Kendo.Helpers.UI.Grid;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace Kendo.Helpers.UI.Grid
{
    [JsonObject]
    public abstract class KendoGridColumnBase : IKendoObject
    {
        
        public abstract string ToJson();


        public override string ToString() => ToJson();
    }
}

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {

        public static string ToJson(this IEnumerable<KendoGridColumnBase> columns)
        {
            string json = string.Empty;

            if (columns?.Any() ?? false)
            {

                StringBuilder sbFields = new StringBuilder();
                StringBuilder sbCommands = new StringBuilder();
                foreach (KendoGridColumnBase item in columns)
                {
                    if (item is KendoGridFieldColumn || item is KendoGridTemplateColumn)
                    {
                        sbFields.Append($"{(sbFields.Length > 0 ? "," : string.Empty)}{item.ToJson()}");
                    }
                    else if (item is KendoGridCommandColumn)
                    {
                        sbCommands.Append($"{(sbCommands.Length > 0 ? "," : string.Empty)}{item.ToJson()}");
                    }
                }
                json = $"[{sbFields}{(sbFields.Length > 0 && sbCommands.Length > 0 ? "," : string.Empty)}{(sbCommands.Length > 0 ? $@"{{""command"":[{sbCommands}]}}" : string.Empty)}]";
            }

            return json;
        }
    }
}
