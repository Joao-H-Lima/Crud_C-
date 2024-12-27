using System.ComponentModel.DataAnnotations;

namespace ProjetoSimpliss.Models
{
    public class Beneficios
    {
        public int Id { get; set; }
        public string? NomeBeneficio { get; set; }
        public double PorcentagemDesconto { get; set; }  
    }
}
