namespace GymApp.MVC.Entidades
{
    public class Reserva
    {
        public int Id { get; set; }
        public int MiembroId { get; set; }
        public Miembro Miembro { get; set; } = null!;
        public int EquipoId { get; set; }
        public Equipo Equipo { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}

