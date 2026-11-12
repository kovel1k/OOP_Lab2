using System;
using System.Collections.Generic;

namespace OOP_Lab3_2
{
    // Клас реалізує IComparable<Circle> для сортування та побудови бінарного дерева
    public class Circle : IComparable<Circle>
    {
        public string FillColor { get; set; }
        public string OutlineColor { get; set; }
        public double Radius { get; set; }

        public Circle(string fillColor, string outlineColor, double radius)
        {
            FillColor = fillColor;
            OutlineColor = outlineColor;
            Radius = radius;
        }

        public double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        public double GetCircumference()
        {
            return 2 * Math.PI * Radius;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Коло [Радіус: {Radius,5:F2} | Заповнення: {FillColor,-10} | Контур: {OutlineColor,-10} | Площа: {GetArea(),6:F2} | Довжина: {GetCircumference(),6:F2}]");
        }

        // Реалізація IComparable<T> (порівняння за радіусом за замовчуванням)
        public int CompareTo(Circle other)
        {
            if (other == null) return 1;
            return this.Radius.CompareTo(other.Radius);
        }
    }

    // Реалізація IComparer<T> для альтернативного сортування (наприклад, за площею)
    public class CircleAreaComparer : IComparer<Circle>
    {
        public int Compare(Circle x, Circle y)
        {
            if (x == null || y == null) return 0;
            return x.GetArea().CompareTo(y.GetArea());
        }
    }
}
