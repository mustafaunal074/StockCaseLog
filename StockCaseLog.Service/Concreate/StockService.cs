using StockCaseLog.Domain.Entities;
using StockCaseLog.Repository.Abstract;
using StockCaseLog.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockCaseLog.Service.Concreate
{
    public class StockService : Service<Stock>, IStockService
    {
        private readonly IStockRepository _repository;

        public StockService(IStockRepository repository)
        {
            _repository = repository;
        }

        public List<Stock> GetAllProductCodeAsync(string productCode)
        {
            var stockProduct = this.GetAllAsync().Result
                .Where(x=>x.ProductCode == productCode)
                .ToList();
            return stockProduct;
        }

        public List<Stock> GetAllVariantCodeAsync(string variantCode)
        {
            var stockVariant = _repository.GetAllAsync();
            var stockVariant1 = stockVariant.Result
                .Where(x => x.VariantCode == variantCode)
                .ToList();
            return stockVariant1;
        }

        public void UpdateCustomersVariantDelete(string variantCode)
        {
            var stockVariant = _repository.GetAllAsync();
            var stockVariant1 = stockVariant.Result
                .Where(x => x.VariantCode == variantCode)
                .ToList();
            _repository.RemoveRange(stockVariant1);
        }

        public List<Stock> UpdateCustomersVariantStock(string variantCode, int quantity)
        {
            var stockList = this.GetAllAsync().Result
                .Where(x => x.VariantCode == variantCode).
                ToList();

            if (stockList.Count > 0)
            {
                foreach (var stock in stockList)
                {
                    stock.Quantity = quantity;
                    _repository.Update(stock);
                }
            }
            return stockList;
        }
    }
}
