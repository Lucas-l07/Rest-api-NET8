using Microsoft.AspNetCore.Mvc;

namespace Rest_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{operation}/{firstNumber}/{secondNumber}")]
        public IActionResult Get(string operation, string firstNumber, string secondNumber)
        {
            decimal firstNumberConverted = ConvertToDecimal(firstNumber);
            decimal secondNumberConverted = ConvertToDecimal(secondNumber);
            bool bValidationNumber = IsNumeric(secondNumber) && IsNumeric(secondNumber);

            if (!bValidationNumber)
            {
                return BadRequest("Invalid input.");
            }

            string result = operation switch
            {
                "sum"            => Sum(firstNumberConverted, secondNumberConverted).ToString(),
                "subtraction"    => decimal.Subtract(firstNumberConverted, secondNumberConverted).ToString(),
                "multiplication" => Multiplcation(firstNumberConverted, secondNumberConverted).ToString(),
                "division"       => Division(firstNumberConverted, secondNumberConverted).ToString(),
                "average"        => Average(firstNumberConverted, secondNumberConverted).ToString(),
                _                => "Invalid operation."
            };

            return Ok(result);
        }

        private decimal Sum(decimal firstNumber, decimal secondNumber) => firstNumber + secondNumber;
        private decimal Multiplcation(decimal firstNumber, decimal secondNumber) => firstNumber * secondNumber;
        private decimal Division(decimal firstNumber, decimal secondNumber) => firstNumber / secondNumber;
        private decimal Average(decimal firstNumber, decimal secondNumber) => (firstNumber + secondNumber) / 2;

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if (decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }

            return 0;
        }

        private bool IsNumeric(string strNumber)
        {
            bool isNumber = double.TryParse(strNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out double number);

            return isNumber;
        }
    }
}