export class DictionaryItemType { // TODO Hueta
  id: string;
  term: string;
  meaning: string;
  weight: number;

  constructor(
    id: string,
    term: string,
    meaning: string,
    weight: number,
  ) {
    this.id = id;
    this.term = term;
    this.meaning = meaning;
    this.weight = weight;
  }

  static clone(original: DictionaryItemType) {
    return new DictionaryItemType(
      original.id,
      original.term,
      original.meaning,
      original.weight
    );
  }
}