using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using restaurant.entity;

namespace restaurant.webui.Identity
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public bool NewOrder { get; set; }
        public bool Waiter {get; set;}
    }
}