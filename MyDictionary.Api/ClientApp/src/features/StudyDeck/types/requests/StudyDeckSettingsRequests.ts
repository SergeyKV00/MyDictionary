export interface AddDictionaryToStudyDeckRequest {
    studyDeckId: string;
    dictionaryId: string;
    isSynchronized: boolean;
}

export interface AddWordsToStudyDeckRequest {
    studyDeckId: string;
    wordIds: string[];
}
