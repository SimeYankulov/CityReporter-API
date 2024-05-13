using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityReporter.Models.DTOs.UserDtos
{
    public class ResponseLogin
    {
        public string Jwt { get; set; }
        public string Role { get; set; }
    }
}
