export interface DictionaryItemExampleType {
  readonly type: "DictionaryItemExampleType";
  id: string
  dictionaryItemId: string
  example: string
  translation?: string | null
}