using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Interfaces;
using System;

namespace Mimic.Application.Rules.Words.Query
{
    public class R01AutoFillQuery : IRuleHandler<QueryWordRuleDto, QueryWordRuleDto>
    {
        public IRuleHandler<QueryWordRuleDto, QueryWordRuleDto> Next { get; set; }

        public QueryWordRuleDto Apply(QueryWordRuleDto ruleDto, QueryWordRuleDto queryDto)
        {
            if (!ruleDto.CreatedDate.HasValue)
            {
                ruleDto.CreatedDate = DateTime.Now.AddDays(-30);
            }

            if (!ruleDto.CurrentPage.HasValue)
            {
                ruleDto.CurrentPage = 1;
            }

            if (!ruleDto.PageSize.HasValue)
            {
                ruleDto.PageSize = 10;
            }

            return ruleDto;
        }
    }
}
