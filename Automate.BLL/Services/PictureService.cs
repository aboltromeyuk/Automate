using Automate.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automate.BLL.DTO;
using Automate.DAL.Interfaces;
using AutoMapper;
using Automate.DAL.Entities;

namespace Automate.BLL.Services
{
    public class PictureService : IPictureService
    {
        IUnitOfWork Database { get; set; }

        public PictureService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public PictureDTO GetPicture(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Picture, PictureDTO>());
            return Mapper.Map<Picture, PictureDTO>(Database.Pictures.Get(id));
        }

        public IEnumerable<PictureDTO> GetPictures()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Picture, PictureDTO>());
            return Mapper.Map<IEnumerable<Picture>, List<PictureDTO>>(Database.Pictures.GetAll());
        }

        public void Create(PictureDTO picture)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PictureDTO, Picture>());
            Database.Pictures.Create(Mapper.Map<PictureDTO, Picture>(picture));
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Pictures.Delete(id);
            Database.Save();
        }

        public void Update(PictureDTO picture)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PictureDTO, Picture>());
            Database.Pictures.Update(Mapper.Map<PictureDTO, Picture>(picture));
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
