﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    public class Account
    {
        public Guid? Guid { get; set; }
        public string Password { get; set; }

        public int? OTP { get; set; }

        public bool IsUsed { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? ExpiredTime { get; set; }

    }
}
