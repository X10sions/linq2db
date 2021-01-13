#nullable enable
using LinqToDB.Mapping;

namespace LinqToDB.DataProvider.DB2iSeries
{
	public class DB2iSeriesMappingSchema : MappingSchema
	{
		public static DB2iSeriesMappingSchema Instance => new DB2iSeriesMappingSchema();

		public DB2iSeriesMappingSchema() : this(ProviderName.DB2iSeries) { }

		DB2iSeriesMappingSchema(string configuration, params MappingSchema[] schemas) : base(configuration, schemas)
		{
		}

	}
}
