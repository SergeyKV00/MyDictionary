import { apiPost, apiGet} from "@/engine/api";

const endpoint = "Users";

class UserService {
  async get(id: string) {
    return await apiGet(`/${endpoint}/Get?id=${id}`);
  }
  async login(username: string, password: string) {
    const response =  await apiPost<string>(`/${endpoint}/Login`, {
      username,
      password
    });

    return response;
  }
}

export default new UserService();