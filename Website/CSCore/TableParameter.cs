using System.Linq;
using Microsoft.SqlServer.Server;
using System.Data;
using CSCore;

namespace System.Collections.Generic
{
	public static class CollectionExtensionMethods
	{
		#region Extension Methods
		/// <summary>
		/// Converts dictionary of int and string for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_int_string.
		/// </summary>
		/// <param name="values">Dictionary of int and string</param>
		/// <returns>DictionaryIntStringCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this Dictionary<int, string> values)
		{
			if ((values == null)||(values.Count == 0))
			{
				return IntStringCollection.EmptyTable;
			}
			else
			{
				IntStringCollection collection = new IntStringCollection();
				foreach (var item in values) collection.Add(new KeyValuePair<int, string>(item.Key, item.Value));
				return collection;
			}
		}

		/// <summary>
		/// Converts dictionary of int and bool for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_int_bit.
		/// </summary>
		/// <param name="values">Dictionary of int and bool</param>
		/// <returns>DictionaryIntStringCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this Dictionary<int, bool> values)
		{
            if ((values == null) || (values.Count == 0))
			{
				return IntBoolCollection.EmptyTable;
			}
			else
			{
				IntBoolCollection collection = new IntBoolCollection();
				foreach (var item in values) collection.Add(new KeyValuePair<int, bool>(item.Key, item.Value));
				return collection;
			}
		}

		/// <summary>
		/// Converts collection of keyvaluepair of int and bool for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_int_bit.
		/// </summary>
		/// <param name="values">Dictionary of int and bool</param>
		/// <returns>DictionaryIntStringCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this IEnumerable<KeyValuePair<int, bool>> values)
		{
			if ((values == null) || (values.Count() == 0))
			{
				return IntBoolCollection.EmptyTable;
			}
			else
			{
				IntBoolCollection collection = new IntBoolCollection();
				foreach (var item in values) collection.Add(item);
				return collection;
			}
		}

		/// <summary>
		/// Converts collection of keyvaluepair of guid and ints for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_guid_int.
		/// </summary>
		/// <param name="values">Collection of KeyValuePair of Guid and int</param>
		/// <returns>GuidIntPairCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this IEnumerable<KeyValuePair<Guid, int>> values)
		{
			if ((values == null) || (values.Count() == 0))
			{
				return GuidIntPairCollection.EmptyTable;
			}
			else
			{
				GuidIntPairCollection collection = new GuidIntPairCollection();
				collection.AddRange(values);
				return collection;
			}
		}

		/// <summary>
		/// Converts collection of pairs of ints and guid for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_int_guid.
		/// </summary>
		/// <param name="values">Collection of KeyValuePair of Guid and int</param>
		/// <returns>GuidIntPairCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this IEnumerable<Pair<int, Guid>> values)
		{
			if ((values == null) || (values.Count() == 0))
			{
				return IntGuidPairCollection.EmptyTable;
			}
			else
			{
				IntGuidPairCollection collection = new IntGuidPairCollection();
				collection.AddRange(values);
				return collection;
			}
		}

		/// <summary>
		/// Converts collection of keyvaluepair of two ints for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_int_int.
		/// </summary>
		/// <param name="values">Collection of KeyValuePair&lt;int, int&gt;</param>
		/// <returns>IntPairCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this IEnumerable<KeyValuePair<int, int>> values)
		{
			if ((values == null) || (values.Count() == 0))
			{
				return IntKeyValuePairCollection.EmptyTable;
			}
			else
			{
				IntKeyValuePairCollection collection = new IntKeyValuePairCollection();
				collection.AddRange(values);
				return collection;
			}
		}


		/// <summary>
		/// Converts collection of keyvaluepair of two ints for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_int_int.
		/// </summary>
		/// <param name="values">Collection of KeyValuePair int and int</param>
		/// <returns>IntPairCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this IEnumerable<Pair<int, int>> values)
		{
			if ((values == null) || (values.Count() == 0))
			{
				return IntPairCollection.EmptyTable;
			}
			else
			{
				IntPairCollection collection = new IntPairCollection();
				collection.AddRange(values);
				return collection;
			}
		}



