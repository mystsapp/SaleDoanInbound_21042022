using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetAll();

        Task<Hotel> GetByIdAsync(decimal id);

        IEnumerable<Hotel> Find(Func<Hotel, bool> predicate);
    }
    public class HotelRepository : IHotelRepository
    {
        private readonly qltourContext _qltourContext;

        public HotelRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }

        public IEnumerable<Hotel> Find(Func<Hotel, bool> predicate)
        {
            return _qltourContext.Hotel.Where(predicate);
        }

        public IEnumerable<Hotel> GetAll()
        {
            return _qltourContext.Hotel;
        }

        public async Task<Hotel> GetByIdAsync(decimal id)
        {
            return await _qltourContext.Hotel.FindAsync(id);
        }
    }
}
