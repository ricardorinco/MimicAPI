using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;

namespace Mimic.Application.Rules.Words.Add
{
    public class R01CreateWord : IRuleHandler<AddWordRuleDto, Word>
    {
        public IRuleHandler<AddWordRuleDto, Word> Next { get; set; }

        public Word Apply(AddWordRuleDto ruleDto, Word word)
        {
            word.Description = ruleDto.Description;
            word.Points = ruleDto.Points;

            return Next.Apply(ruleDto, word);
        }
    }
}
