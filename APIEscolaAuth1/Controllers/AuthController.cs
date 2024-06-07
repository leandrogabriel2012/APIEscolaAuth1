using APIEscolaAuth1.Context;
using APIEscolaAuth1.DTOs;
using APIEscolaAuth1.Models;
using APIEscolaAuth1.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIEscolaAuth1.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AuthController(TokenService tokenService, AppDbContext context, IMapper mapper)
    {
        _tokenService = tokenService;
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("Login")]
    public string Login(UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var token = _tokenService.Generate(user);
        _context.Users.Add(user);
        _context.SaveChanges();

        return token;
    }

    [HttpGet]
    [Route("User")]
    [Authorize]
    public ActionResult<string?> Get()
    {
        var user = User?.Identity;
        if (user is null)
        {
            return BadRequest();
        }

        return Ok($"Login: {user.Name}");
    }

    [HttpGet]
    [Route("Free")]
    [AllowAnonymous]
    public ActionResult<string> GetFree()
    {
        return Ok("Acesso livre!");
    }

    [HttpGet]
    [Route("Employee")]
    [Authorize(Policy = "Employee")]
    public ActionResult<string> GetEmployee()
    {
        return Ok("Acesso para empregados comuns!");
    }

    [HttpGet]
    [Route("Admin")]
    [Authorize(Policy = "Admin")]
    public ActionResult<string> GetAdmin()
    {
        return Ok("Acesso para administradores!");
    }
}
