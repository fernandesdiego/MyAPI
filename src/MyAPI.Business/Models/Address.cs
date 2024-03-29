﻿using System;

namespace MyAPI.Business.Models
{
    public class Address : Entity
    {
        public Guid SupplierId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string ZIP { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Supplier Supplier { get; set; }

    }
}