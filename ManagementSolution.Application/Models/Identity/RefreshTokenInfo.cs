﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSolution.Application.Models.Identity
{
    public class RefreshTokenInfo
    {
        public string Id { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }
    }
}