		public static object ToTableType(this IEnumerable<Pair<int, decimal>> values)
		{
			if (values.Count() == 0)
			{
				return IntDecimalCollection.EmptyTable;
			}
			else
			{
				IntDecimalCollection collection = new IntDecimalCollection();
				collection.AddRange(values);
				return collection;
			}
		}

		public static object ToTableType(this Dictionary<int, decimal> values)
		{
			if (values.Count == 0)
			{
				return IntDecimalCollection.EmptyTable;
			}
			else
			{
				IntDecimalCollection collection = new IntDecimalCollection();
				collection.AddRange(values.Select(v => new Pair<int, decimal>(v.Key, v.Value)));
				return collection;
			}
		}

		/// <summary>
		/// Converts collection of ints for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_int.
		/// </summary>
		/// <param name="values">Collection of ints</param>
		/// <returns>IntCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this IEnumerable<int> values)
		{
			// Doing empty list check due to the following error message:
			//There are no records in the SqlDataRecord enumeration. To send a table-valued parameter with no rows, use a null reference for the value instead.
			if ((values == null) || (values.Count() == 0))
			{
				return IntCollection.EmptyTable;
			}
			else
			{
				IntCollection collection = new IntCollection();
				collection.AddRange(values);
				return collection;
			}
		}

		/// <summary>
		/// Converts collection of guids for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_guid.
		/// </summary>
		/// <param name="values">Collection of guids</param>
		/// <returns>GuidCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this IEnumerable<Guid> values)
		{
			if ((values == null) || (values.Count() == 0))
			{
				return GuidCollection.EmptyTable;
			}
			else
			{
				GuidCollection collection = new GuidCollection();
				collection.AddRange(values);
				return collection;
			}
		}

		/// <summary>
		/// Converts collection of strings for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_string.
		/// </summary>
		/// <param name="values">Collection must of strings.</param>
		/// <returns>StringCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this IEnumerable<string> values)
		{
			if ((values == null) || (values.Count() == 0))
			{
				return StringCollection.EmptyTable;
			}
			else
			{
				StringCollection collection = new StringCollection();
				collection.AddRange(values);
				return collection;
			}
		}


		/// <summary>
		/// Converts collection of guid strings for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_guid.
		/// </summary>
		/// <param name="values">Srings in collection must be Guids.</param>
		/// <returns>GuidStringCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToGuidTableType(this IEnumerable<string> values)
		{
			if ((values == null) || (values.Count() == 0))
			{
				return GuidStringCollection.EmptyTable;
			}
			else
			{
				GuidStringCollection collection = new GuidStringCollection();
				collection.AddRange(values);
				return collection;
			}
		}

		

		/// <summary>
		/// Converts dictionary of int and date time for use to pass as table valued parameter.
		/// Stored procedure parameter must be of type tt_int_date.
		/// </summary>
		/// <param name="values">Dictionary of int and date</param>
		/// <returns>DictionaryIntStringCollection type that implements IEnumerable SqlDataRecord</returns>
		public static object ToTableType(this Dictionary<int, DateTime?> values)
		{
			if ((values == null) || (values.Count == 0))
			{
				return IntDateTimeCollection.EmptyTable;
			}
			else
			{
				IntDateTimeCollection collection = new IntDateTimeCollection();
				foreach (var item in values)
				{
					KeyValuePair<int, DateTime?> pair;
					if (item.Value.HasValue)
						pair = new KeyValuePair<int, DateTime?>(item.Key, item.Value.Value);
					else
						pair = new KeyValuePair<int, DateTime?>(item.Key, null);
					collection.Add(pair);


				}
				return collection;
			}
		}

