using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace SWProvincias_Martinez.Models
{
    [Table("Provincia")]
    public class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Nombre { get; set; }

        public List<Ciudad> Ciudades { get; set; }
    }
}
