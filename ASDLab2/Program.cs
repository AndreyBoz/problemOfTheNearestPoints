using System;
namespace Program {
    class Lab2 {
        class Snail : IComparable{
            Random rnd = new Random();
            public int X { get; set; } = 0;
            public int Y { get; set; } = 0;
            public Snail()
            {
                X = rnd.Next(0, 100000);
                Y = rnd.Next(0, 100000);
            }
            override public string ToString() {
                return String.Format($"{X} {Y}");
            }

            public int CompareTo(object? obj)
            {
                Snail snail = obj as Snail;
                return X.CompareTo(snail.X);
            }
        }
        static double MinDistance(List<Snail> snails)
        {
            int size = snails.Count;
            if (size <= 3) return MinDistSlow(snails);
            snails.Sort((snails1, snails2) => snails1.X.CompareTo(snails2.X)); // sort by x
            
            double midle = (snails[snails.Count() / 2 - 1].X + snails[snails.Count() / 2].X) / 2; // the midline with which I divide the points

            List<Snail> leftSnails = new List<Snail>(size);
            List<Snail> rightSnails = new List<Snail>(size);

            leftSnails.AddRange(snails.GetRange(0, size / 2));
            rightSnails.AddRange(snails.GetRange(size / 2, size - (size/2)));
            double leftMin = MinDistance(leftSnails);
            double rightMin = MinDistance(rightSnails);
            double minDist = Math.Min(leftMin, rightMin);

            List<Snail> leftMidleSnails = new List<Snail>();
            List<Snail> rightMidleSnails = new List<Snail>();
            
            double distMax = 100000*Math.Sqrt(2); // max distance
            double distance;
            bool flag1, flag2;
            if (leftMidleSnails != null && rightMidleSnails != null)
            {
                foreach (var snail in leftMidleSnails)
                {
                    foreach (var snail2 in rightMidleSnails)
                    {
                        flag1 = (snail2.Y >= (snail.Y - minDist) && snail2.Y <= snail.Y);       
                        flag2 = (snail2.Y >= snail.Y && snail2.Y <= (snail.Y + minDist));
                        if (flag1 || flag2)
                        {
                            distance = Distance(snail, snail2);
                            if (distance < distMax)
                            {
                                distMax = distance;
                            }

                        }
                    }
                }
                return Math.Min(minDist, distMax);
            }
            return minDist;
        }
        static double MinDistSlow(List<Snail> snails)
        {
            double minDist = Distance(snails[0], snails[1]);
            for (int i = 0; i < snails.Count; i++)
            {
                for (int j = 0; j < snails.Count; j++)
                {
                    if (i == j) continue;
                    if (minDist > Distance(snails[i], snails[j]))
                    {
                        minDist = Distance(snails[i], snails[j]);
                    }
                }
            }
            return minDist;
        }
        static double Distance(Snail a, Snail b) {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
        
        static void Main(string[] args) {
            int countSnail = 0;
            double minDist;
            List<Snail> snails = new List<Snail>();
            Console.WriteLine("Enter the number of snails:: ");
            if (int.TryParse(Console.ReadLine(), out countSnail)) {
                for (int i = 0;i < countSnail; i++) {
                    snails.Add(new Snail());    
                }
                snails.ForEach(snail => Console.WriteLine(snail));
                minDist = MinDistance(snails);
                Console.WriteLine($"The snails will meet in:{Math.Round(minDist/2,2)}") ;
            }
            else
            {
                Console.WriteLine("Введите количество улиток, ошибка записи.");
            }

        }
    }
}
