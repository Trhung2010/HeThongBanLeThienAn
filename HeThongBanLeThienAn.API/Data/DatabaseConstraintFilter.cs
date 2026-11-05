using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MiniSupermarket.API.Data;

// The database is the final guard when concurrent requests pass the prechecks.
public sealed class DatabaseConstraintFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DbUpdateConcurrencyException)
        {
            context.Result = new ConflictObjectResult(new { message = "Dữ liệu đã thay đổi. Vui lòng tải lại trước khi lưu." });
            context.ExceptionHandled = true;
            return;
        }
        if (context.Exception is DbUpdateException { InnerException: SqlException sql }
            && sql.Number is 2601 or 2627 or 547)
        {
            context.Result = new ConflictObjectResult(new
            {
                message = sql.Number == 547
                    ? "Dữ liệu đang được sử dụng hoặc dữ liệu liên quan đã thay đổi. Vui lòng tải lại."
                    : "Dữ liệu bị trùng. Vui lòng kiểm tra mã vạch, tên hoặc số điện thoại."
            });
            context.ExceptionHandled = true;
        }
    }
}
