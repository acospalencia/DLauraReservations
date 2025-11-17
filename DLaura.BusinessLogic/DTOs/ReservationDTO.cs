using DLaura.Entities;
using System.ComponentModel.DataAnnotations;

namespace DLaura.BusinessLogic.DTOs
{
    public class CreateReservationRequest
    {
        public int UserId { get; set; }

        public int TableNumber { get; set; }

        public DateOnly ReservationDate { get; set; }

        public string ReservateShift { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

    }
    public class UpdateReservationRequest   
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TableNumber { get; set; }

        public DateOnly ReservationDate { get; set; }

        public string ReservateShift { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

    }
    public class ReservationResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TableNumber { get; set; }

        public DateOnly ReservationDate { get; set; }

        public string ReservateShift { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public virtual UserTableResponse TableNumberNavigation { get; set; } = null!;

        public virtual UserResponse User { get; set; } = null!;
    }

    public class ReservationByFilterRequest
    {
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateOnly ReservationDate { get; set; }

        [Required(ErrorMessage = "El turno es obligatorio.")]
        public string ReservateShift { get; set; } = null!;

    }
    public class DateFilterRequest
    {
        public DateOnly ReservationDate { get; set; }

    }
    public class ReservationsByIdResquest
    {
        public int Id { get; set; }
    }


}
