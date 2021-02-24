using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Infrastructure.Interfaces
{
    public interface IFileManager<T>
    {
        Task WriteToJsonAsync(string path, List<T> entity);
        Task<List<T>> ReadFromJsonAsync(string path);
    }
}
