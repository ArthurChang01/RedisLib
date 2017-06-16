using CoreLib.DB.Exceptions;
using SecurityDriven.TinyORM;
using SecurityDriven.TinyORM.Extensions;
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

        #region BulkInsert
        /// <summary>
        /// Bulk insert data into database
        /// </summary>
        /// <typeparam name="T">POCO type</typeparam>
        /// <param name="collection">input data</param>
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

        /// <summary>
        /// Bulk insert data into database
        /// </summary>
        /// <typeparam name="T">POCO type</typeparam>
        /// <param name="collection">input data</param>
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
        #endregion

        #region Fetch
        /// <summary>
        /// Fetch data by Id
        /// </summary>
        /// <typeparam name="T">Specific returning value type</typeparam>
        /// <param name="sql">sql sentence</param>
        /// <param name="param">sql parameter object</param>
        /// <returns>result set</returns>
        public async Task<T> FetchByIdAsync<T>(string sql, object param)
        {
            T result = default(T);

            var db = DbContext.Create(scsb.ConnectionString);
            var query = await db.QueryAsync(sql, param);

            result = query.First();

            return result;
        }

        /// <summary>
        /// Fetch data
        /// </summary>
        /// <typeparam name="T">Specific returning value type</typeparam>
        /// <param name="sql">sql sentence</param>
        /// <param name="parm">sql parameter object</param>
        /// <param name="objFactory">Specific POCO building factory</param>
        /// <returns>result set</returns>
        public async Task<IEnumerable<T>> Fetch<T>(string sql, object parm = null, Func<T> objFactory = null)
            where T : class, new()
        {
            IEnumerable<T> result = null;

            var db = DbContext.Create(scsb.ConnectionString);
            IReadOnlyList<dynamic> query;

            if (parm == null)
                query = await db.QueryAsync(sql);
            else
                query = await db.QueryAsync(sql, parm);

            if (objFactory == null)
                result = query.ToObjectArray(objFactory);
            else
                result = query.ToObjectArray<T>();

            return result;
        }
        #endregion

    }
}
