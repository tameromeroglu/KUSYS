using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Data
{
    public class DbContextTransactionWrapper
    {
        private IDbContextTransaction _transaction { get; set; }
        private DbContext _context { get; set; }

        public DbContextTransactionWrapper(DbContext context)
        {
            _context = context;
            _transaction = context.Database.BeginTransaction();
        }

        public IDbContextTransaction GetTransaction()
        {
            return _transaction;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            DisposeTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            DisposeTransaction();
        }

        public void DisposeTransaction()
        {
            _transaction.Dispose();
        }
    }
}
