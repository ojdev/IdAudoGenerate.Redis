using IdAutoGenerate.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IdAutoGenerateFactory _idAutoGenerate;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IdAutoGenerateFactory idAutoGenerate, ILogger<WeatherForecastController> logger)
        {
            _idAutoGenerate = idAutoGenerate ?? throw new ArgumentNullException(nameof(idAutoGenerate));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var code = await _idAutoGenerate.GetCode(5, '0', "sh");
            var id = await _idAutoGenerate.GetIncrementAsync();
            return new string[] {
                code,
                id.ToString()
            };
        }
    }
}
