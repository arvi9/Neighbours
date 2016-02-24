namespace Neighbours.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;
    using Neighbours.Data.Common.Repositories;
    using Neighbours.Data.Models;

    public class CitiesService : ICitiesService
    {
        private IRepository<City> cities;

        public CitiesService(IRepository<City> cities)
        {
            this.cities = cities;
        }

        public City Add(City model)
        {
            throw new NotImplementedException();
        }

        public City Add(string name)
        {
            var model = this.cities.All().FirstOrDefault(d => d.Name == name);
            if (model == null)
            {
                model = new City()
                {
                    Name = name
                };

                this.cities.Add(model);
                this.cities.SaveChanges();
            }

            return model;
        }

        public IQueryable<City> GetAll()
        {
            return this.cities.All();
        }

        public City GetById(int id)
        {
            return this.cities.GetById(id);
        }
    }
}
