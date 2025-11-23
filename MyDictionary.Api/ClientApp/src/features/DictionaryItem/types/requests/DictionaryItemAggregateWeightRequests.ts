export class DictionaryItemAggregateWeightRequests {
  dictionaryId: string;
  weightAggregate: AggregateWeightType;

  constructor(dictionaryId: string, agrWeight: AggregateWeightType) {
    this.dictionaryId = dictionaryId;
    this.weightAggregate = agrWeight;
  }
}

export enum AggregateWeightType {
  min = 1,
  max = 2
}