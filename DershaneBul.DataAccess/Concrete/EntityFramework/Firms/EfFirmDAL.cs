using DershaneBul.Core.DataAccess.Concrete.EntityFramework;
using DershaneBul.Core.Models.Concrete;
using DershaneBul.DataAccess.Abstract.Firms;
using DershaneBul.Entities.ComplexType;
using DershaneBul.Entities.Containers.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DershaneBul.DataAccess.Concrete.EntityFramework.Firms
{
    public class EfFirmDAL : PureSqlRepository, IFirmDAL
    {
        readonly DershaneBulDbContext _context;
        public EfFirmDAL(DershaneBulDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedList<FirmComplexTypeModel>> GetComplexFirmListByRequestAsync(RequestFirm request)
        {
            var sqlQuery = new StringBuilder();
            var prmLst = new List<SqlParameter>();

            sqlQuery.AppendFormat(@"SELECT DISTINCT(f.FirmId)
                                        ,DENSE_RANK() OVER(ORDER BY f.FirmId) AS RowNum
	                                    ,f.FirmName 
	                                    ,f.FirmDescription
	                                    ,f.CreationDate
	                                    ,u.FullName
	                                    ,u.UserName
	                                    ,a.Street
	                                    ,a.AddressDescription
	                                    ,a.AddressName
	                                    ,a.DoorNumber
	                                    ,city.CityName
	                                    ,t.TownName
                                    FROM Firm f {0}
                                    INNER JOIN FirmProgram fp {0} ON f.FirmId = fp.FirmId
                                    INNER JOIN Program p {0} ON fp.ProgramId = p.ProgramId
                                    INNER JOIN Address a {0} ON f.AddressId = a.AddressId
                                    INNER JOIN [User] u {0} ON f.UserId = u.UserId
                                    INNER JOIN City city {0} ON a.CityId = city.CityId
                                    INNER JOIN Town t {0} ON a.TownId = t.TownId
                                    WHERE 1=1 ", " WITH(NOLOCK)");

            if (request.FirmId != default(Guid))
            {
                var prmFirmId = new SqlParameter("prmFirmId", request.FirmId);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "f.FirmId", prmFirmId.ParameterName);
                prmLst.Add(prmFirmId);
            }
            if (request.ProgramId != default(Guid))
            {
                var paramProgramId = new SqlParameter("paramProgramId", request.ProgramId);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "p.ProgramId", paramProgramId.ParameterName);
                prmLst.Add(paramProgramId);
            }
            if (request.CityId > 0)
            {
                var paramCityId = new SqlParameter("paramCityId", request.CityId);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "city.CityId", paramCityId.ParameterName);
                prmLst.Add(paramCityId);
            }
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                var paramSearchText = new SqlParameter("paramSearchText", request.SearchText);
                sqlQuery.AppendFormat(" AND ( {0} like '%' + @{2} + '%' OR {1} like '%' + @{2} + '%' ) ", "f.FirmName", "f.FirmDescription",
                    paramSearchText.ParameterName);
                prmLst.Add(paramSearchText);
            }

            request.AdditionalOrderParameter = "f.FirmId";
            var firmsPagedList = await GetPagedListSqlQuery<FirmComplexTypeModel>(sqlQuery.ToString(), prmLst, request);
            return firmsPagedList;
        }
         

        public async Task<List<FirmContactComplexTypeModel>> GetFirmContactComplexTypeByRequestAsync(RequestFirm request)
        {
            var sqlQuery = new StringBuilder();
            var prmLst = new List<SqlParameter>();

            sqlQuery.AppendFormat(@"SELECT                              
                                    F.FirmId,
                                    FP.FirmProgramId,
                                    C.ContactDescription,
                                    C.ContactTypeId,
                                    CT.ContactTypeDescription,
                                    CT.Icon, 
                                    FROM Firm F  
                                    INNER JOIN FirmProgram FP {0} ON F.FirmId=FP.FirmId 
                                    INNER JOIN Program P {0} ON P.ProgramId=FP.ProgramId
                                    INNER JOIN Contact C {0} ON C.FirmProgramId= FP.FirmProgramId
                                    INNER JOIN ContactType CT {0} ON CT.ContactTypeId=C.ContactTypeId 
                                    WHERE 1=1 ", " WITH(NOLOCK)");

            if (request.FirmId != default(Guid))
            {
                var prmFirmId = new SqlParameter("prmFirmId", request.FirmId);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "f.FirmId", prmFirmId.ParameterName);
                prmLst.Add(prmFirmId);
            }
            if (request.ProgramId != default(Guid))
            {
                var paramProgramId = new SqlParameter("paramProgramId", request.ProgramId);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "p.ProgramId", paramProgramId.ParameterName);
                prmLst.Add(paramProgramId);
            }
            if (request.ProgramId != default(Guid))
            {
                var paramFirmProgramId = new SqlParameter("paramFirmProgramId", request.FirmProgramId);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "p.FirmProgramId", paramFirmProgramId.ParameterName);
                prmLst.Add(paramFirmProgramId);
            }

            return await ExecuteSqlCommandAsync<FirmContactComplexTypeModel>(sqlQuery.ToString(), prmLst);
        }

        public Task<FirmComplexTypeModel> GetFirmComplexTypeByRequestAsync(RequestFirm request)
        {
            var sqlQuery = new StringBuilder();
            var prmLst = new List<SqlParameter>();

            sqlQuery.AppendFormat(@"SELECT DISTINCT(f.FirmId)
                                        ,DENSE_RANK() OVER(ORDER BY f.FirmId) AS RowNum
	                                    ,f.FirmName 
	                                    ,f.FirmDescription
	                                    ,f.CreationDate
	                                    ,u.FullName
	                                    ,u.UserName
	                                    ,a.Street
	                                    ,a.AddressDescription
	                                    ,a.AddressName
	                                    ,a.DoorNumber
	                                    ,city.CityName
	                                    ,t.TownName
                                    FROM Firm f {0}
                                    INNER JOIN FirmProgram fp {0} ON f.FirmId = fp.FirmId
                                    INNER JOIN Program p {0} ON fp.ProgramId = p.ProgramId
                                    INNER JOIN Address a {0} ON f.AddressId = a.AddressId
                                    INNER JOIN [User] u {0} ON f.UserId = u.UserId
                                    INNER JOIN City city {0} ON a.CityId = city.CityId
                                    INNER JOIN Town t {0} ON a.TownId = t.TownId
                                    WHERE 1=1 ", " WITH(NOLOCK)");

            if (request.FirmId != default(Guid))
            {
                var prmFirmId = new SqlParameter("prmFirmId", request.FirmId);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "f.FirmId", prmFirmId.ParameterName);
                prmLst.Add(prmFirmId);
            }
            if (request.ProgramId != default(Guid))
            {
                var paramProgramId = new SqlParameter("paramProgramId", request.ProgramId);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "p.ProgramId", paramProgramId.ParameterName);
                prmLst.Add(paramProgramId);
            }
            if (request.CityId > 0)
            {
                var paramCityId = new SqlParameter("paramCityId", request.CityId);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "city.CityId", paramCityId.ParameterName);
                prmLst.Add(paramCityId);
            }
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                var paramSearchText = new SqlParameter("paramSearchText", request.SearchText);
                sqlQuery.AppendFormat(" AND ( {0} like '%' + @{2} + '%' OR {1} like '%' + @{2} + '%' ) ", "f.FirmName", "f.FirmDescription",
                    paramSearchText.ParameterName);
                prmLst.Add(paramSearchText);
            }

            request.AdditionalOrderParameter = "f.FirmId";
            var firmsPagedList =ExecuteSqlCommandAsync<FirmComplexTypeModel>(sqlQuery.ToString(),prmLst).Result.ToAsyncEnumerable().FirstOrDefault();
            return firmsPagedList;
        }
    }
}
