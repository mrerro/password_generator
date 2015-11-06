using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace keygen
{
    internal class Generator
    {
        public char[,] keyArray;
        public int[] algoritmArray;
        private int stepCount;
        private int stopKey = 0;

        public string generateKey(string line)
        {
            var outLine = "";
            foreach (var item in line)
            {
                for (var i = 0; i < keyArray.GetLength(0); i++)
                {
                    for (var j = 0; j < keyArray.GetLength(1); j++)
                    {
                        if (item == keyArray[i, j])
                        {
                            outLine = outLine + step(i, j);
                        }
                    }
                }
            }
            if ((stopKey = stopKey + outLine.Length) < 6)
            {
                outLine = outLine + generateKey(line);
            }
            return outLine;
        }

        public void arrayGenerator(string[] lines)
        {
            var d = Math.Sqrt(lines[0].Length);
            var p = (int)d;
            if (d - p == 0)
            {
                keyArray = new char[p, p];
                algoritmArray = new int[lines[1].Length];
                var count = 0;
                for (var i = 0; i < p; i++)
                {
                    for (var j = 0; j < p; j++)
                    {

                        keyArray[i, j] = lines[0].ElementAt(count);
                        count++;
                    }
                }
            }
            for (var i = 0; i < lines[1].Length; i++)
            {
                algoritmArray[i] = (int)lines[1].ElementAt(i) - 48;
            }
        }

        private char step(int i, int j)
        {
            while (true)
            {
                if (stepCount >= algoritmArray.Length)
                {
                    stepCount = 0;
                }
                switch (algoritmArray[stepCount])
                {
                    case 0:
                        if (i - 1 != -1)
                        {
                            stepCount++;
                            var temp = i - 1;
                            return keyArray[temp, j];
                        }
                        stepCount++;
                        continue;
                    case 1:
                        if ((i - 1 != -1) && (j + 1 < 6))
                        {
                            stepCount++;
                            return keyArray[i - 1, j + 1];
                        }
                        stepCount++;
                        continue;
                    case 2:
                        if (j + 1 < 6)
                        {
                            stepCount++;
                            return keyArray[i, j + 1];
                        }
                        stepCount++;
                        continue;
                    case 3:
                        if ((i + 1 < 6) && (j + 1 < 6))
                        {
                            stepCount++;
                            return keyArray[i + 1, j + 1];
                        }
                        stepCount++;
                        continue;
                    case 4:
                        if (i + 1 < 6)
                        {
                            stepCount++;
                            var temp = i + 1;
                            return keyArray[temp, j];
                        }
                        stepCount++;
                        continue;
                    case 5:
                        if ((i + 1 < 6) && (j - 1 != -1))
                        {
                            stepCount++;
                            return keyArray[i + 1, j - 1];
                        }
                        stepCount++;
                        continue;
                    case 6:
                        if (j - 1 != -1)
                        {
                            stepCount++;
                            return keyArray[i, j - 1];
                        }
                        stepCount++;
                        continue;
                    case 7:
                        if ((i - 1 != -1) && (j - 1 != -1))
                        {
                            stepCount++;
                            return keyArray[i - 1, j - 1];
                        }
                        stepCount++;
                        continue;
                }
                return ' ';
            }
        }
    }
}
