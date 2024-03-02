using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Api.Core;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Dto;

namespace Tracker.Module.Budget.Api.Controllers
{
    [Route(Constants.ROUTE_TEMPLATE)]
    [ApiController]
    public class
        RecurringPaymentController : EntityController<RecurringPayment, SearchableRecurringPayment, RecurringPaymentDto>
    {
        /// <inheritdoc />
        public RecurringPaymentController(
            IEntityQueryManager<RecurringPayment, SearchableRecurringPayment, RecurringPaymentDto> manager) :
            base(manager)
        {
        }
    }
}