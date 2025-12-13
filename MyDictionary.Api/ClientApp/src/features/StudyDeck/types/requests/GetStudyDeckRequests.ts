export interface GetStudyDeckRequest {
    id: string;
}

export interface GetStudyDeckWordsRequest {
    studyDeckId: string;
    page: number;
    pageSize: number;
    sortField?: string;
    sortOrder?: string;
}
