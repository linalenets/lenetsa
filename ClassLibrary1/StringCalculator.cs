using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public class StringCalculator
    {
        public int Add(string data)
        {
            if(string.IsNullOrEmpty(data))
                return 0;

            string[] numbers = SplitNumbers(data);
            
            int result = 0;
            int number;
            for (int i = 0; i < numbers.Length; i++)
            {
                number = int.Parse(numbers[i]);
                if (number < 0)
                    throw new ArgumentException();
                else if(number <= 1000) // liczby tylko mniejsze lub równe 1000
                result += number;
            }
            return result;
        }

        private string[] SplitNumbers(string data)
        {
            string[] result;
            if (data[0] == '/') // na początku jest delimiter zdefiniowany
            {
                if (data[2] == '[') // delimiter jest kilkuznakowy
                {
                    List<string> delimiters = new List<string>();
                    string delim = "";
                    int i = 3;
                    while (!Char.IsNumber(data[i]))
                    {
                        if (data[i] == ']')
                        {
                            delimiters.Add(delim);
                            delim = "";
                        }
                        else if (data[i] == '[') ; // nic nie rób, opuśc ten znak
                        else
                            delim += data[i];
                        i++;
                    }
                    data = data.Substring(i); // żeby początkowych "//[]" nie uwzględniło
                    result = data.Split(delimiters.ToArray(), StringSplitOptions.None);
                }
                else // delimiter jednoznakowy
                {
                    char delimiter = data[2];
                    data = data.Substring(3); // żeby początkowych "//" nie uwzględniło
                    result = data.Split(delimiter);
                }
            }
            else // domyślne delimitery
                result = data.Split(',', '\n');
            return result;
        }
    }
}
