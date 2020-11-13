using System.Linq;
using LinqToDB;
using LinqToDB.Mapping;
using NUnit.Framework;

namespace Tests.UserTests
{
	[TestFixture]
	public class Issue1287Tests : TestBase
	{
		[Table]
		[Table("ALLTYPES", Configuration = ProviderName.DB2)]
		[Table("ALLTYPES", Configuration = ProviderName.DB2iSeries)]
		private class AllTypes
		{
			[Column]
			[Column("CHARDATATYPE", Configuration = ProviderName.DB2)]
			[Column("CHARDATATYPE", Configuration = ProviderName.DB2iSeries)]
			public char charDataType { get; set; }
		}

		[Table("AllTypes")]
		[Table("ALLTYPES", Configuration = ProviderName.DB2)]
		[Table("ALLTYPES", Configuration = ProviderName.DB2iSeries)]
		private class AllTypesNullable
		{
			[Column]
			[Column("CHARDATATYPE", Configuration = ProviderName.DB2)]
			[Column("CHARDATATYPE", Configuration = ProviderName.DB2iSeries)]
			public char? charDataType { get; set; }
		}

		[Test]
		public void TestNullableChar([DataSources(ProviderName.SqlCe)] string context)
		{
			using (var db = GetDataContext(context))
			{
				var list = db.GetTable<AllTypesNullable>().Where(_ => _.charDataType == '1').ToList();

				Assert.AreEqual(1, list.Count);
				Assert.AreEqual('1', list[0].charDataType);
			}
		}

		[Test]
		public void TestChar([DataSources(ProviderName.SqlCe)] string context)
		{
			using (var db = GetDataContext(context))
			{
				var list = db.GetTable<AllTypes>().Where(_ => _.charDataType == '1').ToList();

				Assert.AreEqual(1, list.Count);
				Assert.AreEqual('1', list[0].charDataType);
			}
		}
	}
}
