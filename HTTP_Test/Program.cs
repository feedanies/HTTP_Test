using HTTP_TEST.Services;
using System.Threading.Tasks;

namespace HTTP_TEST
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            UserService userService = new UserService();
            //var users= await userService.GetAllUsers();
            //foreach (var user in users)
            //{
            //    Console.WriteLine(user);
            //}

            //var user = await userService.GetUserByID(22);
            //Console.WriteLine(user);

            //var dto = new UserDTO
            //{
            //    email = "lala@gmail.com",
            //    first_name = "Lala",
            //    last_name = "Muradova"
            //};

            //var result= await userService.AddUser(dto);
            //Console.WriteLine(result);

            //var dto = new UserDTO
            //{
            //    email = "lala@gmail.com",
            //    first_name = "Lala",
            //    last_name = "Muradova"
            //};

            //var result = await userService.UpdateUser(2,dto);
            //Console.WriteLine(result);



            var result = await userService.DeleteUser(2);
            Console.WriteLine(result);
        }
    }
}