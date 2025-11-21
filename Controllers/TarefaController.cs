using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.DTOs;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefa = _context.Tarefas.Where(tarefa => tarefa.Titulo.Contains(titulo));
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(string data)
        {
            DateTime dataConvertida;

            if (!DateTime.TryParse(data, out dataConvertida))
            {
                return BadRequest(new { Erro = "Data inválida" });
            }

            if (dataConvertida == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }

            var tarefa = _context.Tarefas.Where(tarefa => tarefa.Data.Date == dataConvertida.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            if(!Enum.IsDefined(typeof(EnumStatusTarefa), status))
            {
                return BadRequest(new { Erro = "Status inválido" });
            }
            var tarefa = _context.Tarefas.Where(tarefa => tarefa.Status == status);
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Criar(TarefaInputDTO dto)
        {
            DateTime dataConvertida;

            if (!DateTime.TryParse(dto.Data, out dataConvertida))
            {
                return BadRequest(new { Erro = "Data inválida" });
            }

            if (dataConvertida == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }
            var tarefa = new Tarefa
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Data = dataConvertida,
                Status = dto.Status,
            };
                
            try
            {
                _context.Add(tarefa);
                _context.SaveChanges();

                return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Erro = "Falha de integridade ao salvar as alterações." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Erro = "Ocorreu um erro interno inesperado ao atualizar a tarefa." });
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Atualizar(int id, TarefaInputDTO dto)
        {
            DateTime dataConvertida;

            if(!DateTime.TryParse(dto.Data, out dataConvertida))
            {
                return BadRequest(new { Erro = "Data inválida" });
            }

            if(dataConvertida == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }

            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null) return NotFound();

            try
            {
                tarefaBanco.Titulo = dto.Titulo;
                tarefaBanco.Descricao = dto.Descricao;
                tarefaBanco.Data = dataConvertida;
                tarefaBanco.Status = dto.Status;

                _context.Tarefas.Update(tarefaBanco);
                _context.SaveChanges();
                
                return Ok(tarefaBanco);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Erro = "Falha de integridade ao salvar as alterações." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Erro = "Ocorreu um erro interno inesperado ao atualizar a tarefa." });
            }            
        }

        [HttpPatch("{id:int}/toggle-status")] // Implementei o PATCH para alterar o status da tarefa
        public IActionResult AlterarStatus(int id)
        {       
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            try
            {
                if(tarefaBanco.Status == EnumStatusTarefa.Finalizado)
                {
                    tarefaBanco.Status = EnumStatusTarefa.Pendente;
                }
                else
                {
                    tarefaBanco.Status = EnumStatusTarefa.Finalizado;
                }

                _context.Tarefas.Update(tarefaBanco);
                _context.SaveChanges();

                return Ok(tarefaBanco);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Erro = "Falha de integridade ao salvar as alterações." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Erro = "Ocorreu um erro interno inesperado ao atualizar a tarefa." }); 
            }
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            try
            {   
                _context.Tarefas.Remove(tarefaBanco);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { Erro = "Erro interno ao tentar remover a tarefa." });
            }

        }
    }
}
