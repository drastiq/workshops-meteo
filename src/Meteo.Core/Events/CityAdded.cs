namespace Meteo.Core.Events
{
    public class CityAdded : IEvent
    {
        public string Name { get; set; }
    }
}