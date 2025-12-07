import type { DictionaryItemExampleType } from "./DictionaryItemExampleType";

export class DictionaryItemType {
  id: string;
  term: string;
  meaning: string;
  weight: number;
  examples: DictionaryItemExampleType[];

  constructor(
    id: string,
    term: string,
    meaning: string,
    weight: number,
    examples: DictionaryItemExampleType[]
  ) {
    this.id = id;
    this.term = term;
    this.meaning = meaning;
    this.weight = weight;
    this.examples = examples;
  }

  static clone(original: DictionaryItemType) {
    return new DictionaryItemType(
      original.id,
      original.term,
      original.meaning,
      original.weight,
      original.examples
    );
  }
}