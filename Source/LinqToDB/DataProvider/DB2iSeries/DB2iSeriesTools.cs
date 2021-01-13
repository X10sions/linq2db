#nullable enable
using System;
using System.Data;
using System.Data.Common;
using JetBrains.Annotations;
using LinqToDB.Configuration;
using LinqToDB.Data;
#if NETFRAMEWORK
using System.Data.Odbc;
using System.Data.OleDb;
#endif

namespace LinqToDB.DataProvider.DB2iSeries
{

	[PublicAPI]
	public static class DB2iSeriesTools
	{

		private static readonly Lazy<IDataProvider> _db2iSeriesDataProviderV5R4 = new Lazy<IDataProvider>(() =>
		{
			var provider = new DB2iSeriesDataProvider(ProviderName.DB2iSeries, DB2iSeriesVersion.V5_4);
			DataConnection.AddDataProvider(provider);
			return provider;
		}, true);

		private static readonly Lazy<IDataProvider> _db2iSeriesDataProviderV6R1 = new Lazy<IDataProvider>(() =>
		{
			var provider = new DB2iSeriesDataProvider(ProviderName.DB2iSeries, DB2iSeriesVersion.V6_1);
			DataConnection.AddDataProvider(provider);
			return provider;
		}, true);

		private static readonly Lazy<IDataProvider> _db2iSeriesDataProviderV7R1 = new Lazy<IDataProvider>(() =>
		{
			var provider = new DB2iSeriesDataProvider(ProviderName.DB2iSeries, DB2iSeriesVersion.V7_1);
			DataConnection.AddDataProvider(provider);
			return provider;
		}, true);

		private static readonly Lazy<IDataProvider> _db2iSeriesDataProviderV7R2 = new Lazy<IDataProvider>(() =>
		{
			var provider = new DB2iSeriesDataProvider(ProviderName.DB2iSeries, DB2iSeriesVersion.V7_2);
			DataConnection.AddDataProvider(provider);
			return provider;
		}, true);

		private static readonly Lazy<IDataProvider> _db2iSeriesDataProviderV7R3 = new Lazy<IDataProvider>(() =>
		{
			var provider = new DB2iSeriesDataProvider(ProviderName.DB2iSeries, DB2iSeriesVersion.V7_3);
			DataConnection.AddDataProvider(provider);
			return provider;
		}, true);

		private static readonly Lazy<IDataProvider> _db2iSeriesDataProviderV7R4 = new Lazy<IDataProvider>(() =>
		{
			var provider = new DB2iSeriesDataProvider(ProviderName.DB2iSeries, DB2iSeriesVersion.V7_4);
			DataConnection.AddDataProvider(provider);
			return provider;
		}, true);

		public static bool AutoDetectProvider { get; set; } = true;

		public static DB2iSeriesConnectionType GetDB2iSeriesConnectionType(this DbConnectionStringBuilder connectionStringBuilder)
		{
			if(connectionStringBuilder.ContainsKey("DRIVER")) return DB2iSeriesConnectionType.Odbc;
			else if(connectionStringBuilder.ContainsKey("PROVIDER")) return DB2iSeriesConnectionType.OleDb;
			else if(connectionStringBuilder.ContainsKey("SERVER")) return DB2iSeriesConnectionType.DB2;
#if NETFRAMEWORK
			else if(connectionStringBuilder.ContainsKey("DATA SOURCE"))
				return DB2iSeriesConnectionType.DB2i;
#endif
			else
				throw new NotImplementedException();
		}

