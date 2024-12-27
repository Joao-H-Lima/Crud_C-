namespace ProjetoSimpliss.ViewModels
{
    public class ContribuinteCreateViewModel
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public string RegimeTributacao { get; set; }
        public int BeneficioId { get; set; }                   

        public List<BeneficioCheckboxViewModel>? Beneficios { get; set; }

        public List<int> SelectedBeneficios { get; set; } 
       
    }
    public class BeneficioCheckboxViewModel
    {
        public int Id { get; set; }
        public string NomeBeneficio { get; set; }
        public bool IsSelected { get; set; }
    }
}
