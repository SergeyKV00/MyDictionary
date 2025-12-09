import type { DictionaryItemExampleType } from "./DictionaryItemExampleType";
import type { WordFormType } from "./WordFormType";

export class DictionaryItemType {
  id: string;
  term: string;
  meaning: string;
  weight: number;
  examples: DictionaryItemExampleType[];
  wordForm?: WordFormType | null;

  constructor(
    id: string,
    term: string,
    meaning: string,
    weight: number,
    examples: DictionaryItemExampleType[],
    wordForm?: WordFormType | null
  ) {
    this.id = id;
    this.term = term;
    this.meaning = meaning;
    this.weight = weight;
    this.examples = examples;
    this.wordForm = wordForm;
  }

  static clone(original: DictionaryItemType) {
    return new DictionaryItemType(
      original.id,
      original.term,
      original.meaning,
      original.weight,
      original.examples,
      original.wordForm
    );
  }
}