export class DictionaryItemCreateRequests {
  dictionaryId: string;
  term: string = "";
  meaning: string = "";
  weight: number = 1;

  constructor(dictionaryId: string) {
    this.dictionaryId = dictionaryId;
  }
}