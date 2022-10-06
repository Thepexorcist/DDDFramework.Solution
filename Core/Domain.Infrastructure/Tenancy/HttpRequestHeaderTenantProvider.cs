using Domain.Infrastructure.Tenancy.Interfaces;
using Domain.Tenancy;
using Domain.Tenancy.Interfaces;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Tenancy
{
    public class HttpRequestHeaderTenantProvider<TTenantIdentity> : ITenantProvider<TTenantIdentity>
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _tenantKey = string.Empty;

        #endregion

        #region Constructors

        public HttpRequestHeaderTenantProvider(IHttpContextAccessor httpContextAccessor, string tenantKey)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantKey = tenantKey;
        }

        #endregion

        public ITenant<TTenantIdentity> GetTenant()
        {
            if (!_httpContextAccessor.HttpContext.Request.Headers.Keys.Contains(_tenantKey))
            {
                throw new InvalidOperationException(string.Format("Excepted to find a tenant in request header with the key {0}", _tenantKey));
            }

            var tenantId = _httpContextAccessor.HttpContext.Request.Headers["Tenant"].ToString();

            if (typeof(IdentityBase<int>).IsAssignableFrom(typeof(TTenantIdentity)))
            {
                var identity = (TTenantIdentity)Activator.CreateInstance(typeof(TTenantIdentity), int.Parse(tenantId));
                return new ResolvedTenant<TTenantIdentity>(identity);
            }
            if (typeof(IdentityBase<Guid>).IsAssignableFrom(typeof(TTenantIdentity)))
            {
                var identity = (TTenantIdentity)Activator.CreateInstance(typeof(TTenantIdentity), Guid.Parse(tenantId));
                return new ResolvedTenant<TTenantIdentity>(identity);
            }
            if (typeof(IdentityBase<string>).IsAssignableFrom(typeof(TTenantIdentity)))
            {
                var identity = (TTenantIdentity)Activator.CreateInstance(typeof(TTenantIdentity), tenantId);
                return new ResolvedTenant<TTenantIdentity>(identity);
            }

            var resolvedTenant = new ResolvedTenant<TTenantIdentity>(TConverter.ChangeType<TTenantIdentity>(tenantId));
            return resolvedTenant;
        }

        public static class TConverter
        {
            public static T ChangeType<T>(object value)
            {
                return (T)ChangeType(typeof(T), value);
            }

            public static object ChangeType(Type t, object value)
            {
                TypeConverter tc = TypeDescriptor.GetConverter(t);
                return tc.ConvertFrom(value);
            }
        }
    }
}
