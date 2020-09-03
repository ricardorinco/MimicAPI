using Mimic.Application.Dtos;
using Mimic.Application.Dtos.Words;
using Mimic.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimic.Application.Interfaces
{
    /// <summary>
    /// Serviço de Palavras
    /// </summary>
    public interface IWordService
    {
        /// <summary>
        /// Realiza uma consulta de palavras de acordo com os filtros informados
        /// </summary>
        /// <param name="ruleDto">Objeto QueryWordRuleDto</param>
        /// <returns>Lista das palavras encontradas</returns>
        Task<IList<Word>> GetByQueryAsync(QueryWordRuleDto ruleDto);

        /// <summary>
        /// Realiza a busca de uma palavra através do Id informado
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <returns>Palavra encontrada</returns>
        Task<Word> GetByIdAsync(int id);

        /// <summary>
        /// Realiza a inclusão da palavra informada
        /// </summary>
        /// <param name="ruleDto">Objeto AddWordRuleDto</param>
        /// <returns>Palavra adicionada</returns>
        Task<ApplicationDto<Word>> AddAsync(AddWordRuleDto ruleDto);

        /// <summary>
        /// Realiza a atualização da palavra informada
        /// </summary>
        /// <param name="ruleDto">Objeto UpdateWordRuleDto</param>
        /// <returns>Palavra atualizada</returns>
        Task<Word> UpdateAsync(UpdateWordRuleDto ruleDto);

        /// <summary>
        /// Realiza a exclusão lógica de uma palavra através do Id informado
        /// </summary>
        /// <param name="ruleDto">Objeto DeleteWordRuleDto</param>
        Task DeleteAsync(DeleteWordRuleDto ruleDto);
    }
}
