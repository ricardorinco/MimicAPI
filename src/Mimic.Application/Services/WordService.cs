using Mimic.Application.Dtos;
using Mimic.Application.Dtos.Validations;
using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Application.Resources;
using Mimic.Application.Rules;
using Mimic.Application.Validations;
using Mimic.Domain.Models;
using Mimic.Infra.Data.Interfaces;
using System.Collections.Generic;
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

        public async Task<IList<Word>> GetByQueryAsync(QueryWordRuleDto ruleDto)
        {
            ruleDto = ApplyRulesHandler<QueryWordRuleDto, QueryWordRuleDto>
                .ApplyRules(ruleDto, ruleDto, "Words.Query");

            return await wordRepository.GetByQueryAsync(
                ruleDto.CreatedDate.Value,
                ruleDto.CurrentPage.Value,
                ruleDto.PageSize.Value
            );
        }

        public async Task<Word> GetByIdAsync(int id)
        {
            return await wordRepository.GetByIdAsync(id);
        }

        public async Task<ApplicationDto<Word>> AddAsync(AddWordRuleDto ruleDto)
        {
            var validation = ApplyValidationsHandler<AddWordRuleDto>
                .ApplyRules(ruleDto, "Words.Add");
            
            if (!validation.IsValid)
            {
                return new ApplicationDto<Word>(validation);
            }

            var word = ApplyRulesHandler<AddWordRuleDto, Word>
                .ApplyRules(ruleDto, new Word(), "Words.Add");

            await wordRepository.AddAsync(word);

            return new ApplicationDto<Word>(word);
        }
        public async Task<ApplicationDto<Word>> UpdateAsync(UpdateWordRuleDto ruleDto)
        {
            var foundWord = await GetByIdAsync(ruleDto.Id);
            if (foundWord == null)
            {
                var customValidation = new CustomValidation();
                customValidation.AddError(
                    "Word",
                    string.Format(ValidationsResource.DataNotFoundDatabase, "Word"),
                    "DataNotFoundDatabase"
                );

                return new ApplicationDto<Word>(customValidation);
            }

            var validation = ApplyValidationsHandler<UpdateWordRuleDto>
                .ApplyRules(ruleDto, "Words.Add");

            if (!validation.IsValid)
            {
                return new ApplicationDto<Word>(validation);
            }
            
            var word = ApplyRulesHandler<UpdateWordRuleDto, Word>
                .ApplyRules(ruleDto, foundWord, "Words.Update");

            await wordRepository.UpdateAsync(word);

            return new ApplicationDto<Word>(word);
        }
        public async Task DeleteAsync(DeleteWordRuleDto ruleDto)
        {
            var foundWord = await GetByIdAsync(ruleDto.Id);

            var word = ApplyRulesHandler<DeleteWordRuleDto, Word>
                .ApplyRules(ruleDto, foundWord, "Words.Delete");

            await wordRepository.UpdateAsync(word);
        }
    }
}
