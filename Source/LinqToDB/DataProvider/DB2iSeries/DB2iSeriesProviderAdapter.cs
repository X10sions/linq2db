#nullable enable
using System;

namespace LinqToDB.DataProvider.DB2iSeries{
	public class DB2iSeriesProviderAdapter : IDynamicProviderAdapter	{

		public static DB2iSeriesProviderAdapter  Instance => new DB2iSeriesProviderAdapter();

		public Type ConnectionType => throw new NotImplementedException();
		public Type DataReaderType => throw new NotImplementedException();
		public Type ParameterType => throw new NotImplementedException();
		public Type CommandType => throw new NotImplementedException();
		public Type TransactionType => throw new NotImplementedException();
	}
}
