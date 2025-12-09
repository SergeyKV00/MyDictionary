import type { WordFormType } from "../models/WordFormType";

export interface WordFormCreateRequests extends WordFormType {
  dictionaryItemId: string
}