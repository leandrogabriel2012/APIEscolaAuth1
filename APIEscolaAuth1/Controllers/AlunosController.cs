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
public class AlunosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    private readonly ILogger<AlunosController> _logger;

    public AlunosController(IUnitOfWork uof, IMapper mapper, ILogger<AlunosController> logger)
    {
        _uof = uof;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get()
    {
        var alunos = await _uof.AlunoRepository.GetAllAsync();
        if (alunos is null)
        {
            return NotFound();
        }

        var alunosDto = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);

        return Ok(alunosDto);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<AlunoDTO>> Get(int id)
    {
        var aluno = await _uof.AlunoRepository.GetAsync(id);
        if (aluno is null)
        {
            return NotFound();
        }

        var alunoDto = _mapper.Map<AlunoDTO>(aluno);

        return Ok(alunoDto);
    }

    [HttpPost]
    [Authorize(Policy = "Employee")]
    public async Task<ActionResult<AlunoDTO>> Post(AlunoDTO alunoDto)
    {
        if (alunoDto is null)
        {
            return BadRequest();
        }

        var aluno = _mapper.Map<Aluno>(alunoDto);
        _uof.AlunoRepository.Create(aluno);
        await _uof.CommitAsync();
        _logger.LogInformation($"Aluno de id = {aluno.Id} criado!");
        alunoDto = _mapper.Map<AlunoDTO>(aluno);

        return Ok(alunoDto);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "Employee")]
    public async Task<ActionResult<AlunoDTO>> Put(int id, AlunoDTO alunoDto)
    {
        if (alunoDto.Id != id)
        {
            return BadRequest();
        }

        var aluno = _mapper.Map<Aluno>(alunoDto);
        _uof.AlunoRepository.Update(aluno);
        await _uof.CommitAsync();
        _logger.LogInformation($"Aluno de id = {aluno.Id} alterado!");

        return Ok(alunoDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<AlunoDTO>> Delete(int id)
    {
        var aluno = await _uof.AlunoRepository.GetAsync(id);
        if (aluno is null)
        {
            return NotFound();
        }

        _uof.AlunoRepository.Delete(aluno);
        await _uof.CommitAsync();
        _logger.LogInformation($"Aluno de id = {aluno.Id} excluído!");
        var salaDto = _mapper.Map<AlunoDTO>(aluno);

        return Ok(salaDto);
    }

    [HttpGet("Turma/{id:int}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAlunosTurma(int id)
    {
        var alunos = await _uof.AlunoRepository.GetAlunosTurmaAsync(id);
        if(alunos is null)
        {
            return NotFound();
        }

        var alunosDto = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);

        return Ok(alunosDto);
    }
}
