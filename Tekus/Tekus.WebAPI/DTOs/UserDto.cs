﻿namespace Tekus.WebAPI.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; } 
        public bool Admin { get; set; }
    }
}
        
