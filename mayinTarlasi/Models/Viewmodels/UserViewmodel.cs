using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mayinTarlasi.Models.Viewmodels
{
    public class UserViewmodel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
    }
}