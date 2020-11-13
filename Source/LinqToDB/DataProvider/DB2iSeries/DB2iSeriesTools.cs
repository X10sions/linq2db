#nullable enable
using System;
using JetBrains.Annotations;
using LinqToDB.Configuration;
using LinqToDB.Data;

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

		private static readonly Lazy<IDataProvider> _db2iSeriesDataProviderV7R4 = new Lazy<IDataProvider>(() =>
		{
			var provider = new DB2iSeriesDataProvider(ProviderName.DB2iSeries, DB2iSeriesVersion.V7_4);
			DataConnection.AddDataProvider(provider);
			return provider;
		}, true);

		public static bool AutoDetectProvider { get; set; } = true;

		public static IDataProvider? ProviderDetector(IConnectionStringSettings css, string connectionString)
		{
			//var csb = new DbConnectionStringBuilder()
			//{
			//	ConnectionString = connectionString.ToUpper()
			//};
			//var version = csb.GetVersion();

			switch(css.ProviderName)
			{
				case "":
				case null:
				case ProviderName.DB2iSeries: return _db2iSeriesDataProviderV5R4.Value;
					//case DB2iSeriesProviderName.DB2i_DB2Connect:
					//	return _db2iSeriesDotconnectDataProvider.Value;

					//case DB2iSeriesProviderName.DB2i_Odbc: return _db2iSeriesOdbcDataProvider.Value;
					//case DB2iSeriesProviderName.DB2i_OleDb: return _db2iSeriesOleDbDataProvider.Value;
			}
			//var providerType = csb.GetProviderType();
			return null;
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

	}
}
