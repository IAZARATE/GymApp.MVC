namespace GymApp.MVC.Entidades
{
    public class Clase
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime Horario { get; set; }
        public int EntrenadorId { get; set; }
        public Entrenador Entrenador { get; set; } = null!;
        public List<Miembro> MiembrosInscritos { get; set; } = new List<Miembro>();
    
    public void InscribirMiembro(Miembro miembro)
        {
            if (!MiembrosInscritos.Contains(miembro))
            {
                MiembrosInscritos.Add(miembro);
            }
        }
    }
}
