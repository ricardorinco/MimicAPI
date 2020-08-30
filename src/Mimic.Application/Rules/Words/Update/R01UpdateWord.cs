using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;

namespace Mimic.Application.Rules.Words.Update
{
    public class R01UpdateWord : IRuleHandler<UpdateWordRuleDto, Word>
    {
        public IRuleHandler<UpdateWordRuleDto, Word> Next { get; set; }

        public Word Apply(UpdateWordRuleDto ruleDto, Word foundWord)
        {
            foundWord.Description = ruleDto.Description;
            foundWord.Points = ruleDto.Points;
            foundWord.Active = ruleDto.Active;

            return Next.Apply(ruleDto, foundWord);
        }
    }
}
