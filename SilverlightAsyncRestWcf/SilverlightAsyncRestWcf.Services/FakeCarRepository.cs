using System;
using System.Collections.Generic;
using System.Linq;
using SilverlightAsyncRestWcf.Common;

namespace SilverlightAsyncRestWcf.Services
{
    public class FakeCarRepository : IRepository<Car>
    {
        private readonly IList<Car> _data;

        public FakeCarRepository()
        {
            _data = new List<Car>
                        {
                            new Car() {Id = 1, Make = "Rolls Royce", Model = "Bentley", Year = 1996},
                            new Car() {Id = 2, Make = "Ford", Model = "Fiesta", Year = 1996},
                            new Car() {Id = 3, Make = "Mercedes", Model = "C Class", Year = 1996},
                            new Car() {Id = 4, Make = "BMW", Model = "7 Series", Year = 1996},
                            new Car() {Id = 5, Make = "Jaguar", Model = "XKS", Year = 1996},
                            new Car() {Id = 6, Make = "Audi", Model = "R8", Year = 1996}
                        };
        }

        public Car GetById(string id)
        {
            int number;
            return Int32.TryParse(id, out number)
                       ? _data.SingleOrDefault(c => c.Id == number)
                       : null;
        }

        public IList<Car> GetAll()
        {
            return _data;
        }

        public void Insert(Car entity)
        {
            _data.Add(entity);
        }

        public void Update(Car entity)
        {
            var lookup = GetById(entity.Id.ToString());
            if (lookup == null) throw new Exception("Car not found");

            lookup.Make = entity.Make;
            lookup.Model = entity.Model;
            lookup.Year = entity.Year;
        }

        public void Delete(string id)
        {
            var lookup = GetById(id);
            if (lookup == null) throw new Exception("Car not found");
            _data.Remove(lookup);
        }
    }
}
