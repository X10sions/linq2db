#nullable enable
using System;
using JetBrains.Annotations;
using LinqToDB.Mapping;
using LinqToDB.SchemaProvider;
using LinqToDB.SqlProvider;

namespace LinqToDB.DataProvider.DB2iSeries
{
	public class DB2iSeriesProviderOptions
	{
	}

		public enum DB2iSeriesVersion
	{
		V5_4 = 54,
		V6_1 = 61,
		V7_1 = 71,
		V7_2 = 72,
		V7_3 = 73,
		V7_4 =74
	}

	public class DB2iSeriesMappingSchema : MappingSchema
	{
		public static DB2iSeriesMappingSchema Instance => new DB2iSeriesMappingSchema();

		public DB2iSeriesMappingSchema() : this(ProviderName.DB2iSeries) { }

		DB2iSeriesMappingSchema(string configuration, params MappingSchema[] schemas) : base(configuration, schemas)
		{
		}

	}

	public class DB2iSeriesDataProvider : DynamicDataProviderBase<DB2iSeriesProviderAdapter>
	{

		public DB2iSeriesDataProvider(string name, DB2iSeriesVersion version) : base(name, DB2iSeriesMappingSchema.Instance, DB2iSeriesProviderAdapter.Instance)
		{ }

		public override TableOptions SupportedTableOptions => throw new NotImplementedException();

		public override ISqlBuilder CreateSqlBuilder(MappingSchema mappingSchema)
		{
			throw new NotImplementedException();
		}

		public override ISchemaProvider GetSchemaProvider()
		{
			throw new NotImplementedException();
		}

		public override ISqlOptimizer GetSqlOptimizer()
		{
			throw new NotImplementedException();
		}

	}
}
