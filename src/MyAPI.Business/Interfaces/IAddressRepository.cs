﻿using System;
using System.Threading.Tasks;
using MyAPI.Business.Interfaces;
using MyAPI.Business.Models;

namespace MyAPI.Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressBySupplier(Guid supplierId);
    }
}