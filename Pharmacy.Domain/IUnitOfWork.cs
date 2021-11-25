﻿using Pharmacy.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Pharmacy.Domain
{
    public interface IUnitOfWork: IDisposable
    {
        IMedicineRepository MedicineRepository { get; }
        IStoresRepository StoresRepository { get; }

        Task SaveAsync();
    }
}
