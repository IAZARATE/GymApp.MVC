namespace GymApp.MVC.Entidades
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();
    
    public void Reservar(Miembro miembro, DateTime fechaInicio, DateTime fechaFin)
        {
            if (Estado == "Disponible")
            {
                var reserva = new Reserva
                {
                    Miembro = miembro,
                    Equipo = this,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin
                };
                Reservas.Add(reserva);
                Estado = "Reservado";
            }
        }
    }
}
