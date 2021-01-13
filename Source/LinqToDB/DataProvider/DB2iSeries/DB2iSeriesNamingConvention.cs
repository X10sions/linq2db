#nullable enable
using System.Data.Common;

namespace LinqToDB.DataProvider.DB2iSeries
{
	public enum DB2iSeriesNamingConvention
	{
		Sql = 0,
		System = 1
	}

	public static class DB2iSeriesNamingConventionExtensions
	{

		public static string GetDelimiter(this DB2iSeriesNamingConvention naming) => naming == DB2iSeriesNamingConvention.Sql ? "." : "/";

		public static string GetDummyTableName(this DB2iSeriesNamingConvention naming) => $"SYSIBM{naming.GetDelimiter()}SYSDUMMY1";

		public static DB2iSeriesNamingConvention GetNamingConvention(this DbConnectionStringBuilder csb) {
			foreach(var key in new[] {
				"NAM",
//#if NETFRAMEWORK
				"Naming",
//#endif
				"Naming Convention" }) {
				var value = csb[key].ToString().ToLower();
				switch(value) {
					case "1":
					case "system":
						return DB2iSeriesNamingConvention.System;
				}
			}
			return DB2iSeriesNamingConvention.Sql;
		}

	}

}
