﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Api;
using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;

namespace Tracker.Module.Budget.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentTypeController : EntityController<PaymentType, SearchablePaymentType, PaymentTypeDto>
    {
        /// <inheritdoc />
        public PaymentTypeController(
            IEntityQueryManager<PaymentType, SearchablePaymentType, PaymentTypeDto> manager) : base(manager)
        {
        }
    }
}