		public static object ToTableType(this Dictionary<string, string> values)
		{
			if ((values == null) || (values.Count == 0))
            {
				return StringStringDictionary.EmptyTable;
            }
			else
			{ 
				StringStringDictionary r = new StringStringDictionary();
				foreach (KeyValuePair<string, string> kp in values)
				{
					r.Add(kp.Key, kp.Value);
				}
				return r;
			}
		}


        public static object ToTableType(this IEnumerable<Pair<string, string>> values)
        {
            if ((values == null) || (values.ToList().Count == 0))
            {
                return StringStringPair.EmptyTable;
            }
            else
            {
                StringStringDictionary r = new StringStringDictionary();
                foreach (Pair<string, string> kp in values)
                {
                    r.Add(kp.Item1, kp.Item2);
                }
                return r;
            }
        }

        /// <summary>
        /// Converts collection of triplet of 3 ints for use to pass as table valued parameter.
        /// Stored procedure parameter must be of type tt_int_int_int.
        /// </summary>
        /// <param name="values">Collection of KeyValuePair int and  int</param>
        /// <returns>IntPairCollection type that implements IEnumerable SqlDataRecord</returns>
        public static object ToTableType(this IEnumerable<Triplet<int, int, int>> values)
        {
            if ((values == null) || (values.Count() == 0))
            {
                return IntTripletCollection.EmptyTable;
            }
            else
            {
                IntTripletCollection collection = new IntTripletCollection();
                collection.AddRange(values);
                return collection;
            }
        }
	}
	#endregion 
	#region Collection Helper Classes (SqlDataRecord Enumerator)

	internal class IntDecimalCollection : List<Pair<int, decimal>>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static IntDecimalCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value1", typeof(int));
			_emptyTable.Columns.Add("value2", typeof(decimal));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value1", SqlDbType.Int), new SqlMetaData("value2", SqlDbType.Money));

