﻿using System;
using Xunit;

namespace Cheats
{
	public sealed class MemoryTests: Assert
	{
		public struct MyStruct
		{
			public Guid guid;
			public decimal money;
		}

		[Fact]
		public void BitcastTest()
		{
			var s = new MyStruct() { guid = Guid.NewGuid() };
			var guid = s.BitCast<MyStruct, Guid>();
			Equal(s.guid, guid);
		}

		[Fact]
		public void AsBinaryTest()
		{
			var g = Guid.NewGuid();
			var bytes = ValueType<Guid>.AsBinary(g);
			True(g.ToByteArray().SequenceEqual(bytes));
		}

		[Fact]
		public void BitwiseEqualityTest2()
		{
			var value1 = Guid.NewGuid();
			var value2 = value1;
			True(value1.BitwiseEquals(value2));
			value2 = default;
			False(value1.BitwiseEquals(value2));
		}

		[Fact]
		public void DefaultTests()
		{
			var value = default(Guid);
            True(ValueType<Guid>.IsDefault(value));
            value = Guid.NewGuid();
            False(ValueType<Guid>.IsDefault(value));
		}
	}
}
