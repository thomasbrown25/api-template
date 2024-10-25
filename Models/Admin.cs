using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace personal_trainer_api.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public int? UserId { get; set; } = 0;
        public int SettingId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public DateTime LastVisited { get; set; } = DateTime.Now;
    }
}