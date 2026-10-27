using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HTTP_TEST.Services
{

    public class Rootobject
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public User[] data { get; set; }
        public Support support { get; set; }
        public _Meta _meta { get; set; }
    }
    public class RootobjectSingle  //1 user ucun
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public User data { get; set; }
        public Support support { get; set; }
        public _Meta _meta { get; set; }
    }

    public class Support
    {
        public string url { get; set; }
        public string text { get; set; }
    }

    public class _Meta
    {
        public string powered_by { get; set; }
        public string docs_url { get; set; }
        public string upgrade_url { get; set; }
        public string example_url { get; set; }
        public string variant { get; set; }
        public string message { get; set; }
        public Cta cta { get; set; }
        public string context { get; set; }
    }

    public class Cta
    {
        public string label { get; set; }
        public string url { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }

        public override string ToString()
        {
            return $@"
ID: {id}
Mail: {email}
Firstname: {first_name}
Lastname: {last_name}
Avatar: {avatar}
";
        }

    }

    public class UserDTO //Create, Update
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

    }


    public class UserService
    {
        private readonly string MyApiKey = "reqres_087027fff03e487fbd3bd61d1fbab89e";
        private readonly string url = "https://reqres.in/api/users";
        private readonly HttpClient httpClient = new HttpClient();

        public UserService()
        {
            httpClient.DefaultRequestHeaders.Add("x-api-key", MyApiKey);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Rootobject>(jsonString);

            return data?.data?.ToList() ?? new List<User>();
        }
        public async Task<User?> GetUserByID(int id)
        {
            var response = await httpClient.GetAsync($"{url}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<RootobjectSingle>(jsonString);

            return data?.data;
        }

        public async Task<string> AddUser(UserDTO userDto) //POST
        {
            var json = JsonConvert.SerializeObject(userDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);

            Console.WriteLine($"Status code: {(int)response.StatusCode}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateUser(int id, UserDTO userDto) //PUT
        {
            var json = JsonConvert.SerializeObject(userDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{url}/{id}", content);
            Console.WriteLine($"Status code: {(int)response.StatusCode}");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteUser(int id) //Delete
        {
            var response = await httpClient.DeleteAsync($"{url}/{id}");
            Console.WriteLine($"Status code: {(int)response.StatusCode}");
            return await response.Content.ReadAsStringAsync(); //hecne qaytarmir
        }
    }
}