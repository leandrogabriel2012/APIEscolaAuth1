using APIEscolaAuth1.DTOs;
using APIEscolaAuth1.Models;
using APIEscolaAuth1.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace APIEscolaAuth1.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class TurmasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    private readonly ILogger<AlunosController> _logger;

    public TurmasController(IUnitOfWork uof, IMapper mapper, ILogger<AlunosController> logger)
    {
        _uof = uof;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<TurmaDTO>>> Get()
    {
        var turmas = await _uof.TurmaRepository.GetAllAsync();
        if (turmas is null)
        {
            return NotFound();
        }

        var turmasDto = _mapper.Map<IEnumerable<TurmaDTO>>(turmas);

        return Ok(turmasDto);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<TurmaDTO>> Get(int id)
    {
        var turma = await _uof.TurmaRepository.GetAsync(id);
        if (turma is null)
        {
            return NotFound();
        }

        var turmaDto = _mapper.Map<TurmaDTO>(turma);

        return Ok(turmaDto);
    }

    [HttpPost]
    [Authorize(Policy = "Employee")]
    public async Task<ActionResult<TurmaDTO>> Post(TurmaDTO turmaDto)
    {
        if (turmaDto is null)
        {
            return BadRequest();
        }

        var turma = _mapper.Map<Turma>(turmaDto);
        _uof.TurmaRepository.Create(turma);
        await _uof.CommitAsync();
        _logger.LogInformation($"Turma de id = {turma.Id} criada!");
        turmaDto = _mapper.Map<TurmaDTO>(turma);

        return Ok(turmaDto);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "Employee")]
    public async Task<ActionResult<TurmaDTO>> Put(int id, TurmaDTO turmaDto)
    {
        if (turmaDto.Id != id)
        {
            return BadRequest();
        }

        var turma = _mapper.Map<Turma>(turmaDto);
        _uof.TurmaRepository.Update(turma);
        await _uof.CommitAsync();
        _logger.LogInformation($"Turma de id = {turma.Id} alterada!");

        return Ok(turmaDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<TurmaDTO>> Delete(int id)
    {
        var turma = await _uof.TurmaRepository.GetAsync(id);
        if (turma is null)
        {
            return BadRequest();
        }

        _uof.TurmaRepository.Delete(turma);
        await _uof.CommitAsync();
        _logger.LogInformation($"Turma de id = {turma.Id} excluída!");
        var turmaDto = _mapper.Map<TurmaDTO>(turma);

        return Ok(turmaDto);
    }

    [HttpGet("Sala/{id:int}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<TurmaDTO>>> GetTurmasSala(int id)
    {
        var turmas = await _uof.TurmaRepository.GetTurmasSalaAsync(id);
        if(turmas is null)
        {
            return BadRequest();
        }

        var turmasDto = _mapper.Map<IEnumerable<TurmaDTO>>(turmas);

        return Ok(turmasDto);
    }

    [HttpGet("Nome/{id:int}")]
    [Authorize]
    public async Task<ActionResult<string?>> GetNomeTurma(int id)
    {
        var turma = await _uof.TurmaRepository.GetAsync(id);
        if(turma is null)
        {
            return NotFound();
        }

        return turma.Nome;
    }

    [HttpGet("Nomes")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<string>?>> GetNomesTurmas()
    {
        var turmas = await _uof.TurmaRepository.GetAllAsync();
        if(turmas is null)
        {
            return BadRequest();
        }

        List<string?> listaNomes = new List<string?>();
        foreach (var turma in turmas)
        {
            listaNomes.Add(turma.Nome);
        }

        return Ok(listaNomes);
    }
}
