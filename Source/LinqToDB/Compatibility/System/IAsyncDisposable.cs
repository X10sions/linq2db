﻿#if !NATIVE_ASYNC
namespace System
{
	// Magic (see https://github.com/dotnet/roslyn/issues/45111)
	internal class IAsyncDisposable
	{
	}
}
#endif
