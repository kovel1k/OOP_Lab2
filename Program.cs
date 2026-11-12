using System;
using System.Collections;
using System.Collections.Generic;

namespace OOP_Lab3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ЗАВДАННЯ 2: ДОСЛІДЖЕННЯ КОЛЕКЦІЙ ===");
            DemonstrateCollections();

            Console.WriteLine("\n=== ЗАВДАННЯ 3 ТА 4: БІНАРНЕ ДЕРЕВО (Прямий порядок / Preorder) ===");
            DemonstrateBinaryTree();

            Console.ReadLine();
        }

        static void DemonstrateCollections()
        {
            // Створення 6 об'єктів
            var circles = new Circle[]
            {
                new Circle("Червоний", "Чорний", 5.5),
                new Circle("Синій", "Жовтий", 2.0),
                new Circle("Зелений", "Білий", 10.0),
                new Circle("Жовтий", "Червоний", 3.14),
                new Circle("Чорний", "Білий", 7.5),
                new Circle("Білий", "Синій", 1.5)
            };

            // 1. Масив (Array)
            Console.WriteLine("\n--- Масив (Array) ---");
            Circle[] array = new Circle[6];
            Array.Copy(circles, array, 6);
            array[1] = new Circle("Оновлений", "Колір", 9.9); // Оновлення
            foreach (var c in array) c.PrintInfo();

            // 2. Звичайна (non-generic) колекція (ArrayList)
            Console.WriteLine("\n--- Неузагальнена колекція (ArrayList) ---");
            ArrayList arrayList = new ArrayList();
            foreach (var c in circles) arrayList.Add(c); // Упаковка не відбувається, бо це Reference Type, але тип втрачається (object)
            arrayList.RemoveAt(0); // Видалення
            foreach (Circle c in arrayList) c.PrintInfo(); // Потребує явного приведення типу

            // 3. Узагальнена (Generic) колекція (List<T>)
            Console.WriteLine("\n--- Узагальнена колекція (List<T>) ---");
            List<Circle> genericList = new List<Circle>(circles);
            genericList.Add(new Circle("Новий", "Сірий", 12.0)); // Додавання
            
            // Пошук
            Circle found = genericList.Find(c => c.FillColor == "Зелений");
            Console.WriteLine($"\nЗнайдено коло зеленого кольору з радіусом: {found?.Radius}");
            
            // Сортування через IComparer<T> (Завдання 4)
            Console.WriteLine("\nСортування List<T> за площею (використання IComparer<T>):");
            genericList.Sort(new CircleAreaComparer());
            foreach (var c in genericList) c.PrintInfo();
        }

        static void DemonstrateBinaryTree()
        {
            BinaryTree<Circle> tree = new BinaryTree<Circle>();

            // Додаємо 6 об'єктів у дерево. 
            // Під час додавання викликається IComparable<T>.CompareTo для визначення позиції.
            tree.Add(new Circle("Головний", "Чорний", 5.0)); // Root
            tree.Add(new Circle("Лівий1", "Чорний", 3.0));
            tree.Add(new Circle("Правий1", "Чорний", 8.0));
            tree.Add(new Circle("Лівий2", "Чорний", 1.0));
            tree.Add(new Circle("Правий2", "Чорний", 4.0));
            tree.Add(new Circle("Правий3", "Чорний", 9.0));

            Console.WriteLine("Обхід дерева за допомогою власного ітератора (Preorder):");
            // Цей цикл foreach працює завдяки реалізації IEnumerable<T> та IEnumerator<T>
            foreach (Circle circle in tree)
            {
                circle.PrintInfo();
            }
        }
    }
}
