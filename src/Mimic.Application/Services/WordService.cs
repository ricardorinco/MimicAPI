using Mimic.Application.Dtos.Words;
using Mimic.Application.Rules.Words;
using Mimic.Application.Rules.Words.Update;
using Mimic.Application.Services.Interfaces;
using Mimic.Domain.Interfaces.Repositories;
using Mimic.Domain.Models;
using System.Threading.Tasks;

namespace Mimic.Application.Services
{
    public class WordService : IWordService
    {
        private readonly IWordRepository wordRepository;

        public WordService(IWordRepository wordRepository)
        {
            this.wordRepository = wordRepository;
        }

        public Word GetByIdAsync(int id)
        {
            return wordRepository.GetById(id);
        }

        public async Task<Word> AddAsync(AddWordRequestDto request)
        {
            var word = AddWordRule.Apply(request);

            await wordRepository.AddAsync(word);

            return word;
        }
        public async Task<Word> UpdateAsync(UpdateWordRequestDto request)
        {
            var foundWord = wordRepository.GetById(request.Id);

            var word = R01UpdateWord.Apply(request, foundWord);
            word = R02AutoFillWord.Apply(word);
            
            await wordRepository.UpdateAsync(word);

            return word;
        }
        public async Task DeleteAsync(int id)
        {
            var foundWord = wordRepository.GetById(id);

            DeleteWordRule.Apply(foundWord);
            await wordRepository.UpdateAsync(foundWord);
        }
    }
}
