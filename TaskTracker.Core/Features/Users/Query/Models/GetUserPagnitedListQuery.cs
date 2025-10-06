using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Features.Users.DTOs;
using TaskTracker.Core.Wrappers;
using TaskTracker.Data.Helpers;

namespace TaskTracker.Core.Features.Users.Query.Models
{
    public class GetUserPagnitedListQuery : IRequest<PaginatedResult<GetUserListPagnitedResponse>>
    {
        public GetUserPagnitedListQuery(string tenantId)
        {
            TenantId = tenantId;
                
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public UserOrderingEnum  OrderBy { get; set; }
        public string? Search { get; set; }
        public string TenantId { get; set; }  // يتم ملؤه من Controller


    }
}
