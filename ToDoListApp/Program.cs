using System;

namespace ToDoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var toDoList = new ToDoList();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Показать все задачи");
                Console.WriteLine("3. Отметить задачу как выполненную");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask(toDoList);
                        break;
                    case "2":
                        ShowAllTasks(toDoList);
                        break;
                    case "3":
                        CompleteTask(toDoList);
                        break;
                    case "4":
                        RemoveTask(toDoList);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }

        static void AddTask(ToDoList toDoList)
        {
            Console.Write("Введите описание задачи: ");
            var description = Console.ReadLine();

            try
            {
                toDoList.AddTask(description);
                Console.WriteLine("Задача добавлена.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ShowAllTasks(ToDoList toDoList)
        {
            Console.WriteLine("\nСписок задач:");
            var tasks = toDoList.GetAllTasks();

            if (!tasks.Any())
            {
                Console.WriteLine("Задачи отсутствуют.");
                return;
            }

            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
        }

        static void CompleteTask(ToDoList toDoList)
        {
            Console.Write("Введите ID задачи для отметки как выполненной: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                if (toDoList.CompleteTask(taskId))
                    Console.WriteLine("Задача отмечена как выполненная.");
                else
                    Console.WriteLine("Задача с указанным ID не найдена.");
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
        }

        static void RemoveTask(ToDoList toDoList)
        {
            Console.Write("Введите ID задачи для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                if (toDoList.RemoveTask(taskId))
                    Console.WriteLine("Задача удалена.");
                else
                    Console.WriteLine("Задача с указанным ID не найдена.");
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
        }
    }
}