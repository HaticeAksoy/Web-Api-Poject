using AlphaStellarWebApi.Data;
using AlphaStellarWebApi.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AlphaStellarWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class MyVehicleController : Controller
    {
        private readonly IRepository<Cars> _carRepository;
        private readonly IRepository<Buses> _busRepository;
        private readonly IRepository<Boats> _boatRepository;

        public MyVehicleController(IRepository<Cars> carRepository, IRepository<Buses> busRepository, IRepository<Boats> boatRepository)
        {
            _carRepository = carRepository;
            _busRepository = busRepository;
            _boatRepository = boatRepository;
        }

        [HttpPost]
        [Route("AddCar")]
        public async Task<Cars> AddCar(Cars car)
        {
            return await _carRepository.Add(car);
        }
        [HttpPost]
        [Route("AddBus")]
        public async Task<Buses> AddBus(Buses bus)
        {
            return await _busRepository.Add(bus);
        }
        [HttpPost]
        [Route("AddBoat")]
        public async Task<Boats> AddBoat(Boats boat)
        {
            return await _boatRepository.Add(boat);
        }

        [HttpGet]
        [Route("GetAllCar")]
        public async Task<List<Cars>> GetAllCar()
        {
            var notes = await _carRepository.GetAll();

            var res = notes.Select(x => new Cars
            {
                Id = x.Id,
                Wheels = x.Wheels,
                Lights = x.Lights

            }).ToList();

            return res;

        }

        [HttpGet]
        [Route("CarsByColor/{color}")]
        public async Task<List<Cars>> GetCarsByColor(string color)
        {
            return await _carRepository.Filter(x => x.Color == color);
        }

        [HttpGet]
        [Route("BusesByColor/{color}")]
        public async Task<List<Buses>> GetBusesByColor(string color)
        {
            return await _busRepository.Filter(x => x.Color == color);
        }

        [HttpGet]
        [Route("BoatsByColor/{color}")]
        public async Task<List<Boats>> GetBoatsByColor(string color)
        {
            return await _boatRepository.Filter(x => x.Color == color);
        }
        [HttpDelete]
        [Route("DeleteCar/{id}")]
        public async Task<int> DeleteCar(int id)
        {
            return await _carRepository.DeleteCarById(id);
        }
        [HttpPut]
        [Route("UpdateLights/{id}")]
        public async Task<Cars> UpdateLights(int id)
        {
            var SelectedCar = await _carRepository.GetById(id);
            var statu = SelectedCar.Lights;

            if (statu == "On")
            {
                SelectedCar.Lights = "Off";
                await _carRepository.UpdateField(SelectedCar, x => x.Lights);
            }
            else
            {
                SelectedCar.Lights = "On";
                await _carRepository.UpdateField(SelectedCar, x => x.Lights);
            }


            return null;
        }



    }
}
