using Common.Runtime;
using Entities.Sales;

namespace AwBll.Interfaces.Sales
{
    public interface ISalesReasonBLL
    {
        Task<List<SalesReason>> GetAll();
        Task<SalesReason> GetById(int salesReasonId);
        Task<int> Create(string salesReasonName);
        Task<ExecutionResult> Save(string salesReasonName, string salesReasonType);
    }
}
