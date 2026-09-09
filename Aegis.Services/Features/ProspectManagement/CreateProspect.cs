using Aegis.Utility.Common;
using MediatR;

namespace Aegis.Services.Features.ProspectManagement
{
    public static class CreateProspect
    {
        public class CreateProspectCommand : IRequest<ApiResponse<object>>
        {
            
        }
    }
}