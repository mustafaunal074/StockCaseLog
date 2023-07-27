using StockCaseLog.Domain.Entities;
using StockCaseLog.Repository.Abstract;
using StockCaseLog.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockCaseLog.Repository.Concreate
{
    public class StockRepository : Service<Stock>, IStockRepository
    {
        private readonly StockCaseLogDbContext _context;
        public StockRepository(StockCaseLogDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
