using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iduff.Models
{
    [Table("Evento")]
    public class Evento
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        public string? PalestranteId { get; set; }
        [ForeignKey("PalestranteId")]
        public Usuario? Palestrante { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public long HorasComplementares { get; set; }
        public string? OrganizadorId { get; set; }
        [ForeignKey("OrganizadorId")]
        public Usuario? Organizador { get; set; }
        public string Titulo { get; set; }

        public ICollection<Certificado> Certificados { get; set; } = new List<Certificado>();
    }
}