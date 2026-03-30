using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace CarRentalSys.Infrastructure
{
    public class FileStorage
    {
        private readonly string path; 

        public FileStorage(string path)
        {
            this.path = path;
        }

        public CarRentalStorage Load()
        {
            if(!File.Exists(path))
            {
                return new CarRentalStorage();
            }    

            var json = File.ReadAllText(path);

            var storage = JsonSerializer.Deserialize<CarRentalStorage>(json);

            return storage;
        }

        public void Save(CarRentalStorage storage)
        {
            var json = JsonSerializer.Serialize(storage);
            File.WriteAllText(path, json);
        }
        

    }
}
