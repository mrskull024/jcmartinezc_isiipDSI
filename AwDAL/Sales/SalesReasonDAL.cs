using AwDal.Common;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Common.Runtime;
using Entities.Sales;

namespace AwDal.Production
{
    public class SalesReasonDAL : AwBaseDal
    {
        public SalesReasonDAL(string connectionString) : base(connectionString)
        {
        }
        public List<SalesReason> GetAll()
        {
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand command = new()
            {
                Connection = conn,
                CommandText = "Sales.usp_GetSalesReason",
                CommandType = CommandType.StoredProcedure
            };

            DataTable dt = new();
            SqlDataAdapter adapter = new(command);
            adapter.Fill(dt);

            var query = dt.AsEnumerable().Select(x => new SalesReason
            {
                SalesReasonID = Convert.ToInt32(x["SalesReasonID"]),
                Name = Convert.ToString(x["Name"]),
                ReasonType = Convert.ToString(x["ReasonType"]),
                ModifiedDate = Convert.ToDateTime(x["ModifiedDate"])
            });

            return query.ToList();
        }

        public async Task<SalesReason> GetById(int salesReasonId)
        {
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand command = new()
            {
                Connection = conn,
                CommandText = "Sales.usp_GetSalesReasonById",
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@salesReasonId", salesReasonId);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            SalesReason category = new();
            if (reader.Read())
            {
                category.SalesReasonID = reader.GetInt32("SalesReasonID");
                category.Name = reader.GetString("Name");
                category.ReasonType = reader.GetString("ReasonType");
                category.ModifiedDate = reader.GetDateTime("ModifiedDate");
            }
            return category;
        }

        public async Task<int> Create(string salesReasonName)
        {
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlParameter parameter = new()
            {
                ParameterName = "@name",
                Value = salesReasonName
            };
            SqlCommand command = new()
            {
                Connection = conn,
                CommandText = "Production.uspCreateCategory",
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(parameter);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<ExecutionResult> Save(string salesReasonName, string salesReasonType)
        {
            ExecutionResult result = new ();

            try
            {
                SqlConnection conn = GetConnection();
                conn.Open();
                SqlParameter parameter = new()
                {
                    ParameterName = "@name",
                    Value = salesReasonName
                };
                SqlParameter parameter2 = new()
                {
                    ParameterName = "@reasonType",
                    Value = salesReasonType
                };
                SqlCommand command = new()
                {
                    Connection = conn,
                    CommandText = "Sales.usp_SaveSalesReason",
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(parameter);
                command.Parameters.Add(parameter2);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    result.Outcome = reader.GetBoolean("Outcome");
                    result.Id = reader.GetInt32("Id");
                    result.Message = reader.GetString("Message");
                }
                else
                {
                    result = new ExecutionResult { Outcome = false, Message = "No se obtuvo respuesta desde la base de datos." };
                }
            }
            catch (Exception ex)
            {

                result = new ExecutionResult { Outcome = false, Message = ex.Message };
            }

            return result;
        }
    }
}
