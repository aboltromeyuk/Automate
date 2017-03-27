using Automate.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.BLL.Interfaces
{
    public interface IPictureService
    {
        PictureDTO GetPicture(int id);
        IEnumerable<PictureDTO> GetPictures();
        void Create(PictureDTO picture);
        void Delete(int id);
        void Update(PictureDTO picture);
        void Dispose();
    }
}
