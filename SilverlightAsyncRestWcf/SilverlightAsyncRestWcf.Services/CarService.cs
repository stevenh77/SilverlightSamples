using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using SilverlightAsyncRestWcf.Common;

namespace SilverlightAsyncRestWcf.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CarService : IService<Car>
    {
        private readonly IRepository<Car> _repo;

        public CarService()
        {
            _repo = new FakeCarRepository();
        }

        public CarService(IRepository<Car> repo)
        {
            _repo = repo;
        }

        [WebGet(UriTemplate = "Cars", ResponseFormat = WebMessageFormat.Json)]
        public IList<Car> GetAll()
        {
            var cars = _repo.GetAll();
            return cars;
        }

        [WebGet(UriTemplate = "Car/{id}", ResponseFormat = WebMessageFormat.Json)]
        public Car Get(string id)
        {
            var car = _repo.GetById(id);
            return car;
        }

        [WebInvoke(UriTemplate = "Car", Method = "POST")]
        public void Insert(Car car)
        {
            _repo.Insert(car);
        }

        [WebInvoke(UriTemplate = "Car/{id}", Method = "PUT")]
        public void Update(string id, Car car)
        {
            _repo.Update(car);
        }
     
        [WebInvoke(UriTemplate = "Car/{id}", Method = "DELETE")]
        public void Delete(string id)
        {
            _repo.Delete(id);
        }
    }
}