		public static IDataProvider? ProviderDetector(IConnectionStringSettings css, string connectionString)
		{
			switch(css.ProviderName)
			{
				case "":
				case null:
				case ProviderName.DB2iSeries:
					var csb = new DbConnectionStringBuilder()
					{
						ConnectionString = connectionString.ToUpper()
					};
					var connType = csb.GetDB2iSeriesConnectionType();
					var version = connType switch
					{
					//DB2iSeriesConnectionType.DB2 => new DB2Connection(connectionString).GetVersion().GetDB2iSeriesVersion(),
					//DB2iSeriesConnectionType.DB2i => Extensions.ServerVersionAsVersion(GetiDB2ConnectionServerVersion(connectionString)).GetDB2iSeriesVersion(),
#if NETFRAMEWORK
						DB2iSeriesConnectionType.Odbc => new OdbcConnection(connectionString).GetVersion().GetDB2iSeriesVersion(),
						DB2iSeriesConnectionType.OleDb=> new OleDbConnection(connectionString).GetVersion().GetDB2iSeriesVersion(),
#endif
						_=>  DB2iSeriesVersion.V7_4
					};
					return GetDataProvider(version);
			}
			return null;
		}

		public static string GetiDB2ConnectionServerVersion(string connectionString)
		{
			using(var conn = new DB2iSeriesProviderAdapter.iDB2Connection(connectionString))
			{
				conn.Open();
				return conn.ServerVersion;
			}
		}

		//internal static IDataProvider? ProviderDetector(IConnectionStringSettings css, string connectionString)
		//{
		//	switch(css.ProviderName)
		//	{
		//		case ProviderName.DB2iSeries: return _db2DataProviderLUW.Value;
		//		case ProviderName.DB2zOS: return _db2DataProviderzOS.Value;
		//		case "":
		//		case null:
		//			if(css.Name == "DB2iSeries")
		//				goto case ProviderName.DB2iSeries;
		//			break;

		//		case ProviderName.DB2:
		//		case DB2iSeriesProviderAdapter.NetFxClientNamespace:
		//		case DB2iSeriesProviderAdapter.CoreClientNamespace:
		//			if(css.Name.Contains("LUW"))
		//				return _db2DataProviderLUW.Value;
		//			if(css.Name.Contains("z/OS") || css.Name.Contains("zOS"))
		//				return _db2DataProviderzOS.Value;

		//			if(AutoDetectProvider)
		//			{
		//				try
		//				{
		//					var cs = string.IsNullOrWhiteSpace(connectionString) ? css.ConnectionString : connectionString;

		//					using(var conn = DB2ProviderAdapter.GetInstance().CreateConnection(cs))
		//					{
		//						conn.Open();

		//						var iszOS = conn.eServerType == DB2ProviderAdapter.DB2ServerTypes.DB2_390;

		//						return iszOS ? _db2DataProviderzOS.Value : _db2DataProviderLUW.Value;
		//					}
		//				}
		//				catch
		//				{
		//				}
		//			}
		//			return GetDataProvider();
		//	}
		//	return null;
		//}

		public static IDataProvider GetDataProvider(DB2iSeriesVersion version) => version switch
		{
			DB2iSeriesVersion.V5_4 => _db2iSeriesDataProviderV5R4.Value,
			DB2iSeriesVersion.V6_1 => _db2iSeriesDataProviderV6R1.Value,
			DB2iSeriesVersion.V7_1 => _db2iSeriesDataProviderV7R1.Value,
			DB2iSeriesVersion.V7_2 => _db2iSeriesDataProviderV7R2.Value,
			DB2iSeriesVersion.V7_3 => _db2iSeriesDataProviderV7R3.Value,
			DB2iSeriesVersion.V7_4 => _db2iSeriesDataProviderV7R4.Value
		};

		#region CreateDataConnection

		public static DataConnection CreateDataConnection(string connectionString, DB2iSeriesVersion version)
			=> new DataConnection(GetDataProvider(version), connectionString);

		public static DataConnection CreateDataConnection(IDbConnection connection, DB2iSeriesVersion version)
			=> new DataConnection(GetDataProvider(version), connection);

		public static DataConnection CreateDataConnection(IDbTransaction transaction, DB2iSeriesVersion version)
			=> new DataConnection(GetDataProvider(version), transaction);

		#endregion

	}
}
