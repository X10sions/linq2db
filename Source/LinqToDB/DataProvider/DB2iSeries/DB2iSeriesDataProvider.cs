#nullable enable
using System;
using System.Data;
using LinqToDB.Mapping;
using LinqToDB.SchemaProvider;
using LinqToDB.SqlProvider;

namespace LinqToDB.DataProvider.DB2iSeries
{

	//public class DB2iSeriesDataProviderBase<TConnection, TDataReader>
	//	: DataProviderBase where TConnection : IDbConnection, new() where TDataReader : IDataReader
	//{

	//	public DB2iSeriesDataProviderBase()
	//		: base(ProviderName.DB2iSeries, dB2ISeriesConfiguration.MappingSchema)
	//	{
	//	}

	//	public override string? ConnectionNamespace => typeof(TConnection).Namespace

	//	public override Type DataReaderType => typeof(TDataReader);

	//	public override TableOptions SupportedTableOptions => throw new NotImplementedException();

	//	public override ISqlBuilder CreateSqlBuilder(MappingSchema mappingSchema)
	//	{
	//		throw new NotImplementedException();
	//	}

	//	public override ISchemaProvider GetSchemaProvider()
	//	{
	//		throw new NotImplementedException();
	//	}

	//	public override ISqlOptimizer GetSqlOptimizer()
	//	{
	//		throw new NotImplementedException();
	//	}

	//	protected override IDbConnection CreateConnectionInternal(string connectionString) => new TConnection
	//	{
	//		val.ConnectionString = connectionString
	//	};
	//}

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
