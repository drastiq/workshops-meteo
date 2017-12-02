using System;

namespace Meteo.Core.Domain
{
    public class City
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        protected City()
        {
        }

        public City(string name)
        {
            Id = Guid.NewGuid();
            Name = name.ToLowerInvariant();
        }
    }
}