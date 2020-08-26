using Mimic.Application.Dtos.Words;
using Mimic.Domain.Models;
using System.Threading.Tasks;

namespace Mimic.Application.Interfaces
{
    public interface IWordService
    {
        Word GetById(int id);

        /// <summary>
        /// Adiciona uma nova palavra
        /// </summary>
        /// <param name="ruleDto">Objeto AddWordRuleDto</param>
        /// <returns>Palavra adicionada</returns>
        Task<Word> AddAsync(AddWordRuleDto ruleDto);

        /// <summary>
        /// Atualiza uma palavra existente
        /// </summary>
        /// <param name="ruleDto">Objeto UpdateWordRuleDto</param>
        /// <returns>Objeto de palavra atualizado</returns>
        Task<Word> UpdateAsync(UpdateWordRuleDto ruleDto);

        /// <summary>
        /// Exclusão lógica de uma palavra
        /// </summary>
        /// <param name="ruleDto">Objeto DeleteWordRuleDto</param>
        Task DeleteAsync(DeleteWordRuleDto ruleDto);
    }
}
