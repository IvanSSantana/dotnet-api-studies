using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Validators;
using API.Utils;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MathController : ControllerBase
{
    private readonly MathService _mathService;

    public MathController(MathService mathService)
    {
        _mathService = mathService;
    }

    [HttpGet("sum/{firstNumberStr}/{secondNumberStr}")]
    
    public IActionResult Sum(string firstNumberStr, string secondNumberStr)
    {
        var validation = NumberValidator.IsNumeric([firstNumberStr, secondNumberStr]);
        if (!string.IsNullOrEmpty(validation)) return BadRequest(validation);

        double result = _mathService.Sum(NumberHelper.ConvertToDouble(firstNumberStr), NumberHelper.ConvertToDouble(secondNumberStr));
        return Ok(result);
    }

    [HttpGet("subtraction/{firstNumberStr}/{secondNumberStr}")]
    
    public IActionResult Subtraction(string firstNumberStr, string secondNumberStr)
    {
        var validation = NumberValidator.IsNumeric([firstNumberStr, secondNumberStr]);
        if (!string.IsNullOrEmpty(validation)) return BadRequest(validation);

        double result = _mathService.Subtraction(NumberHelper.ConvertToDouble(firstNumberStr), NumberHelper.ConvertToDouble(secondNumberStr));
        return Ok(result);
    }

    [HttpGet("multiplication/{firstNumberStr}/{secondNumberStr}")]
    
    public IActionResult Multiplication(string firstNumberStr, string secondNumberStr)
    {
        var validation = NumberValidator.IsNumeric([firstNumberStr, secondNumberStr]);
        if (!string.IsNullOrEmpty(validation)) return BadRequest(validation);

        double result = _mathService.Multiplication(NumberHelper.ConvertToDouble(firstNumberStr), NumberHelper.ConvertToDouble(secondNumberStr));
        return Ok(result);
    }

    [HttpGet("division/{firstNumberStr}/{secondNumberStr}")]
    
    public IActionResult Division(string firstNumberStr, string secondNumberStr)
    {
        var validation = NumberValidator.IsNumeric([firstNumberStr, secondNumberStr]);
        if (!string.IsNullOrEmpty(validation)) return BadRequest(validation);

        double result = _mathService.Division(NumberHelper.ConvertToDouble(firstNumberStr), NumberHelper.ConvertToDouble(secondNumberStr));
        return Ok(result);
    }

    [HttpGet("average/{firstNumberStr}/{secondNumberStr}")]
    
    public IActionResult Average(string firstNumberStr, string secondNumberStr)
    {
        var validation = NumberValidator.IsNumeric([firstNumberStr, secondNumberStr]);
        if (!string.IsNullOrEmpty(validation)) return BadRequest(validation);

        double result = _mathService.Average(NumberHelper.ConvertToDouble(firstNumberStr), NumberHelper.ConvertToDouble(secondNumberStr));
        return Ok(result);
    }

    [HttpGet("square-root/{number}")]
    
    public IActionResult SquareRoot(string number)
    {
        var validation = NumberValidator.IsNumeric([number]);
        if (!string.IsNullOrEmpty(validation)) return BadRequest(validation);

        double result = _mathService.SquareRoot(NumberHelper.ConvertToDouble(number));
        return Ok(result);
    }
}
