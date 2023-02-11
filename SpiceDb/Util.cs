﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace SpiceDb;
internal static class Util
{
	public static Struct ToStruct(this Dictionary<string, object> dict)
	{
		var ps = new Struct();
		foreach (var pair in dict)
		{
			string key = pair.Key;
			object value = pair.Value;
			Value pValue = value switch
			{
				string s => new Value { StringValue = s },
				bool b => new Value { BoolValue = b },
				double d => new Value { NumberValue = d },
				int i => new Value { NumberValue = i },
				long l => new Value { NumberValue = l },
				uint u => new Value { NumberValue = u},
				null => new Value { NullValue = NullValue.NullValue },
				_ => throw new Exception($"Unsupported type: {value.GetType().Name}"),
			};
			ps.Fields.Add(key, pValue);
		}

		return ps;
	}
}