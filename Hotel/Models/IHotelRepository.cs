using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public interface IHotelRepository
    {
        void Add(HotelModel hotelModel);
        void Edit(HotelModel hotelModel);
        void Delete(int id);
        IEnumerable<HotelModel> GetAll();
        IEnumerable<HotelModel> GetByValue(string value);//Searchs

    }
}