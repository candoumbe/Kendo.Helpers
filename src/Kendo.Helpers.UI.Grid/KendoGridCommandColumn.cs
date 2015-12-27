using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{
    public class KendoGridCommandColumn : KendoGridColumnBase
    {
        public override string ToJson() => SerializeObject(this);
    }
}
