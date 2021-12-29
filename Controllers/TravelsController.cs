using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RideShare.Data;
using RideShare.Dtos;
using RideShare.Models;

namespace RideShare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelsController : ControllerBase
    {
        private readonly ITravelRepo _repository;
        private readonly IMapper _mapper;
        public TravelsController(ITravelRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TravelReadDto>> GetTravels(string? to, string? from)
        {
            var result = _repository.GetAllTravels();
            if (!string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(from))
            {
                result = result.Where(
                    x => x.To.Contains(to) &&
                    x.From.Contains(from) &&
                    x.Active == true
                );
            }
            else if (!string.IsNullOrEmpty(to) && string.IsNullOrEmpty(from))
            {
                result = result.Where(
                    x => x.To.Contains(to.ToLower()) &&
                    x.Active == true
                );
            }
            else if (string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(from))
            {
                result = result.Where(
                    x => x.To.Contains(from.ToLower()) &&
                    x.Active == true
                );
            }

            if (result != null && result.Any())
                return Ok(_mapper.Map<IEnumerable<TravelReadDto>>(result));

            return NotFound("There is no Travel according your request.");
        }

        [HttpGet("{id}")]
        public ActionResult<TravelReadDto> GetTravelById(int id)
        {
            var travel = _repository.GetTravelById(id);

            if (travel != null)
            {
                return Ok(_mapper.Map<TravelReadDto>(travel));
            }
            return NotFound($"Travel not found with Id : {id}");
        }

        [HttpPost]
        public ActionResult<TravelReadDto> CreateTravel([FromBody] TravelCreateDto travelCreateDto)
        {
            var travelModel = _mapper.Map<Travel>(travelCreateDto);
            travelModel.Date = DateTime.Now;
            _repository.CreateTravel(travelModel);
            _repository.SaveChanges();

            var travelReadDto = _mapper.Map<TravelReadDto>(travelModel);
            return Ok(travelReadDto);
        }

        [HttpPut]
        [Route("{id}/publish")]
        public ActionResult<TravelReadDto> PublishTravel([FromRoute] int id)
        {
            var travel = _repository.GetTravelById(id);
            if (travel != null)
            {
                travel.Active = true;
                _repository.SaveChanges();
                return Ok(_mapper.Map<TravelReadDto>(travel));
            }
            return NotFound("Travel not exists.");
        }

        [HttpPut]
        [Route("{id}/unpublish")]
        public ActionResult<TravelReadDto> UnPublishTravel([FromRoute] int id)
        {
            var travel = _repository.GetTravelById(id);
            if (travel != null)
            {
                travel.Active = false;
                _repository.SaveChanges();
                return Ok(_mapper.Map<TravelReadDto>(travel));
            }
            return NotFound("Travel not exists.");
        }

        [HttpPut]
        [Route("{id}/book")]
        public ActionResult<TravelReadDto> BookTravel([FromRoute] int id,[FromBody] int userId)
        {   
            if (userId <= 0)
            {
                return BadRequest("Enter correct user Id.");
            }
            var user = _repository.GetUserById(userId);

            var travel = _repository.GetTravelById(id);
            if (travel == null)
            {
                return NotFound($"No Travel Found for id: {id}");
            }
            
            if (travel.SeatNumber > travel.BookedNumber)
            {
                var userTrips = _repository.GetAllTrips();
                foreach (var item in userTrips)
                {
                    if (item.UserId == userId && item.TravelId == id)
                    {
                        return BadRequest($"You are already booked in this travel with Id: {id}.");
                    }
                }
                travel.BookedNumber = travel.BookedNumber + 1;

                Trip trip = new Trip();
                trip.Travel = travel;
                trip.TravelId = id;
                trip.User = user;
                trip.UserId = userId;

                _repository.SaveChanges();
                return Ok(_mapper.Map<TravelReadDto>(travel));
            }
            return BadRequest($"Not Enough Seat For This Travel {id}.");
        }
    }
}