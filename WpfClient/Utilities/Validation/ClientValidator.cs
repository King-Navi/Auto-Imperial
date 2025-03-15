using AutoImperialDAO.Models;
using FluentValidation;
using WpfClient.MVVM.Model;

namespace WpfClient.Utilities.Validation
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {                
                RuleFor(client => client.Name)
                    .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                    .Matches(@"^[a-zA-Z\s]+$").WithMessage("El nombre no puede contener números ni caracteres especiales.");

                RuleFor(client => client.PaternalSurname)
                      .NotEmpty().WithMessage("El apellido paterno no puede estar vacío.")
                      .Matches(@"^[\p{L}ñÑüÜ\s]+$").WithMessage("El apellido paterno solo puede contener letras y espacios.");

                RuleFor(client => client.MaternalSurname)
                    .NotEmpty().WithMessage("El apellido materno no puede estar vacío.")
                    .Matches(@"^[\p{L}ñÑüÜ\s]+$").WithMessage("El apellido materno solo puede contener letras y espacios.");

                RuleFor(client => client.Phone)
                    .MaximumLength(15).WithMessage("El numero telefonico no puede exceder los 15 digitos");

                RuleFor(client => client.Street)
                    .NotEmpty().WithMessage("La calle no puede estar vacía.")
                    .Matches(@"^[\p{L}0-9\s\.,#\-]+$").WithMessage("El nombre de la calle solo puede contener letras, números, espacios y los caracteres ., # y -.");

                RuleFor(client => client.CP)
                    .NotEmpty().WithMessage("El código postal no puede estar vacío.")
                    .Matches(@"^\d{5}$").WithMessage("El código postal debe tener 5 dígitos.");

                RuleFor(client => client.City)
                    .NotEmpty().WithMessage("La ciudad no puede estar vacía.");

                RuleFor(client => client.Email)
                    .NotEmpty().WithMessage("El correo electrónico no puede estar vacío.")
                    .EmailAddress().WithMessage("El correo electrónico no es válido.");
                /*
                   3 o 4 letras (empresa o persona física).
                   6 dígitos (fecha de nacimiento o constitución).
                   3 caracteres finales (homoclave asignada por el SAT).
                 */
                RuleFor(client => client.RFC)
                    .NotEmpty().WithMessage("El RFC no puede estar vacío.")
                    .Matches(@"^[A-ZÑ&]{3,4}\d{6}[A-Z\d]{3}$").WithMessage("El RFC no tiene un formato válido.")
                    .Length(12, 13).WithMessage("El RFC debe tener entre 12 y 13 caracteres.");


                /*
                   4 letras del nombre.
                   6 dígitos de la fecha de nacimiento.
                   H o M según el género.
                   5 caracteres según el estado y nombre.
                   2 caracteres asignados por el gobierno.
                 */
                RuleFor(client => client.CURP)
                    .NotEmpty().WithMessage("El CURP no puede estar vacío.")
                    .Matches(@"^[A-Z]{4}\d{6}[HM][A-Z]{5}[A-Z\d]{2}$").WithMessage("El CURP no tiene un formato válido.")
                    .Length(18).WithMessage("El CURP debe tener exactamente 18 caracteres.");
        }
    }
}
