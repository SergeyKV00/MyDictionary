using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Api.Contracts.DictionaryItems.Requests;

public record GetWordWeightRequest(
    Guid DictionaryId, 
    WeightAggregateType WeightAggregate
);
