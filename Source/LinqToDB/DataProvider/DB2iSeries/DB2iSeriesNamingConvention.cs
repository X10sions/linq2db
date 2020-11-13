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

		public static DB2iSeriesNamingConvention GetNamingConvention(this DbConnectionStringBuilder csb)
		{
			foreach(var key in new[] {
				"NAM",
				"Naming",
				"Naming Convention"
			})
			{
				var value = (csb[key] ?? string.Empty).ToString();
				return value.ToLower() switch
				{
					"1" => DB2iSeriesNamingConvention.System,
					"system" => DB2iSeriesNamingConvention.System,
					_ => DB2iSeriesNamingConvention.Sql
				};
			}
			return DB2iSeriesNamingConvention.Sql;
		}

	}

}
