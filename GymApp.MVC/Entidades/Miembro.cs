namespace GymApp.MVC.Entidades
{
    public class Miembro
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Membresia? MembresiaActual { get; set; }
        public List<Clase> ClasesInscritas { get; set; } = new List<Clase>();

        public void Registrar()
        {
            // Aquí iría la lógica para registrar un nuevo miembro en la base de datos
        }

        public void RenovarMembresia(string tipoMembresia)
        {
            MembresiaActual = new Membresia
            {
                Tipo = tipoMembresia,
                FechaInicio = DateTime.Now,
                FechaFin = tipoMembresia == "Mensual" ? DateTime.Now.AddMonths(1) :
                           tipoMembresia == "Trimestral" ? DateTime.Now.AddMonths(3) :
                           DateTime.Now.AddYears(1),
                MiembroId = this.Id,
                Miembro = this
            };
        }
    }
}
