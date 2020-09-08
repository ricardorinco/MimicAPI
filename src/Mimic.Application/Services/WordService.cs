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

        public async Task<IList<Word>> GetByQueryAsync(QueryWordDto ruleDto)
        {
            var nameSpace = $"{EntitiesEnum.Words}.{ActionsEnum.Query}";

            ruleDto = ApplyRulesHandler<QueryWordDto, QueryWordDto>
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

        public async Task<ApplicationDto<Word>> AddAsync(AddWordDto ruleDto)
        {
            var nameSpace = $"{EntitiesEnum.Words}.{ActionsEnum.Add}";
            var validation = ApplyValidationsHandler<AddWordDto>
                .ApplyRules(ruleDto, nameSpace);
            
            if (!validation.IsValid)
            {
                return new ApplicationDto<Word>(validation);
            }

            var word = ApplyRulesHandler<AddWordDto, Word>
                .ApplyRules(ruleDto, new Word(), nameSpace);

            await wordRepository.AddAsync(word);

            return new ApplicationDto<Word>(word);
        }
        public async Task<ApplicationDto<Word>> UpdateAsync(UpdateWordDto ruleDto)
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

            var validation = ApplyValidationsHandler<UpdateWordDto>
                .ApplyRules(ruleDto, nameSpace);

            if (!validation.IsValid)
            {
                return new ApplicationDto<Word>(validation);
            }
            
            var word = ApplyRulesHandler<UpdateWordDto, Word>
                .ApplyRules(ruleDto, foundWord, nameSpace);

            await wordRepository.UpdateAsync(word);

            return new ApplicationDto<Word>(word);
        }
        public async Task<ApplicationDto<Word>> DeleteAsync(DeleteWordDto ruleDto)
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

            var word = ApplyRulesHandler<DeleteWordDto, Word>
                .ApplyRules(ruleDto, foundWord, nameSpace);

            await wordRepository.UpdateAsync(word);

            return null;
        }
    }
}