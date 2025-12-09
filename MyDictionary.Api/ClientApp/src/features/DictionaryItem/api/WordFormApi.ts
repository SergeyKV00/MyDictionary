import api, { EndPointBuilder } from "@/engine/api";
import type { WordFormCreateRequests } from "../types/requests/WordFormCreateRequests";
import type { WordFormType } from "../types/models/WordFormType";

const endpoint = new EndPointBuilder("WordForms");

class WordFormApi {
  async create(payload: WordFormCreateRequests) {
    return await api.post<string>(endpoint.build("Create"), payload);
  }
  async update(payload: WordFormType) {
    return await api.put<string>(endpoint.build("Update"), payload);
  }
}

export default new WordFormApi();