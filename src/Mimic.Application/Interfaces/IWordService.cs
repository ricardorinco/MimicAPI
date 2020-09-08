using Mimic.Application.Arguments.Dtos;
using Mimic.Application.Arguments.Dtos.Words;
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
        /// <param name="ruleDto">Objeto QueryWordDto</param>
        /// <returns>Lista das palavras encontradas</returns>
        Task<IList<Word>> GetByQueryAsync(QueryWordDto ruleDto);

        /// <summary>
        /// Realiza a busca de uma palavra através do Id informado
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <returns>Palavra encontrada</returns>
        Task<Word> GetByIdAsync(int id);

        /// <summary>
        /// Realiza a inclusão da palavra informada
        /// </summary>
        /// <param name="ruleDto">Objeto AddWordDto</param>
        /// <returns>Palavra adicionada</returns>
        Task<ApplicationDto<Word>> AddAsync(AddWordDto ruleDto);

        /// <summary>
        /// Realiza a atualização da palavra informada
        /// </summary>
        /// <param name="ruleDto">Objeto UpdateWordDto</param>
        /// <returns>Palavra atualizada</returns>
        Task<ApplicationDto<Word>> UpdateAsync(UpdateWordDto ruleDto);

        /// <summary>
        /// Realiza a exclusão lógica de uma palavra através do Id informado
        /// </summary>
        /// <param name="ruleDto">Objeto DeleteWordDto</param>
        /// <returns>Palavra atualizada</returns>
        Task<ApplicationDto<Word>> DeleteAsync(DeleteWordDto ruleDto);
    }
}
