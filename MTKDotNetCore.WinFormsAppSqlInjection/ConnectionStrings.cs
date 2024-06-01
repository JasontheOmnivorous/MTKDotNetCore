using System.Data.SqlClient;

namespace MTKDotNetCore.WinFormsAppSqlInjection;

internal static class ConnectionStrings
{
    public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "MTKDotNetCore",
        UserID = "sa",
        Password = "sa@123",
        TrustServerCertificate = true
    };
}
