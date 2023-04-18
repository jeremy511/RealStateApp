﻿using RealState.Core.Application.ViewModels.Properties;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Interfaces.Services
{
    public interface ISaleService : IGenericService<SaveSalesViewModel, MantSalesViewModel, Sales>
    {

    }
}
