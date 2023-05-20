using AwBll.Interfaces.Sales;
using AwBll.Interfaces.Sales;
using Common.Runtime;
using Entities.Sales;
using Microsoft.AspNetCore.Mvc;

namespace jcmartinezc_isiipDSI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReasonController : Controller
    {
        private readonly ISalesReasonBLL _bll;
        public SalesReasonController( ISalesReasonBLL salesReasonBLL)
        {
            _bll = salesReasonBLL;
        }

        /// <summary>
        /// Retorna listado de cateogrías
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SalesReason>> Get()
        {
            return await _bll.GetAll();
        }

        /// <summary>
        /// Obtiene una categoría que tiene como id el valor especificado
        /// </summary>
        /// <param name="id">Identificador de la categoría</param>
        /// <returns><see cref="SalesReason"/></returns>
        [HttpGet("{id}")]
        public async Task<SalesReason> Get(int id)
        {
            return await _bll.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salesReasonName"></param>
        /// <param name="reasonTypeName"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public async Task<ExecutionResult> Save(string salesReasonName, string reasonTypeName)
        {
            return await _bll.Save(salesReasonName, reasonTypeName);
        }
    }
}
