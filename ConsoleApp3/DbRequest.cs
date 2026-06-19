namespace ConsoleApp3
{
    public class DbRequest
    {
        public static void Add(AppDbContext db)
        {
            Console.WriteLine("Enter user name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter country:");
            string country = Console.ReadLine();
            Console.WriteLine("Enter city:");
            string city = Console.ReadLine();
            Console.WriteLine("Enter street:");
            string street = Console.ReadLine();

            User newUser = new User
            {
                Name = name,
                Email = email,
                Adress = new Adress
                {
                    Country = country,
                    City = city,
                    Street = street
                }
            };

            db.Users.Add(newUser);
            db.SaveChanges();

            Console.WriteLine("User have been added!");
            Console.ReadKey();
        }

        public static void Show(AppDbContext db)
        {
            Console.WriteLine("Enter user name:");
            string name = Console.ReadLine();

            var users = db.Users.Where(u => u.Name == name).ToList();
            if (users != null)
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"ID={user.Id}, Name={user.Name}, Email={user.Email}");
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("user not found.");
            }
        }


        public static void ShowUserWithAdress(AppDbContext db)
        {
            Console.Write("Enter user name: ");
            string searchName = Console.ReadLine();

            var query = db.Users.Join(
                db.Adresses,                    
                user => user.Id,               
                address => address.UserId,     
                (user, address) => new         
                {
                    UserName = user.Name,
                    UserEmail = user.Email,
                    City = address.City,
                    Street = address.Street,
                    Country = address.Country,
                }
            ).Where(item => item.UserName == searchName).ToList();

            if (query.Count == 0)
            {
                Console.WriteLine($"User '{searchName}' not found.");
                return;
            }

            foreach (var item in query)
            {
                Console.WriteLine($"{item.UserName} ({item.UserEmail}), Country: {item.Country}, City: {item.City}, Street: {item.Street}");
            }
            Console.ReadKey();

        }


        public static void Start()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("1. Add User");
                        Console.WriteLine("2. Show Users");
                        Console.WriteLine("3. Show User with Address");
                        Console.WriteLine("0. Exit");

                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                Add(db);
                                break;

                            case "2":
                                Show(db);
                                break;

                            case "3":
                                ShowUserWithAdress(db);
                                break;

                            case "0":
                                Console.WriteLine("Выход из программы...");
                                return;

                            default:
                                Console.WriteLine("Неверный ввод! Нажмите любую клавишу для повтора...");
                                Console.ReadKey();
                                break;
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
