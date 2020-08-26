using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Application.Rules;
using Mimic.Application.Rules.Words;
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

        public async Task<Word> AddAsync(AddWordRuleDto ruleDto)
        {
            var word = ApplyRulesHandler<AddWordRuleDto, Word>
                .ApplyRules(ruleDto, new Word(), "Words.Add");

            await wordRepository.AddAsync(word);

            return word;
        }
        public async Task<Word> UpdateAsync(UpdateWordRuleDto ruleDto)
        {
            var foundWord = wordRepository.GetById(ruleDto.Id);
            
            var word = ApplyRulesHandler<UpdateWordRuleDto, Word>
                .ApplyRules(ruleDto, foundWord, "Words.Update");

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
