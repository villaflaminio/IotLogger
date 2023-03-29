using Domain;
using Domain.Interfaces;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(SensorController));

        public SensorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Sensor> GetAllPersons()
        {
            _logger.Info("GetAllPersons");
            return unitOfWork.Sensor.GetAll();
        }


        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Sensor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                _logger.Info($@"{ControllerContext.HttpContext.Request.Path}");
                IEnumerable<Sensor> sensori = unitOfWork.Sensor.GetAll();
                return Ok(sensori);
            }
            catch (Exception ex)
            {   
                _logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                _logger.Info($@"{ControllerContext.HttpContext.Request.Path} {JsonConvert.SerializeObject(id)}");
                IEnumerable<Sensor> sensori = unitOfWork.Sensor.GetById(id);
                return Ok(sensori);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            var ruolo = _service.Get(id);
            return Ok(_mapper.return_result<RuoloAPIModel, Ruolo>(ruolo));
        }

        //// POST api/<ValuesController>
        //[HttpPost]
        //[ProducesResponseType(typeof(RuoloAPIModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Post([FromBody] CreateRuoloAPIModel value)
        //{
        //    loggerx.Info($@"{ControllerContext.HttpContext.Request.Path} {JsonConvert.SerializeObject(value)}");
        //    var ruolo = await _service.Create(value);
        //    return Ok(_mapper.return_result<RuoloAPIModel, Ruolo>(ruolo));

        //}

        //// PUT api/<ValuesController>/5
        //[HttpPut]
        //[ProducesResponseType(typeof(RuoloAPIModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Put([FromBody] RuoloAPIModel value)
        //{
        //    loggerx.Info($@"{ControllerContext.HttpContext.Request.Path} {JsonConvert.SerializeObject(value)}");
        //    var ruolo = await _service.Update(value.Id, value);
        //    return Ok(_mapper.return_result<RuoloAPIModel, Ruolo>(ruolo));
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //[ProducesResponseType(typeof(RuoloAPIModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Delete(uint id)
        //{
        //    loggerx.Info($@"{ControllerContext.HttpContext.Request.Path} {JsonConvert.SerializeObject(id)}");
        //    var ruolo = await _service.Delete(id);
        //    return Ok(_mapper.return_result<RuoloAPIModel, Ruolo>(ruolo));
        //}
    }
}
