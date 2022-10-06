using Domain.Tenancy.Interfaces;
using System.Diagnostics;

namespace Domain.Infrastructure.Tenancy
{
    public sealed class TenantContextOverride<TTenantIdentity> : IDisposable
    {
        private static readonly AsyncLocal<TenantContextOverride<TTenantIdentity>[]> CurrentLocal = new AsyncLocal<TenantContextOverride<TTenantIdentity>[]>();
        private volatile bool isDisposed;

        private TenantContextOverride(ITenant<TTenantIdentity> tenant)
        {
            Tenant = tenant;
        }

        public static TenantContextOverride<TTenantIdentity> Current => CurrentLocal.Value?.LastOrDefault();

        public ITenant<TTenantIdentity> Tenant { get; }

        public static TenantContextOverride<TTenantIdentity> Push(ITenant<TTenantIdentity> tenant)
        {
            var newTenants = CurrentLocal.Value;
            var tenantContext = new TenantContextOverride<TTenantIdentity>(tenant);
            newTenants = newTenants != null ? newTenants.Append(tenantContext).ToArray() : new[] { tenantContext };

            CurrentLocal.Value = newTenants;
            return tenantContext;
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                Debug.Assert(CurrentLocal.Value?.LastOrDefault() == this);

                CurrentLocal.Value = CurrentLocal.Value.Length == 1
                    ? null
                    : CurrentLocal.Value.Take(CurrentLocal.Value.Length - 1).ToArray();

                isDisposed = true;
            }
        }
    }
}
