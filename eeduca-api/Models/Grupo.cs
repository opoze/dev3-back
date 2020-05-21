using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eeduca_api.Models
{
    [Table("Grupos")]
    public class Grupo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Administrador { get; set; }
        [Required]
        [MaxLength(60)]
        public string Nome { get; set; }
        [MaxLength(500)]
        public string Descricao { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DataHoraCriacao { get; set; }
        [MaxLength(6)]
        public string Chave { get; set; }

        [InverseProperty("Grupo")]
        public virtual List<GrupoMensagem> Mensagens { get; set; }

        public void GerarChaveIngresso()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rng = new Random(Id);
            char[] temp = new char[6];

            for (int i = 0; i < temp.Length; i++)
                temp[i] = chars[rng.Next(chars.Length)];

            this.Chave = new String(temp);
        }
    }
}