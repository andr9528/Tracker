using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Api.Core;
using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;

namespace Tracker.Module.Budget.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class
        PaymentTemplateController : EntityController<PaymentTemplate, SearchablePaymentTemplate, PaymentTemplateDto>
    {
        /// <inheritdoc />
        public PaymentTemplateController(
            IEntityQueryManager<PaymentTemplate, SearchablePaymentTemplate, PaymentTemplateDto> manager) : base(manager)
        {
        }
    }
}