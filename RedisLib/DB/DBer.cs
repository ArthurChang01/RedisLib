using CoreLib.DB.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLib.DB
{
    [ExcludeFromCodeCoverage]
    public class DBer : IDBer
    {
        SqlConnectionStringBuilder scsb = null;

        public DBer(string connString)
        {
            scsb = new SqlConnectionStringBuilder(connString);
        }

        private DataTable transformToDataTable<T>(IList<T> collection)
        {
            DataTable dtResult = new DataTable();

            var props = TypeDescriptor.GetProperties(typeof(T))
                                          .Cast<PropertyDescriptor>()
                                          .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                                          .ToArray();

            try
            {
                foreach (var propertyInfo in props)
                {
                    dtResult.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }

                var values = new object[props.Length];
                foreach (var item in collection)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    dtResult.Rows.Add(values);
                }
            }
            catch (Exception ex)
            {
                throw new TransformToDataTableException(ex);
            }

            return dtResult;
        }

        public void BulkInsert<T>(IList<T> collection)
        {
            DataTable table = this.transformToDataTable(collection);

            using (var bulkCopy = new SqlBulkCopy(this.scsb.ConnectionString))
            {
                bulkCopy.BatchSize = collection.Count;
                bulkCopy.DestinationTableName = this.scsb.InitialCatalog;

                try
                {
                    bulkCopy.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    throw new BulkInsertException(ex);
                }
            }
        }

        public async Task BulkInsertAsync<T>(IList<T> collection)
        {
            DataTable table = this.transformToDataTable(collection);

            using (var bulkCopy = new SqlBulkCopy(this.scsb.ConnectionString))
            {
                bulkCopy.BatchSize = collection.Count;
                bulkCopy.DestinationTableName = this.scsb.InitialCatalog;

                try
                {
                    await bulkCopy.WriteToServerAsync(table);
                }
                catch (Exception ex)
                {
                    throw new BulkInsertException(ex);
                }
            }
        }

    }
}
