
using DershaneBul.Core.Models.Concrete;
using DershaneBul.Entities.Abstract;
using DershaneBul.Entities.Containers.Response;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DershaneBul.Core.DataAccess.Abstract
{
    public interface IPureSqlRepository
    {
        int ExecuteScalarSqlCommandAsync(string sql, params object[] parameters);

        Task<List<TEntity>> ExecuteSqlCommandAsync<TEntity>(string sql, params object[] parameters)
            where TEntity : class, IBaseEntity, new();

        Task<PagedList<TEntity>> GetPagedListSqlQuery<TEntity>(string sqlQuery, List<SqlParameter> parameters,
            BaseRequest parameterModel) where TEntity : class, IBaseEntity, new();

        //Tuple<string, int> GetSqlQueryString<TEntity>(string sqlQuery, List<SqlParameter> parameters,
        //   BaseRequest parameterRequest);

        //PagedList<TEntity> GetPagedListSqlModel<TEntity>(List<TEntity> items, int count, BaseRequest parameterRequest);
    }
}
