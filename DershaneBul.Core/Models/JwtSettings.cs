using System;
using System.Collections.Generic;
using System.Text;

namespace DershaneBul.Core.Models
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public TimeSpan TokenLifetime { get; set; }
    }
}
