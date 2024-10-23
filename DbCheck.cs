using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessCard.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BusinessCard
{
    public class DbCheck : IHealthCheck
    {
        private readonly Ctx _ctx;

        public DbCheck(Ctx ctx)
        {
            _ctx = ctx;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            bool canConnect = false;
            Exception? exception = null;
            string? description = null;
            var data = new Dictionary<string, object>();
            try
            {
                canConnect = await _ctx.Database.CanConnectAsync(cancellationToken);
                data["CanConnect"] = true;
                
                await _ctx.Database.OpenConnectionAsync(cancellationToken: cancellationToken);
                data["CanOpenConnection"] = true;
                
                await _ctx.Database.CloseConnectionAsync();
                data["CanCloseConnection"] = true;

                await _ctx.People.ToListAsync(cancellationToken: cancellationToken);
                data["CanRead"] = true;
                
                
                description = canConnect ? "Database exists" : "Database does not exist";
            }
            catch (Exception e)
            {
                exception = e;
                description = $"Database existence check failed: {e.Message}";
            }

            return new HealthCheckResult(canConnect ? HealthStatus.Healthy : HealthStatus.Unhealthy, description,
                exception, data);
        }
    }

    public class SeedCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var status = Seeder.Result.Status == Seeder.Status.Success ? HealthStatus.Healthy : HealthStatus.Unhealthy;

            var description = Seeder.Result.Status switch
            {
                Seeder.Status.Success => "Seed succeeded",
                Seeder.Status.NoRun => "Seed not run",
                _ => "Seed failed: " + Seeder.Result.Exception?.Message
            };

            var result = new HealthCheckResult(status, description, Seeder.Result.Exception);

            return Task.FromResult(result);
        }
    }
}