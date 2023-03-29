using Domain;
using Domain.Interfaces;
using Domain.Model;
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
        [ProducesResponseType(typeof(IQueryable<Sensor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                _logger.Info("GET" + $@"{ControllerContext.HttpContext.Request.Path}");
                IEnumerable<Sensor> sensori = unitOfWork.Sensor.GetAll();
                return Ok(sensori);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IQueryable<Sensor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get(long id)
        {
            try
            {
                _logger.Info("GET" + $@"{ControllerContext.HttpContext.Request.Path} {JsonConvert.SerializeObject(id)}");
                Sensor sensore = unitOfWork.Sensor.GetById(id);
                return Ok(sensore);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Sensor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] SensorDto value)
        {
            try
            {
                _logger.Info("POST" + $@"{ControllerContext.HttpContext.Request.Path} {JsonConvert.SerializeObject(value)}");
                Sensor newSensor = new Sensor(value.SensorId);
                unitOfWork.Sensor.Add(newSensor);
                unitOfWork.Save();

                return Ok(newSensor);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [ProducesResponseType(typeof(Sensor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] SensorDto value)
        {
            try
            {
                _logger.Info("PUT" + $@"{ControllerContext.HttpContext.Request.Path} {JsonConvert.SerializeObject(value)}");

                var sensore = await unitOfWork.Sensor.Update(value.Id, value);
                return Ok(sensore);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Sensor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.Info("DELETE" + $@"{ControllerContext.HttpContext.Request.Path} {JsonConvert.SerializeObject(id)}");
            Sensor sensor = await unitOfWork.Sensor.Delete(id);
            return Ok(sensor);
        }
    }
}
