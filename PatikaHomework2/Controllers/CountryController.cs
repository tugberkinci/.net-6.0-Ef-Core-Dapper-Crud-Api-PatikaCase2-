using Microsoft.AspNetCore.Mvc;
using PatikaHomework2.Data.Model;
using PatikaHomework2.Dto.Response;
using PatikaHomework2.Dto.Dto;
using PatikaHomework2.Service.IServices;
using AutoMapper;

namespace PatikaHomework2.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        

        public CountryController(ICountryService countryService,IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }


        



        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Country>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var country = await Task.Run(() => _countryService.GetAll().Result);
            GenericResponse<IEnumerable<Country>> response = new GenericResponse<IEnumerable<Country>>();
            response.Success = true;
            response.Message = null;
            response.Data = country;

            return Ok(response);
        }


        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        /// <response code="404">Returns error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var country = await Task.Run(() => _countryService.GetById(id));
            GenericResponse<Country> response = new GenericResponse<Country>();

            if (country == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = country;
            return Ok(response);
        }



        /// <summary>
        /// post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="400">Returns error</response>
        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(CountryDto model)
        {
            GenericResponse<Country> response = new GenericResponse<Country>();
            var entity = _mapper.Map<CountryDto, Country>(model);
            var result = await Task.Run(() => _countryService.Add(entity));
            if(result == null)
            {
                response.Success = false;
                response.Message = "An error ocurred.";
                response.Data = null;
                return BadRequest(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = result;

            return Created("",response);

   
        }



        /// <summary>
        /// update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="400">Returns error</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(int id,CountryDto model)
        {
            GenericResponse<Country> response = new GenericResponse<Country>();
            var country = await Task.Run(() => _countryService.GetById(id));
            if (country == null)
            {
                response.Success = false;
                response.Message = "Does not exist. Please check id.";
                response.Data = null;
                return NotFound(response);
            }

            var entity = _mapper.Map<CountryDto, Country>(model);

            country.Continent = !String.IsNullOrEmpty(entity.Continent) ? entity.Continent : country.Continent;
            country.CountryName = !String.IsNullOrEmpty(entity.CountryName) ? entity.CountryName : country.CountryName;
            country.Currency = !String.IsNullOrEmpty(entity.Currency) ? entity.Currency : country.Currency;
            

            var result = await Task.Run(() => _countryService.Update(entity));
            if(result == null)
            {
                response.Success = false;
                response.Message = "An error occured.";
                response.Data = null;
                return BadRequest(response);
            }

            response.Success = true;
            response.Message = null;
            response.Data = result;
            return Ok(response);

        }


        /// <summary>
        /// post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="400">Returns error</response>

        [HttpPut]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(CountryDto model)
        {
            GenericResponse<Country> response = new GenericResponse<Country>();
            var entity = _mapper.Map<CountryDto, Country>(model);
            var result = await Task.Run(() => _countryService.Add(entity));
            if (result == null)
            {
                response.Success = false;
                response.Message = "An error ocurred.";
                response.Data = null;
                return BadRequest(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = result;

            return Created("", response);


        }



        /// <summary>
        /// delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        /// <response code="404">Returns error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var country = await Task.Run(() => _countryService.Delete(id));
            GenericResponse<String> response = new GenericResponse<String>();
            if (country == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null; ;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = country;
            response.Data = null;
            return Ok(response);
       
        }
    }
}
