﻿using System;
using Lakehouse.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Lakehouse.Services
{
    public class SessionService
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public void SetUser(User user, ISession session)
        {
            session.SetString("user", JsonConvert.SerializeObject(user));
        }

        public User GetUser(ISession session)
        {
            string json = session.GetString("user");
            try
            {

                User user = JsonConvert.DeserializeObject<User>(json);
                return user;
            }
            catch (JsonException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void Clear(ISession session)
        {
            session.SetString("user", "");
            session.Clear();
        }
    }
}
