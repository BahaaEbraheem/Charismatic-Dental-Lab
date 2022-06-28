using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Charismatic.EntityFrameworkCore
{
    public static class CharismaticDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CharismaticDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CharismaticDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
