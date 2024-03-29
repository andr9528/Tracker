﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Api.Core;
using Tracker.Shared.Models.Modules.Budget.Dto;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.Modules.Budget.Searchable;

namespace Tracker.Module.Budget.Api.Controllers
{
    [Route(Constants.ROUTE_TEMPLATE)]
    [ApiController]
    public class CurrencyRateController : EntityController<CurrencyRate, SearchableCurrencyRate, CurrencyRateDto>
    {
        /// <inheritdoc />
        public CurrencyRateController(
            IEntityQueryManager<CurrencyRate, SearchableCurrencyRate, CurrencyRateDto> manager) : base(manager)
        {
        }
    }
}