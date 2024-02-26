using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Api;
using Tracker.Shared.Models.Modules.Budget;

namespace Tracker.Module.Budget.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecurringPaymentController : EntityController<RecurringPayment, SearchableRecurringPayment>
    {
        /// <inheritdoc />
        public RecurringPaymentController(
            IEntityQueryManager<RecurringPayment, SearchableRecurringPayment> manager) : base(manager)
        {
        }
    }
}