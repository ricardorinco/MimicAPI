using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using System.Linq;

namespace Mimic.WebApi.Helpers.Swagger
{
    /// <summary>
    /// Classe de extensão para obter o número do versionamento da api
    /// </summary>
    public static class ActionDescriptorExtensions
    {
        /// <summary>
        /// Obtêm o número de versionamento da api
        /// </summary>
        /// <param name="actionDescriptor">ActionDescriptor</param>
        /// <returns>ApiVersionModel</returns>
        public static ApiVersionModel GetApiVersion(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor?.Properties
                .Where((kvp) => ((Type)kvp.Key).Equals(typeof(ApiVersionModel)))
                .Select(kvp => kvp.Value as ApiVersionModel)
                .FirstOrDefault();
        }
    }
}
