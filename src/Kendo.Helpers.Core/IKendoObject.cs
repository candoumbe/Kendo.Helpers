using Newtonsoft.Json.Schema;

namespace Kendo.Helpers.Core
{
    public interface IKendoObject
    {
        /// <summary>
        /// Gets the string representation of the Kendo Object
        /// </summary>
        /// <returns></returns>
        string ToJson();
    }
}
