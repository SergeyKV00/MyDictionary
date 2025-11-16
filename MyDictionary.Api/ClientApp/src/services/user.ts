import { api } from '@/engine/api'

const endpoint = "Users";

class UserService {
  async getUser(id: string) {
    const r = await api.get(`/${endpoint}/Get?id=${id}`);
    return r.data;
  }
  async login(userName: string, password: string) {
    const r = await api.post(`/${endpoint}/Login`, {
        userName,
        password
    });
    return r.data;
  }
}

export default new UserService();