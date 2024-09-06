namespace TemplateDotNet.Domain.Entities
{
    public class Usuario : Entity<Guid>
    {
        public string Nome { get; private set; }

        public Usuario(string nome) : base(Guid.NewGuid())
        {
            Nome = nome;
        }
    }
}
