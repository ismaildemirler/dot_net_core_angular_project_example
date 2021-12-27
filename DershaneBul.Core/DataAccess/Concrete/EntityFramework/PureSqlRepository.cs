using DershaneBul.Core.DataAccess.Abstract;
using DershaneBul.Core.Models.Concrete;
using DershaneBul.Entities.Abstract;
using DershaneBul.Entities.Containers.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DershaneBul.Core.DataAccess.Concrete.EntityFramework
{
    public class PureSqlRepository : IPureSqlRepository
    {
        protected DbContext _context;
        public PureSqlRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int ExecuteScalarSqlCommandAsync(string sql, params object[] parameters)
        {
            var result = 0;
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
                result = (int)command.ExecuteScalar();
            }

            return result;
        }

        public async Task<List<TEntity>> ExecuteSqlCommandAsync<TEntity>(string sql, params object[] parameters)
            where TEntity : class, IBaseEntity, new()
        {
            return await _context.Set<TEntity>().FromSql(sql, parameters).ToListAsync();
        }

        public async Task<PagedList<TEntity>> GetPagedListSqlQuery<TEntity>(string sqlQuery, List<SqlParameter> parameters,
            BaseRequest parameterRequest) where TEntity : class, IBaseEntity, new()
        {
            if (parameters == null)
                parameters = new List<SqlParameter>();

            var rowNumberString = ",DENSE_RANK() OVER(ORDER BY " + parameterRequest.AdditionalOrderParameter + ") AS RowNum";

            var sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(1) FROM (");
            sbCount.Append(sqlQuery.Replace(rowNumberString, ""));
            sbCount.Append(") as Count");

            var paramsCount = new List<SqlParameter>();
            foreach (var parameter in parameters)
            {
                paramsCount.Add(new SqlParameter
                {
                    Value = parameter.Value,
                    ParameterName = parameter.ParameterName
                });
            }

            var pagedList = new PagedList<TEntity>();

            var count = ExecuteScalarSqlCommandAsync(sbCount.ToString(), paramsCount.ToArray());

            if (count > 0)
            {
                var sbMain = new StringBuilder();
                sbMain.Append("SELECT * FROM ( ");
                sbMain.Append(sqlQuery);
                sbMain.Append(" ) AS Main");
                if (parameterRequest != null)
                {
                    sbMain.AppendFormat(" WHERE Main.RowNum <= {0} AND Main.RowNum > {1} ORDER BY RowNum ",
                        parameterRequest.PageIndex * parameterRequest.PageSize,
                        (parameterRequest.PageIndex - 1) * parameterRequest.PageSize);
                }

                var items = await ExecuteSqlCommandAsync<TEntity>(sbMain.ToString(), parameters.ToArray());

                var pageIndex = parameterRequest?.PageIndex ?? 1;
                var pageSize = parameterRequest?.PageSize ?? 5;

                pagedList = new PagedList<TEntity>
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    IndexFrom = 0,
                    TotalCount = count,
                    Items = items,
                    TotalPages = (int)Math.Ceiling(count / (double)pageSize)
                };
            }
            return pagedList;
        }
    }
}
