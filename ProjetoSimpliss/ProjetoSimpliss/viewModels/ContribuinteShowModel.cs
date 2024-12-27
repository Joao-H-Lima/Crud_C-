using ProjetoSimpliss.Models;

namespace ProjetoSimpliss.viewModels
{
    public class ContribuinteShowModel
    {
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public string RegimeTributacao { get; set; }
        public string nomeBeneficio { get; set; }
        public int IdBeneficio { get; set; }
        public double PorcentagemDesconto { get; set; }

        public List<BeneficioCheckboxViewModel>? Beneficios { get; set; }

        public List<int> SelectedBeneficios { get; set; }
    }
    //public class BeneficioCheckboxViewModel
    //{
    //    public int Id { get; set; }
    //    public string NomeBeneficio { get; set; }
    //    public bool IsSelected { get; set; }
    //}
}
