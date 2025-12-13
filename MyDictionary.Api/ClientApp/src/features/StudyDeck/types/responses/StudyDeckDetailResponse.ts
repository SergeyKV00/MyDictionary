export interface StudyDeckDictionaryResponse {
    dictionaryId: string;
    name: string;
    isSynchronized: boolean;
}

export interface StudyDeckDetailResponse {
    id: string;
    name: string;
    description?: string;
    dictionaries: StudyDeckDictionaryResponse[];
}
