using ProEventos.Aula.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Aula.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 a 250 caracteres.")]
        public string Local { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 a 250 caracteres.")]
        public string Tema { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Range(1, 100000, ErrorMessage = "{0} deve ter entre 1 a 100.000 pessoas")]
        public int QtdPessoas { get; set; }

        [StringLength(250, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 a 250 caracteres.")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public string Lote { get; set; }

        [StringLength(250, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 a 250 caracteres.")]
        [RegularExpression("^.*(\\.jpeg|\\.jpg|\\.gif|\\.png)$", ErrorMessage = "{0} precisa ser uma imagem com uma extensão válida")]
        [Required(ErrorMessage = "{0} é um campo obrigatório." )]
        public string ImagemURL { get; set; }

        [StringLength(15, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 a 15 caracteres.")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [EmailAddress(ErrorMessage = "{0} o é inválido, por favor coloque um válido")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 a 250 caracteres.")]
        public string Email { get; set; }

        public virtual IEnumerable<LoteDto> Lotes { get; set; }
        public virtual IEnumerable<RedeSocialDto> RedeSociais { get; set; }
        public virtual IEnumerable<PalestranteDto> Palestrantes { get; set; }

    }
}
