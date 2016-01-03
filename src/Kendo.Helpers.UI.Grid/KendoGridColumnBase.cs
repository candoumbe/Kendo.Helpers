using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;

namespace Kendo.Helpers.UI.Grid
{
    public abstract class KendoGridColumnBase : IKendoObject
    {
        public abstract JSchema Schema { get; }

        public abstract string ToJson();
    }
}
