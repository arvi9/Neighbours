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

    public class DistrictsService : IDistrictsService
    {
        private IRepository<District> districts;

        public DistrictsService(IRepository<District> districts)
        {
            this.districts = districts;
        }

        public District Add(District model)
        {
            throw new NotImplementedException();
        }

        public District Add(string name)
        {
            var model = this.districts.All().FirstOrDefault(d => d.Name == name);
            if (model == null)
            {
                model = new District()
                {
                    Name = name
                };

                this.districts.Add(model);
                this.districts.SaveChanges();
            }

            return model;
        }

        public IQueryable<District> GetAll()
        {
            return this.districts.All();
        }

        public District GetById(int id)
        {
            return this.districts.GetById(id);
        }
    }
}
