﻿namespace API.ViewModels.Account
{
    public class AccountVM
    {
        public Guid? Guid { get; set; }
        public string Password { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
