using Kendo.Helpers.Core;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// An instance of this class represents a DataSource (see cref="http://docs.telerik.com/kendo-ui/api/javascript/data/datasource")
    /// </summary>
    public abstract class KendoDataSource : IKendoObject
    {
        /// <summary>
        /// Computes the Json representation of the DataSource
        /// </summary>
        /// <returns></returns>
        public abstract string ToJson();

    }
}