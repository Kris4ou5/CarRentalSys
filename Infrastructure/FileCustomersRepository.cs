using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Entities;

namespace CarRentalSys.Infrastructure
{
    public class FileCustomersRepository
    {
        private readonly FileStorage _storage;

        public FileCustomersRepository(FileStorage storage)
        {
            _storage = storage;
        }

        public IReadOnlyList<Customer> GetAll()
        {
            var saved = _storage.Load();
            return saved.Customers;
        }
    }
}
