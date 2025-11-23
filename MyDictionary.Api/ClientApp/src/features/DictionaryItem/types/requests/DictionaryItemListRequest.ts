import { PageRequest } from "@/engine/types/PageRequest";

export class DictionaryItemListRequest extends PageRequest {
  dictionaryId: string;
  term?: string | undefined;
  meaning?: string | undefined;

  constructor(dictionaryId: string) {
    super();
    this.dictionaryId = dictionaryId;
  }
}

