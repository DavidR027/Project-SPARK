﻿namespace API.ViewModels.Others
{
    public class ClaimVM
    {
        public string NameIdentifier { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid Guid { get; set; }
        public List<string> Roles { get; set; }
    }
}
