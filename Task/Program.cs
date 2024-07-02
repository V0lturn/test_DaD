namespace Task
{
    internal class Program
    {
        public static List<int> GetNumbers(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found", path);
            }

            List<int> numbers = new List<int>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (int.TryParse(line, out int number))
                    {
                        numbers.Add(number);
                    }
                }
            }

            return numbers;
        }

        public static double FindMedian(List<int> numbers)
        {
            List<int> ascending_list = new List<int>(numbers);
            ascending_list.Sort();

            int count = ascending_list.Count;
            if (count % 2 == 0)
            {
                int middle1 = ascending_list[count / 2 - 1];
                int middle2 = ascending_list[count / 2];
                return (middle1 + middle2) / 2.0;
            }
            else
            {
                return ascending_list[count / 2];
            }
        }

        public static List<int> FindLargestSequence(List<int> numbers, Comparison<int> comparison)
        {
            List<int> largestSequence = new List<int>();
            List<int> currentSequence = new List<int> { numbers[0] };

            for (int i = 1; i < numbers.Count; i++)
            {
                if (comparison(numbers[i], numbers[i - 1]) > 0)
                {
                    currentSequence.Add(numbers[i]);
                }
                else
                {
                    if (currentSequence.Count > largestSequence.Count)
                    {
                        largestSequence = new List<int>(currentSequence);
                    }
                    currentSequence = new List<int> { numbers[i] };
                }
            }

            if (currentSequence.Count > largestSequence.Count)
            {
                largestSequence = currentSequence;
            }

            return largestSequence;
        }

        static void Main(string[] args)
        {
            string path = "D:\\10m.txt";
            List<int> numbers = GetNumbers(path);

            // 1. Максимальне число в файлі
            int max_number = numbers.Max();
            Console.WriteLine($"Max number in file = {max_number}\n");

            // 2. Мінімальне  число в файлі
            int min_number = numbers.Min();
            Console.WriteLine($"Min number in file = {min_number}\n");

            // 3. Медіана
            double median = FindMedian(numbers);
            Console.WriteLine($"Median = {median}\n");

            // 4. Середнє арифметичне значення
            double average = numbers.Average();
            Console.WriteLine($"Average value = {average}\n");

            // 5. Найбільшу послідовність чисел (які ідуть один за одним), яка збільшується (опціонально)
            List<int> increasingSequence = FindLargestSequence(numbers, (a, b) => a.CompareTo(b));

            Console.WriteLine("Longest increasing sequence: ");
            foreach (int number in increasingSequence)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();

            // 6. Найбільшу послідовність чисел (які ідуть один за одним), яка зменшується (опціонально)
            List<int> decreasingSequence = FindLargestSequence(numbers, (a, b) => b.CompareTo(a));

            Console.WriteLine("\nLongest decreasing sequence: ");
            foreach (int number in decreasingSequence)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();
        }
    }
}
