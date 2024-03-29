﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracker.Shared.Abstraction.Interfaces.Budget.Entity;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Api.Core;
using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;

namespace Tracker.Module.Budget.Api.Controllers
{
    [Route(Constants.ROUTE_TEMPLATE)]
    [ApiController]
    public class PaymentController : EntityController<Payment, SearchablePayment, PaymentDto>
    {
        /// <inheritdoc />
        public PaymentController(IEntityQueryManager<Payment, SearchablePayment, PaymentDto> manager) : base(manager)
        {
        }
    }
}