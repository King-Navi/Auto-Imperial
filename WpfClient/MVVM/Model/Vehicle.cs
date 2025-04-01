﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.MVVM.Model
{
    class Vehicle
    {
        public int VehicleId { get; set; }
        public string? Branch { get; set; }
        public string? Model { get; set; }         
        public string? Version { get; set; } 

        public string? VehicleType { get; set; }          

        public string? VehicleStatus { get; set; }         

        public decimal? SupplierPrice { get; set; }        

        public decimal? SellPrice { get; set; }            

        public int? Year { get; set; }                     

        public string? Color { get; set; }

        public string? VIN { get; set; } 

        public string? ChassisNumber { get; set; } 
        public string? EngineNumber { get; set; } 

        public int SupplierPurchaseName { get; set; }

        public string? Transmission { get; set; } 
        public string? Doors { get; set; }           
        public string? Engine { get; set; } 

        public List<string> Photos { get; set; } = new();   

        public List<string> Discounts { get; set; } = new(); 
    }
}
