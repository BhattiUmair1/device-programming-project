using Project.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface IFlightsRepository
    {
        public Task<List<Flights>> GetFlightAsync();
        public Task AddFlightAsync();
        public Task DeleteFlightAsync();
    }
}
