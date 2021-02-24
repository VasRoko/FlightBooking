using FlightBooking.Infrastructure.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBooking.Infrastructure
{
    public class FileManager<T> : IFileManager<T> where T : class
    {
        private readonly IStreamer _stream;

        public FileManager(IStreamer stream)
        {
            _stream = stream;
        }
        public async Task WriteToJsonAsync(string path, List<T> entity)
        {
            try
            {
                var jsonObject = JsonConvert.SerializeObject(entity, Formatting.Indented);
                using (var writer = _stream.StreamWriter(path))
                {
                    await writer.WriteAsync(jsonObject);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<T>> ReadFromJsonAsync(string path)
        {
            try
            {
                using (var reader = _stream.StreamReader(path))
                {
                    var result = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<List<T>>(result);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
