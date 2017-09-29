using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Company.IdentityServer.Example.Configuration
{
    public class TempUser : TestUser
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }

    internal class Users
    {
        public static List<TempUser> Get()
        {
            return new List<TempUser>
            {
                new TempUser
                {
                    SubjectId = "1",  //contactId
                    Username = "user1",
                    Email = "user1@company.com",
                    Password = "Password123!" ,
                    Roles = new List<string> {"CustomerSupport"}
                    //Claims = new List<Claim>
                    //{
                    //    new Claim(JwtClaimTypes.Email, "user1@company.com"),
                    //    new Claim(JwtClaimTypes.Role, "CustomerSupport")
                    //}
                },
                new TempUser
                {
                    SubjectId = "2",  //ContactId
                    Username = "user2",
                    Email = "user2@company.com",
                    Password = "Password123!",
                    Roles = new List<string> {"Builder"}
                    //Claims = new List<Claim>
                    //{
                    //    new Claim(JwtClaimTypes.Email, "user2@company.com"),
                    //    new Claim(JwtClaimTypes.Role, "Builder")
                    //}
                }
            };


        }
    }
}