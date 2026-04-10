using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Models
{
    public class RefreshToken
    {
        public int RftId { get; set; }
        public string Token { get; set; }

        public DateTime Created { get; set; }
        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }
        public DateTime? RevokedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
