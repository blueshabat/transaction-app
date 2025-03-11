using Core.Infrastructure.Database;
using Dapper;
using System.Data;
using Transaction.Domain.Repositories;

namespace Transaction.Infrastructure.Transaction
{
    public class TransactionRepository(DapperContext context) : ITransactionRepository
    {
        public async Task<int> Create(Domain.Entities.Transaction transaction, CancellationToken cancellationToken = default)
        {
            var query = @"
                INSERT INTO DBO.TRANSACTIONS
                      (TYPE
                      ,AMOUNT
                      ,DATE
                      ,DESCRIPTION
                      ,CATEGORY
                      ,BALANCE)
                VALUES
                      (@TYPE
                      ,@AMOUNT
                      ,@DATE
                      ,@DESCRIPTION
                      ,@CATEGORY
                      ,@BALANCE);
                
                SELECT CAST(SCOPE_IDENTITY() AS INT)
            ";
            var parameters = new DynamicParameters();
            parameters.Add("TYPE", transaction.Type, DbType.String);
            parameters.Add("AMOUNT", transaction.Amount, DbType.Decimal);
            parameters.Add("DATE", transaction.Date, DbType.DateTime);
            parameters.Add("DESCRIPTION", transaction.Description, DbType.String);
            parameters.Add("CATEGORY", transaction.Category, DbType.String);
            parameters.Add("BALANCE", transaction.Balance, DbType.Decimal);
            using var connection = context.CreateConnection();
            return await connection.QuerySingleAsync<int>(query, parameters);
        }

        public async Task<int> Delete(int id, CancellationToken cancellationToken = default)
        {
            var query = @"
                DELETE DBO.TRANSACTIONS WHERE ID = @ID; 
                SELECT @ID";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.Int32);
            using var connection = context.CreateConnection();
            return await connection.QuerySingleAsync<int>(query, parameters);
        }

        public async Task<Domain.Entities.Transaction?> GetById(int id, CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT ID Id
                      ,TYPE Type
                      ,AMOUNT Amount
                      ,DATE Date
                      ,DESCRIPTION Description
                      ,CATEGORY Category
                      ,BALANCE Balance
                  FROM DBO.TRANSACTIONS
                  WHERE ID = @ID;
            ";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.Int32);
            using var connection = context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Domain.Entities.Transaction>(query, parameters);
        }

        public async Task<Domain.Entities.Transaction?> GetLast(CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT TOP 1 ID Id
                      ,TYPE Type
                      ,AMOUNT Amount
                      ,DATE Date
                      ,DESCRIPTION Description
                      ,CATEGORY Category
                      ,BALANCE Balance
                  FROM DBO.TRANSACTIONS
                  ORDER BY ID DESC;
            ";
            using var connection = context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Domain.Entities.Transaction>(query);
        }

        public async Task<IEnumerable<Domain.Entities.Transaction>> List(CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT ID Id
                      ,TYPE Type
                      ,AMOUNT Amount
                      ,DATE Date
                      ,DESCRIPTION Description
                      ,CATEGORY Category
                      ,BALANCE Balance
                  FROM DBO.TRANSACTIONS
                 ORDER BY ID DESC;
            ";
            using var connection = context.CreateConnection();
            return await connection.QueryAsync<Domain.Entities.Transaction>(query, cancellationToken);
        }

        public async Task<int> Update(Domain.Entities.Transaction transaction, CancellationToken cancellationToken = default)
        {
            var query = @"
                UPDATE DBO.TRANSACTIONS
                SET TYPE = @TYPE
                    ,AMOUNT = @AMOUNT
                    ,DATE = @DATE
                    ,DESCRIPTION = @DESCRIPTION
                    ,CATEGORY = @CATEGORY
                    ,BALANCE = @BALANCE
                WHERE ID = @ID;

                SELECT @ID
            ";
            var parameters = new DynamicParameters();
            parameters.Add("ID", transaction.Id, DbType.Int32);
            parameters.Add("TYPE", transaction.Type, DbType.String);
            parameters.Add("AMOUNT", transaction.Amount, DbType.Decimal);
            parameters.Add("DATE", transaction.Date, DbType.DateTime);
            parameters.Add("DESCRIPTION", transaction.Description, DbType.String);
            parameters.Add("CATEGORY", transaction.Category, DbType.String);
            parameters.Add("BALANCE", transaction.Balance, DbType.Decimal);
            using var connection = context.CreateConnection();
            return await connection.QuerySingleAsync<int>(query, parameters);
        }
    }
}
