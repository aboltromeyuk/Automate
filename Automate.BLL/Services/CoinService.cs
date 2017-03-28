using AutoMapper;
using Automate.BLL.DTO;
using Automate.BLL.Interfaces;
using Automate.DAL.Entities;
using Automate.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.BLL.Services
{
    public class CoinService : ICoinService
    {
        IUnitOfWork Database { get; set; }

        public CoinService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public CoinDTO GetCoin(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Coin, CoinDTO>());
            return Mapper.Map<Coin, CoinDTO>(Database.Coins.Get(id));
        }

        public CoinDTO GetCoinByNominal(int nominal)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Coin, CoinDTO>());
            return Mapper.Map<Coin, CoinDTO>(Database.Coins.Find(c => c.Nominal == nominal).Single());
        }

        public IEnumerable<CoinDTO> GetCoins()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Coin, CoinDTO>());
            return Mapper.Map<IEnumerable<Coin>, List<CoinDTO>>(Database.Coins.GetAll());
        }

        public void Create(CoinDTO coin)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CoinDTO, Coin>());
            Database.Coins.Create(Mapper.Map<CoinDTO, Coin>(coin));
        }

        public void Delete(int id)
        {
            Database.Coins.Delete(id);
        }

        public void Update(CoinDTO coin)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CoinDTO, Coin>());
            Database.Coins.Update(Mapper.Map<CoinDTO, Coin>(coin));
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<CoinDTO> ReturnChange(int sum)
        {
            int maxNominal = 0;
            CoinDTO[] coins = this.GetCoins().ToArray();
            var coinsOfChange = new List<CoinDTO>();
            var coinTemp = new CoinDTO();
            var sumTemp = sum;
            int k = 0;

            while (sumTemp != 0)
            {
                for(var i=0; i<coins.Length; i++)
                {
                    if (coins[i].Nominal > maxNominal && coins[i].Number > 0 && sumTemp >= coins[i].Nominal)
                    {
                        maxNominal = coins[i].Nominal;
                        coinTemp = coins[i];
                        k = i;
                    }
                    else if (i == coins.Length - 1 && maxNominal > sumTemp) return coinsOfChange;
                }

                    if (maxNominal <= sumTemp)
                    {
                        coinsOfChange.Add(new CoinDTO
                        {
                            Id = coinTemp.Id,
                            Nominal = coinTemp.Nominal,
                            Number = coinTemp.Number,
                            Blocked = coinTemp.Blocked
                        });

                        coins[k].Nominal -= 1;
                        sumTemp -= maxNominal;
                    }
                }
            return coinsOfChange;
        }
        
        }
    }

