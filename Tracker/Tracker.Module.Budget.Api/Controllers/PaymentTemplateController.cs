using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Api;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;

namespace Tracker.Module.Budget.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentTemplateController : EntityController<PaymentTemplate, SearchablePaymentTemplate>
    {
        /// <inheritdoc />
        public PaymentTemplateController(
            IEntityQueryManager<PaymentTemplate, SearchablePaymentTemplate> manager) : base(manager)
        {
        }
    }
}