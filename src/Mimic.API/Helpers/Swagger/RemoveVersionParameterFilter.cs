using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Mimic.WebApi.Helpers.Swagger
{
    /// <summary>
    /// Classe de extensão para remover o parâmetro de versionamento da api
    /// </summary>
    public class RemoveVersionParameterFilter : IOperationFilter
    {
        /// <summary>
        /// Remove o parâmetro do versionamento da api
        /// </summary>
        /// <param name="operation">OpenApiOperation</param>
        /// <param name="context">OperationFilterContext</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }
}