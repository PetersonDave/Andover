using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Andover.Domain.Components.Database.Provider;
using Andover.Domain.Components.Database.Provider.Entities.Attributes;
using Andover.Domain.Core;

namespace Andover.Data.Database.Provider
{
	public class DatabaseProvider : ProviderBase, IDatabaseProvider
	{
		public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

		public DatabaseProvider(string connectionString, string databaseName)
		{
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");
            if (string.IsNullOrEmpty(databaseName)) throw new ArgumentNullException("Database");

			ConnectionString = connectionString;
		    DatabaseName = databaseName;
		}

		public T GetFirstResult<T>(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				throw new ArgumentNullException("query");
			}

			return GetResults<T>(query).FirstOrDefault();
		}

		public IEnumerable<T> GetResults<T>(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				throw new ArgumentNullException("query");
			}

			var theList = new List<T>();

			using (var conn = new SqlConnection(ConnectionString))
			{
				conn.Open();
				using (SqlCommand cmd = conn.CreateCommand())
				{
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = query;

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
						while (reader.Read())
						{
							var instance = GetInstanceFromReader<T>(infos, reader);
							theList.Add((T)instance);
						}
					}
				}
			}

			return theList;
		}

		private object GetInstanceFromReader<T>(PropertyInfo[] infos, SqlDataReader reader)
		{
			var instance = Activator.CreateInstance(typeof (T));
			foreach (var pi in infos)
			{
				var attributes = pi.GetCustomAttributes(typeof (DataField), false);

				bool isAttributeValid = (attributes[0] as DataField) != null;
				if (isAttributeValid)
				{
					var fieldName = (attributes[0] as DataField).FieldName;
					bool readerHasField = reader[fieldName] != null;
					if (readerHasField)
					{
						pi.SetValue(instance, Convert.ChangeType(reader[fieldName], pi.PropertyType), null);
					}
				}
			}
			return instance;
		}

		public override void DisposeProvider()
		{
			throw new NotImplementedException();
		}
	}
}
