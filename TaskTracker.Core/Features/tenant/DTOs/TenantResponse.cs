using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Entities;

namespace TaskTracker.Core.Features.tenant.DTOs
{
    public class TenantResponse
    {

       // public int Id { get; set; }

        public string Name { get; set; }

       // public string Identifier { get; set; } // مثل كود tenant في الـJWT أو subdomain

    
    }
}
