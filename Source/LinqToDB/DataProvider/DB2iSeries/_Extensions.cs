#nullable enable
using System;
using System.Data;
using System.Data.Common;
using LinqToDB.Data;

namespace LinqToDB.DataProvider.DB2iSeries
{
	public static class Extensions
	{

		public static DB2iSeriesVersion GetDB2iSeriesVersion(this Version version) => version switch
		{
			var x when x >= new Version(7, 4) => DB2iSeriesVersion.V7_4,
			var x when x >= new Version(7, 3) => DB2iSeriesVersion.V7_3,
			var x when x >= new Version(7, 2) => DB2iSeriesVersion.V7_2,
			var x when x >= new Version(7, 1) => DB2iSeriesVersion.V7_1,
			var x when x >= new Version(6, 1) => DB2iSeriesVersion.V6_1,
			_ => DB2iSeriesVersion.V5_4
		};

		//public static string GetQuotedLibList(this DataConnection dataConnection) => "'" + string.Join("','", dataConnection.GetLibList()) + "'";

		public static Version GetVersion(this IDbConnection connection) {
			if(connection is DbConnection) {
				var dbConnection = connection as DbConnection;
				var doOpenclose = dbConnection.State != ConnectionState.Open;
				if(doOpenclose)
					dbConnection.Open();
				//var version = AsVersion(dbConnection.ServerVersion);
				var version = new Version(dbConnection.ServerVersion.Split(' ')[0]);
				if(doOpenclose)
					dbConnection.Close();
				return version;
			}
			return new Version();
		}

		public static Version GetVersion(this IDataProvider dataProvider, string connectionString)
		{
			using(var conn = (DbConnection)dataProvider.CreateConnection(connectionString))
			{
				conn.Open();
				return conn.GetVersion();
			}
		}

		public static Version GetVersion(this DataConnection dataConnection) => ((DbConnection)dataConnection.Connection).GetVersion();
	}
}
