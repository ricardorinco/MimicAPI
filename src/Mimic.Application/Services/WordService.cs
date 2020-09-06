using Mimic.Application.Arguments.Dtos;
using Mimic.Application.Arguments.Dtos.Validations;
using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Arguments.Enums;
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
            var nameSpace = $"{EntitiesEnum.Words}.{ActionsEnum.Query}";

            ruleDto = ApplyRulesHandler<QueryWordRuleDto, QueryWordRuleDto>
                .ApplyRules(ruleDto, ruleDto, nameSpace);

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
            var nameSpace = $"{EntitiesEnum.Words}.{ActionsEnum.Add}";
            var validation = ApplyValidationsHandler<AddWordRuleDto>
                .ApplyRules(ruleDto, nameSpace);
            
            if (!validation.IsValid)
            {
                return new ApplicationDto<Word>(validation);
            }

            var word = ApplyRulesHandler<AddWordRuleDto, Word>
                .ApplyRules(ruleDto, new Word(), nameSpace);

            await wordRepository.AddAsync(word);

            return new ApplicationDto<Word>(word);
        }
        public async Task<ApplicationDto<Word>> UpdateAsync(UpdateWordRuleDto ruleDto)
        {
            var nameSpace = $"{EntitiesEnum.Words}.{ActionsEnum.Update}";

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
                .ApplyRules(ruleDto, nameSpace);

            if (!validation.IsValid)
            {
                return new ApplicationDto<Word>(validation);
            }
            
            var word = ApplyRulesHandler<UpdateWordRuleDto, Word>
                .ApplyRules(ruleDto, foundWord, nameSpace);

            await wordRepository.UpdateAsync(word);

            return new ApplicationDto<Word>(word);
        }
        public async Task<ApplicationDto<Word>> DeleteAsync(DeleteWordRuleDto ruleDto)
        {
            var nameSpace = $"{EntitiesEnum.Words}.{ActionsEnum.Delete}";

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

            var word = ApplyRulesHandler<DeleteWordRuleDto, Word>
                .ApplyRules(ruleDto, foundWord, nameSpace);

            await wordRepository.UpdateAsync(word);

            return null;
        }
    }
}