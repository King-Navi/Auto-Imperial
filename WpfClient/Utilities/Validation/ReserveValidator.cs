using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.MVVM.Model;

namespace WpfClient.Utilities.Validation
{
    public class ReserveValidator : AbstractValidator<Reserve>
    {
        public ReserveValidator()
        {
            RuleFor(r => r.IdClient)
                .GreaterThan(0).WithMessage("Debe seleccionar un cliente válido.");

            RuleFor(r => r.IdSeller)
                .GreaterThan(0).WithMessage("Debe seleccionar un vendedor válido.");

            RuleFor(r => r.IdVersion)
                .GreaterThan(0).WithMessage("Debe seleccionar una versión válida del vehículo.");

            RuleFor(r => r.DownPayment)
                .GreaterThanOrEqualTo(0).WithMessage("El enganche no puede ser negativo.");
        }
    }
}
