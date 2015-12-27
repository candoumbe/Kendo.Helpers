namespace Kendo.Helpers.Data
{
    /// <summary>
    /// An instance of this class represents a  DataSource (see cref="http://docs.telerik.com/kendo-ui/api/javascript/data/datasource")
    /// </summary>
    public abstract class KendoDataSource : IKendoObject
    {


        public abstract string ToJson();

    }
}