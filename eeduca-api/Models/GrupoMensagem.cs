using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eeduca_api.Models
{
    [Table("GruposMensagens")]
    public class GrupoMensagem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }        
        public int GrupoId { get; set; }
        [Required]
        [ForeignKey("GrupoId")]
        public Grupo Grupo { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DataHoraCriacao { get; set; }
        [Required]
        public string Mensagem { get; set; }
    }
}