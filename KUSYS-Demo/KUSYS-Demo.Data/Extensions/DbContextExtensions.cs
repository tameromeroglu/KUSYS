using KUSYS_Demo.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task<TEntity> AddAsyncExt<TEntity>(this KusysContext context, TEntity entity) where TEntity : class
        {
            await context.Set<TEntity>().AddAsync(entity);
            var result = await context.SaveChangesAsync();

            return result > 0 ? entity : null;
        }


        public static async Task<TEntity> UpdateAsyncExt<TEntity>(this KusysContext context, TEntity entity)
      where TEntity : class
        {
            context.Set<TEntity>().Update(entity);

            var result = await context.SaveChangesAsync();

            return result > 0 ? entity : null;
        }

        public static async Task<bool> DeleteAsyncExt<TEntity>(this KusysContext context, TEntity entity)
           where TEntity : class
        {

            context.Set<TEntity>().Remove(entity);

            var result = await context.SaveChangesAsync();

            return result > 0;

        }

        public static DbContextTransactionWrapper BeginTransactionWrapper(this KusysContext context)
        {
            return new DbContextTransactionWrapper(context);
        }

    }
}
