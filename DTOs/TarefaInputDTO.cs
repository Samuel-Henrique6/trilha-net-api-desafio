using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.DTOs
{
    public class TarefaInputDTO
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public string Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
    }
}