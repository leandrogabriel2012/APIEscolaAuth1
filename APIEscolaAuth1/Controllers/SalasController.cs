using APIEscolaAuth1.DTOs;
using APIEscolaAuth1.Models;
using APIEscolaAuth1.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEscolaAuth1.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class SalasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    private readonly ILogger<SalasController> _logger;

    public SalasController(IUnitOfWork uof, IMapper mapper, ILogger<SalasController> logger)
    {
        _uof = uof;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<SalaDTO>>> Get()
    {
        var salas = await _uof.SalaRepository.GetAllAsync();
        if(salas is null)
        {
            return NotFound();
        }

        var salasDto = _mapper.Map<IEnumerable<SalaDTO>>(salas);

        return Ok(salasDto);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<SalaDTO>> Get(int id)
    {
        var sala = await _uof.SalaRepository.GetAsync(id);
        if (sala is null)
        {
            return NotFound();
        }

        var salaDto = _mapper.Map<SalaDTO>(sala);

        return Ok(salaDto);
    }

    [HttpPost]
    [Authorize(Policy = "Employee")]
    public async Task<ActionResult<SalaDTO>> Post(SalaDTO salaDto)
    {
        if (salaDto is null)
        {
            return BadRequest();
        }

        var sala = _mapper.Map<Sala>(salaDto);
        _uof.SalaRepository.Create(sala);
        await _uof.CommitAsync();
        _logger.LogInformation($"Sala de id = {sala.Id} criada!");
        salaDto = _mapper.Map<SalaDTO>(sala);

        return Ok(salaDto);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "Employee")]
    public async Task<ActionResult<SalaDTO>> Put(int id, SalaDTO salaDto)
    {
        if (salaDto.Id != id)
        {
            return BadRequest();
        }

        var sala = _mapper.Map<Sala>(salaDto);
        _uof.SalaRepository.Update(sala);
        await _uof.CommitAsync();
        _logger.LogInformation($"Sala de id = {sala.Id} alterada!");

        return Ok(salaDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<SalaDTO>> Delete(int id)
    {
        var sala = await _uof.SalaRepository.GetAsync(id);
        if (sala is null)
        {
            return NotFound();
        }

        _uof.SalaRepository.Delete(sala);
        await _uof.CommitAsync();
        _logger.LogInformation($"Sala de id = {sala.Id} excluída!");
        var salaDto = _mapper.Map<SalaDTO>(sala);

        return Ok(salaDto);
    }
}
