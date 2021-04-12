using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Lab2
{
    public class Heap<T>
    {
        public T[] _array; //массив сортируемых элементов
        private int heapsize;//число необработанных элементов
        private IComparer<T> _comparer;
        public int operationsCount;

        public Heap(T[] a, IComparer<T> comparer)
        {
            _array = a;
            heapsize = a.Length;
            _comparer = comparer;
            build_max_heap();
        }

        public void HeapSort()
        {
            for (int i = _array.Length - 1; i > 0; i--)
            {
                T temp = _array[0];//Переместим текущий максимальный элемент из нулевой позиции в хвост массива
                _array[0] = _array[i];
                _array[i] = temp;

                heapsize--;//Уменьшим число необработанных элементов
                           //operationsCount++;
                max_heapify(0);//Восстановление свойства пирамиды
            }
        }

        private int left(int i) { return 2 * i + 1; }//Индекс левого потомка
        private int right(int i) { return 2 * i + 2; }//Индекс правого потомка

        //Метод переупорядочивает элементы пирамиды при условии,
        //что элемент с индексом i меньше хотя бы одного из своих потомков, нарушая тем самым свойство невозрастающей пирамиды
        public void max_heapify(int i)
        {
            int l = left(i);
            int r = right(i);
            int lagest = i;
            if (l < heapsize && _comparer.Compare(_array[l], _array[i]) > 0)
            {
                lagest = l;
                operationsCount++;
            }
            if (r < heapsize && _comparer.Compare(_array[r], _array[lagest]) > 0)
            {
                lagest = r;
                operationsCount++;
            }
            if (lagest != i)
            {
                T temp = _array[i];
                _array[i] = _array[lagest];
                _array[lagest] = temp;
                operationsCount++;

                max_heapify(lagest);
            }
        }

        //метод строит невозрастающую пирамиду
        public void build_max_heap()
        {
            int i = (_array.Length - 1) / 2;

            while (i >= 0)
            {
                max_heapify(i);
                i--;
                //operationsCount++;
            }
        }
    }

    public class Lab2Main
    {
        public List<string> generateStrings(int numOfWords, int minLength, int maxLength)
        {

            int num_words = numOfWords;

            var lstWords = new List<string>();

            // Создаем массив букв, которые мы будем использовать.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            // Создаем генератор случайных чисел.
            Random rand = new Random();

            // Делаем слова.
            for (int i = 1; i <= num_words; i++)
            {
                // Сделайте слово.
                string word = "";
                for (int j = 1; j <= rand.Next(minLength, maxLength); j++)
                {
                    // Выбор случайного числа
                    // для выбора буквы из массива букв.
                    int letter_num = rand.Next(0, letters.Length - 1);

                    // Добавить письмо.
                    word += letters[letter_num];
                }

                // Добавьте слово в список.
                lstWords.Add(word);
            }
            return lstWords;
        }

        public static void Main(string[] args)
        {
            var test = new Lab2Main();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("C:\\Users\\1\\source\\repos\\Lab2\\Lab2\\XMLFile1.xml");
            var root = xDoc.DocumentElement;
            var experiments = root.SelectNodes("experiment").Item(0).ChildNodes;
            using (StreamWriter writer = new StreamWriter("C:\\Users\\1\\source\\repos\\Lab2\\Lab2\\results.txt"))
            {
                foreach (XmlNode experiment in experiments)
                {
                    var name = experiment.Attributes["name"].Value;
                    var type = name.Split(" ")[0];
                    var repeat = Int32.Parse(experiment.Attributes["repeat"].Value);
                    var minElement = Int32.Parse(experiment.Attributes["minElement"].Value);
                    var maxElement = Int32.Parse(experiment.Attributes["maxElement"].Value);
                    var startLength = Int32.Parse(experiment.Attributes["startLength"].Value);
                    var maxLength = Int32.Parse(experiment.Attributes["maxLength"].Value);
                    if (type.Equals("Arithmetic"))
                    {
                        var diff = Int32.Parse(experiment.Attributes["diff"].Value);
                        for (var j = startLength; j <= maxLength; j += diff)
                        {
                            var operationsOnLength = "" + j.ToString();
                            for (var i = 0; i < repeat; i++)
                            {
                                var arr = test.generateStrings(j, minElement, maxElement).ToArray();
                                var comparer = StringComparer.Ordinal;
                                Heap<string> heap = new Heap<string>(arr, comparer);
                                heap.HeapSort();
                                writer.WriteLine(operationsOnLength + " " + heap.operationsCount.ToString());
                            }
                        }
                    }
                    else
                    {
                        var diff = Double.Parse(experiment.Attributes["diff"].Value);
                        for (var j = (double)startLength; j <= maxLength; j *= diff)
                        {
                            var convertedLength = (int)j;
                            var operationsOnLength = "" + convertedLength.ToString();
                            for (var i = 0; i < repeat; i++)
                            {
                                var arr = test.generateStrings(convertedLength, minElement, maxElement).ToArray();
                                var comparer = StringComparer.Ordinal;
                                Heap<string> heap = new Heap<string>(arr, comparer);
                                heap.HeapSort();
                                writer.WriteLine(operationsOnLength + " " + heap.operationsCount.ToString());
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Результаты вычислений в виде .txt файла можно найти в папке с программой, для выхода нажмите любую кнопку");
            Console.ReadLine();
        }
    }
}