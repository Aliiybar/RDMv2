using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sign.Api.HealthChecks
{
    public static class DatabaseCheck
    {
        public static HealthCheckResult Check()
        {
            if(true)
            {
                return HealthCheckResult.Healthy("Health check OK");
            }
            else
            {
                return HealthCheckResult.Unhealthy("Health check Fails");
            }
            
        }
    }
}
