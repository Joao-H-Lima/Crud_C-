using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetoSimpliss.Models
{
    public class ContribBeneficio
    {
        [Key]
        public int Id { get; set; }

        // Chave estrangeira para Contribuintes
        public int ContribuinteId { get; set; }
        [ForeignKey("ContribuinteId")]
        public Contribuintes Contribuinte { get; set; }

        // Chave estrangeira para Beneficios
        public int BeneficioId { get; set; }
        [ForeignKey("BeneficioId")]
        public Beneficios Beneficio { get; set; }

        public DateTime DataVinculo { get; set; }
    }
}