			foreach (Pair<int, decimal> kv in this)
			{
				row.SetInt32(0, kv.Item1);
				row.SetDecimal(1, kv.Item2);
				yield return row;
			}
		}
	}

	internal class IntStringCollection : List<KeyValuePair<int, string>>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static IntStringCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value1", typeof(int));
			_emptyTable.Columns.Add("value2", typeof(string));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value1", SqlDbType.Int), new SqlMetaData("value2", SqlDbType.NVarChar, 4000));

			foreach (KeyValuePair<int, string> kv in this)
			{
				row.SetInt32(0, kv.Key);
				row.SetString(1, kv.Value);
				yield return row;
			}
		}
	}

	internal class IntBoolCollection : List<KeyValuePair<int, bool>>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static IntBoolCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("order_id", typeof(int));
			_emptyTable.Columns.Add("value1", typeof(int));
			_emptyTable.Columns.Add("value2", typeof(bool));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("order_id", SqlDbType.Int), new SqlMetaData("value1", SqlDbType.Int), new SqlMetaData("value2", SqlDbType.Bit));
			int ordinal = 0;
			foreach (KeyValuePair<int, bool> kv in this)
			{
				row.SetInt32(0, ordinal++);
				row.SetInt32(1, kv.Key);
				row.SetBoolean(2, kv.Value);
				yield return row;
			}
		}
	}

	internal class GuidIntPairCollection : List<KeyValuePair<Guid, int>>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static GuidIntPairCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value1", typeof(Guid));
			_emptyTable.Columns.Add("value2", typeof(int));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value1", SqlDbType.UniqueIdentifier), new SqlMetaData("value2", SqlDbType.Int));

			foreach (KeyValuePair<Guid, int> kv in this)
			{
				row.SetSqlGuid(0, kv.Key);
				row.SetInt32(1, kv.Value);
				yield return row;
			}
		}
	}

	internal class IntGuidPairCollection : List<Pair<int, Guid>>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static IntGuidPairCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("int_value", typeof(int));
			_emptyTable.Columns.Add("guid_value", typeof(Guid));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("int_value", SqlDbType.Int), new SqlMetaData("guid_value", SqlDbType.UniqueIdentifier));

			foreach (Pair<int, Guid> kv in this)
			{
				row.SetInt32(0, kv.Item1);
				row.SetSqlGuid(1, kv.Item2);

				yield return row;
			}
		}
	}


	internal class IntKeyValuePairCollection : List<KeyValuePair<int, int>>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static IntKeyValuePairCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value1", typeof(int));
			_emptyTable.Columns.Add("value2", typeof(int));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value1", SqlDbType.Int), new SqlMetaData("value2", SqlDbType.Int));

			foreach (KeyValuePair<int, int> kv in this)
			{
				row.SetInt32(0, kv.Key);
				row.SetInt32(1, kv.Value);
				yield return row;
			}
		}
	}

	internal class IntPairCollection : List<Pair<int, int>>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static IntPairCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value1", typeof(int));
			_emptyTable.Columns.Add("value2", typeof(int));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value1", SqlDbType.Int), new SqlMetaData("value2", SqlDbType.Int));

			foreach (Pair<int, int> kv in this)
			{
				row.SetInt32(0, kv.Item1);
				row.SetInt32(1, kv.Item2);
				yield return row;
			}
		}
	}

    internal class IntTripletCollection : List<Triplet<int, int, int>>, IEnumerable<SqlDataRecord>
    {
        private static DataTable _emptyTable;
        public static DataTable EmptyTable
        {
            get
            {
                return _emptyTable;
            }
        }

        static IntTripletCollection()
        {
            _emptyTable = new DataTable();
            _emptyTable.Columns.Add("value1", typeof(int));
            _emptyTable.Columns.Add("value2", typeof(int));
            _emptyTable.Columns.Add("value3", typeof(int));
        }

        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var row = new SqlDataRecord(
                new SqlMetaData("value1", SqlDbType.Int), 
                new SqlMetaData("value2", SqlDbType.Int), 
                new SqlMetaData("value3", SqlDbType.Int)
                );

            foreach (Triplet<int, int, int> kv in this)
            {
                row.SetInt32(0, kv.Item1);
                row.SetInt32(1, kv.Item2);
                row.SetInt32(2, kv.Item3);
                yield return row;
            }
        }
    }

	
	// Relies on tt_int table type with 2 columns ( value int, order_id int NULL )
	internal class IntCollection : List<int>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static IntCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value", typeof(int));
			_emptyTable.Columns.Add("order_id", typeof(int));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value", SqlDbType.Int), new SqlMetaData("order_id", SqlDbType.Int));

			int ordinal = 0;
			foreach (int val in this)
			{
				row.SetInt32(0, val);
				row.SetInt32(1, ordinal++);
				yield return row;
			}
		}
	}

	// Relies on tt_guid table type with single column ( value guid, order_id int NULL )
	internal class GuidCollection : List<Guid>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static GuidCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value", typeof(Guid));
			_emptyTable.Columns.Add("order_id", typeof(int));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value", SqlDbType.UniqueIdentifier), new SqlMetaData("order_id", SqlDbType.Int));

			int ordinal = 0;
			foreach (Guid val in this)
			{
				row.SetGuid(0, val);
				row.SetInt32(1, ordinal++);
				yield return row;
			}
		}
	}

	// Relies on tt_guid table type with single column ( value guid, order_id int NULL )
	internal class GuidStringCollection : List<string>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static GuidStringCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value", typeof(Guid));
			_emptyTable.Columns.Add("order_id", typeof(int));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value", SqlDbType.UniqueIdentifier), new SqlMetaData("order_id", SqlDbType.Int));

			int ordinal = 0;
			foreach (string val in this)
			{
				row.SetGuid(0, new Guid(val));
				row.SetInt32(1, ordinal++);
				yield return row;
			}
		}
	}

    //The original class was changed sometime back and broke all functions using this with tt_int_string
    //Since this is now being used by both tt_int_string AND tt_guid table which use different column names
    //this temporary extra function will be added since we are so close to the 92 release in order to be safe
    //it should be revisisted after the release to be fixed in a more permanent manner
    // Relies on tt_int_string AND tt_guid table type with single column ( value guid, order_id int NULL )
    internal class StringCollection_92Release_Revisit : List<string>, IEnumerable<SqlDataRecord>
    {
        private static DataTable _emptyTable;
        public static DataTable EmptyTable
        {
            get
            {
                return _emptyTable;
            }
        }

        static StringCollection_92Release_Revisit()
        {
            _emptyTable = new DataTable();
            _emptyTable.Columns.Add("value1", typeof(int));
            _emptyTable.Columns.Add("value2", typeof(string));
        }

        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var row = new SqlDataRecord(new SqlMetaData("value1", SqlDbType.Int), new SqlMetaData("value2", SqlDbType.NVarChar, 4000));

            int ordinal = 0;
            foreach (string val in this)
            {
                row.SetInt32(0, ordinal++);
                row.SetString(1, val);
                yield return row;
            }
        }
    }

	// Relies on tt_guid table type with single column ( value guid, order_id int NULL )
	internal class StringCollection : List<string>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static StringCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value", typeof(string));
			_emptyTable.Columns.Add("order_id", typeof(int));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value", SqlDbType.NVarChar, 4000), new SqlMetaData("order_id", SqlDbType.Int));

			int ordinal = 0;
			foreach (string val in this)
			{
				row.SetString(0, val);
				row.SetInt32(1, ordinal++);
				yield return row;
			}
		}
	}

	

	internal class IntDateTimeCollection : List<KeyValuePair<int, DateTime?>>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static IntDateTimeCollection()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value1", typeof(int));
			_emptyTable.Columns.Add("value2", typeof(DateTime));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value1", SqlDbType.Int), new SqlMetaData("value2", SqlDbType.DateTime));

			foreach (KeyValuePair<int, DateTime?> kv in this)
			{
				row.SetInt32(0, kv.Key);
				if (kv.Value.HasValue)
					row.SetDateTime(1, kv.Value.Value);
				yield return row;

			}
		}
	}

	internal class StringStringDictionary : Dictionary<string, string>, IEnumerable<SqlDataRecord>
	{
		private static DataTable _emptyTable;
		public static DataTable EmptyTable
		{
			get
			{
				return _emptyTable;
			}
		}

		static StringStringDictionary()
		{
			_emptyTable = new DataTable();
			_emptyTable.Columns.Add("value1", typeof(string));
			_emptyTable.Columns.Add("value2", typeof(string));
		}

		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var row = new SqlDataRecord(new SqlMetaData("value1", SqlDbType.NVarChar, 1000), new SqlMetaData("value2", SqlDbType.NVarChar, 1000));

			foreach (KeyValuePair<string, string> kv in this)
			{
				row.SetString(0, kv.Key);
				row.SetString(1, kv.Value);
				yield return row;
			}
		}
	}


    internal class StringStringPair: List<Pair<string, string>>, IEnumerable<SqlDataRecord>
    {
        private static DataTable _emptyTable;
        public static DataTable EmptyTable
        {
            get
            {
                return _emptyTable;
            }
        }

        static StringStringPair()
        {
            _emptyTable = new DataTable();
            _emptyTable.Columns.Add("value1", typeof(string));
            _emptyTable.Columns.Add("value2", typeof(string));
        }

        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var row = new SqlDataRecord(new SqlMetaData("value1", SqlDbType.NVarChar, 1000), new SqlMetaData("value2", SqlDbType.NVarChar, 1000));

            foreach (Pair<string, string> kv in this)
            {
                row.SetString(0, kv.Item1);
                row.SetString(1, kv.Item2);
                yield return row;
            }
        }
    }

	#endregion
}
