using Automate.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.BLL.Interfaces
{
    public interface ICoinService
    {
        CoinDTO GetCoin(int id);
        CoinDTO GetCoinByNominal(int nominal);
        IEnumerable<CoinDTO> ReturnChange(int sum);
        IEnumerable<CoinDTO> GetCoins();
        
        void Create(CoinDTO coin);
        void Delete(int id);
        void Update(CoinDTO coin);
        void Dispose();
    }
}
