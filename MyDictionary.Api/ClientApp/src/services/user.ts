import Api from "@/engine/api";

const endpoint = "Users";

class UserService {
  async get(id: string) {
    return await Api.get(`/${endpoint}/Get?id=${id}`);
  }
  // async login(username: string, password: string) {
  //   const response =  await Api.post<string>(`/${endpoint}/Login`, {
  //     username,
  //     password
  //   });

  //   return response;
  // }
}

export default new UserService();