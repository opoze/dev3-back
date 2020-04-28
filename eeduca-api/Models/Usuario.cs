using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eeduca_api.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(120)]        
        public string Email { get; set; }        
        [Required]
        public byte[] Senha { get; set; }
        [InverseProperty("Administrador")]
        public List<Grupo> Grupos { get; set; }
    }
}