using Mimic.Application.Dtos.Words;
using Mimic.Application.Rules.Words;
using Mimic.Application.Rules.Words.Update;
using Mimic.Application.Interfaces;
using Mimic.Domain.Interfaces.Repositories;
using Mimic.Domain.Models;
using System.Threading.Tasks;
using Mimic.Application.Rules.Words.Add;

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

        public async Task<Word> AddAsync(AddWordRuleDto ruleDto)
        {
            var word = R00AddWordRules.ApplyRules(ruleDto, new Word());

            await wordRepository.AddAsync(word);

            return word;
        }
        public async Task<Word> UpdateAsync(UpdateWordRuleDto ruleDto)
        {
            var foundWord = wordRepository.GetById(ruleDto.Id);
            
            var word = R00UpdateWordRules.ApplyRules(ruleDto, foundWord);

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
