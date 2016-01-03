using Kendo.Helpers.Core;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// An instance of this class represents a DataSource (see cref="http://docs.telerik.com/kendo-ui/api/javascript/data/datasource")
    /// </summary>
    public interface IKendoDataSource : IKendoObject
    {

        int? Page { get; set; }

        int? PageSize { get; set; }

    }
}