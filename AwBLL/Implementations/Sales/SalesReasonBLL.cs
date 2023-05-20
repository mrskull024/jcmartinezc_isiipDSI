using AwBll.Interfaces.Sales;
using AwDal.Production;
using Common.Runtime;
using Entities.Sales;
using Microsoft.Extensions.Configuration;

namespace AwBll.Implementations.Sales
{
    public class SalesReasonBLL : ISalesReasonBLL
    {
        private readonly IConfiguration _configuration;
        private readonly SalesReasonDAL dbSalesReason;
        public SalesReasonBLL(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString(name: "AwConnectionString");
            dbSalesReason = new SalesReasonDAL(connectionString);
        }

        public Task<int> Create(string categoryName)
        {
            return dbSalesReason.Create(categoryName);
        }

        public async Task<List<SalesReason>> GetAll()
        {
           return dbSalesReason.GetAll();
        }

        public async Task<SalesReason> GetById(int categoryId)
        {
            return await dbSalesReason.GetById(categoryId); 
        }

        public Task<ExecutionResult> Save(string salesReasonName, string salesReasonType)
        {
            return dbSalesReason.Save(salesReasonName, salesReasonType);
        }
    }
}
