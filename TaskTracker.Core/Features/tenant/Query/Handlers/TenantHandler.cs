using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Bases;
using TaskTracker.Core.Features.tenant.DTOs;
using TaskTracker.Core.Features.tenant.Query.Models;
using TaskTracker.Core.Features.Users.DTOs;
using TaskTracker.Core.Features.Users.Query.Models;
using TaskTracker.Services.abstracts;

namespace TaskTracker.Core.Features.tenant.Query.Handlers
{
    public class TenantHandler : IRequestHandler<GetTenantListQuery, Response<List<TenantResponse>>>
    {
        private readonly ITenantServices _tenantServices;
        private readonly IMapper _mappper;
        private readonly ResponseHandler _responseHandler;

        public TenantHandler(ITenantServices Services, IMapper mapper, ResponseHandler responseHandler)
        {
            _tenantServices = Services;
            _mappper = mapper;
            _responseHandler = responseHandler;

        }


        public async Task<Response<List<TenantResponse>>> Handle(GetTenantListQuery request, CancellationToken cancellationToken)
        {
            var users = await _tenantServices.GetTenantListAsync(request.TenantId);

            var response = _mappper.Map<List<TenantResponse>>(users);
            return _responseHandler.Success(response);


        }
    }
}
