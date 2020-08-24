using Mimic.Application.Dtos.Words;
using Mimic.Domain.Models;
using System.Threading.Tasks;

namespace Mimic.Application.Services.Interfaces
{
    public interface IWordService
    {
        Word GetByIdAsync(int id);

        /// <summary>
        /// Adiciona uma nova palavra
        /// </summary>
        /// <param name="request">Request para adicionar uma nova palavra</param>
        /// <returns>Palavra adicionada</returns>
        Task<Word> AddAsync(AddWordRequestDto request);

        /// <summary>
        /// Atualiza uma palavra existente
        /// </summary>
        /// <param name="request">Request para atualziar uma palavra</param>
        /// <returns>Objeto de palavra atualizado</returns>
        Task<Word> UpdateAsync(UpdateWordRequestDto request);

        /// <summary>
        /// Exclusão lógica de uma palavra
        /// </summary>
        /// <param name="id">Id do registro</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
