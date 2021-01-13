#nullable enable
using System;
using System.Data;
using System.Linq.Expressions;
using LinqToDB.Expressions;

namespace LinqToDB.DataProvider.DB2iSeries{
	public class DB2iSeriesProviderAdapter : IDynamicProviderAdapter	{

		public static DB2iSeriesProviderAdapter  Instance => new DB2iSeriesProviderAdapter();

		public Type ConnectionType => throw new NotImplementedException();
		public Type DataReaderType => throw new NotImplementedException();
		public Type ParameterType => throw new NotImplementedException();
		public Type CommandType => throw new NotImplementedException();
		public Type TransactionType => throw new NotImplementedException();


		#region Wrappers

		[Wrapper]
		public class iDB2Connection : TypeWrapper, IDisposable
		{
			private static LambdaExpression[] Wrappers { get; }				= new LambdaExpression[] {
				// [0]: get ServerVersion
				(Expression<Func<iDB2Connection, string>>)((iDB2Connection this_) => this_.ServerVersion),
				// [1]: get LibraryList
				(Expression<Func<iDB2Connection, string>>)((iDB2Connection this_) => this_.LibraryList),
				// [2]: CreateCommand
				(Expression<Func<iDB2Connection, IDbCommand>>)((iDB2Connection this_) => this_.CreateCommand()),
				// [3]: Open
				(Expression<Action<iDB2Connection>>)((iDB2Connection this_) => this_.Open()),
				// [4]: Dispose
				(Expression<Action<iDB2Connection>>)((iDB2Connection this_) => this_.Dispose()),
				// [5]: Naming
				(Expression<Func<iDB2Connection, iDB2NamingConvention>>)((iDB2Connection this_) => this_.Naming),
			};

			public iDB2Connection(object instance, Delegate[] wrappers) : base(instance, wrappers)			{			}

			public iDB2Connection(string connectionString) => throw new NotImplementedException();

			public string ServerVersion => ((Func<iDB2Connection, string>)CompiledWrappers[0])(this);
			public string LibraryList => ((Func<iDB2Connection, string>)CompiledWrappers[1])(this);

			public IDbCommand CreateCommand() => ((Func<iDB2Connection, IDbCommand>)CompiledWrappers[2])(this);
			public void Open() => ((Action<iDB2Connection>)CompiledWrappers[3])(this);
			public void Dispose() => ((Action<iDB2Connection>)CompiledWrappers[4])(this);
			public iDB2NamingConvention Naming => ((Func<iDB2Connection, iDB2NamingConvention>)CompiledWrappers[5])(this);
		}

		[Wrapper]
		private class iDB2Parameter		{
			public iDB2DbType iDB2DbType { get; set; }
		}

		[Wrapper]
		public enum iDB2DbType		{
			iDB2BigInt = 1,
			iDB2Binary = 18,
			iDB2Blob = 20,
			iDB2Char = 6,
			iDB2CharBitData = 8,
			iDB2Clob = 21,
			iDB2DataLink = 23,
			iDB2Date = 12,
			iDB2DbClob = 22,
			iDB2DecFloat16 = 24,
			iDB2DecFloat34 = 25,
			iDB2Decimal = 4,
			iDB2Double = 17,
			iDB2Graphic = 10,
			iDB2Integer = 2,
			iDB2Numeric = 5,
			iDB2Real = 16,
			iDB2Rowid = 15,
			iDB2SmallInt = 3,
			iDB2Time = 13,
			iDB2TimeStamp = 14,
			iDB2VarBinary = 19,
			iDB2VarChar = 7,
			iDB2VarCharBitData = 9,
			iDB2VarGraphic = 11,
			iDB2Xml = 26
		}

		[Wrapper]
		public enum iDB2NamingConvention		{
			SQL = 0,
			System = 1
		}

		#endregion

	}
}
