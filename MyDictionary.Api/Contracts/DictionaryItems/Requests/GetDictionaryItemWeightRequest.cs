using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Api.Contracts.DictionaryItems.Requests;

public record GetDictionaryItemWeightRequest(
    Guid DictionaryId, 
    WeightAggregateType WeightAggregate
);
