﻿using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Models
{
    public class CityModel
    {
        public int CityID { get; set; }

        public int StateID { get; set; }

        public int CountryID { get; set; }

        public String CityName { get; set; }
        public String CityCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt {get;set;}
    }
}
