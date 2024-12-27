namespace ProjetoSimpliss.Models
{
    public class Contribuintes
    {
        public int Id { get; set; }

        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }

        public DateTime DataAbertura { get; set; }

        public string RegimeTributacao { get; set; }

        // Relacionamento com Benefícios (Chave Estrangeira)
        public int BeneficioId { get; set; }

        public Beneficios? Beneficio { get; set; }
    }
}
