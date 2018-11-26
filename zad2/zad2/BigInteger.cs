using System;
using System.Collections.Generic;
using System.Text;

namespace zad2
{
    [Serializable]
    public class BigInteger
    {
        #region static

        public static BigInteger PowerModulo(BigInteger a, BigInteger e, BigInteger n)
        {
            BigInteger p, w;
            int howManyBits = HowManyBits(e);
            byte[] bits = ToBits(e, howManyBits);

            p = new BigInteger(a);
            w = 1;
            for (int i = bits.Length - 1; i >= 0; i--)
            {
                if (bits[i] == 1)
                    w = MultiplyModulo(w, p, n);
                p = MultiplyModulo(p, p, n);
            }

            return w;
        }

        public static int HowManyBits(BigInteger e)
        {
            BigInteger m = 1;
            int howManyBits = 0;
            while (m <= e)
            {
                m *= 2;
                howManyBits++;
            }
            return howManyBits;
        }

        public static bool MillerRabinTest(BigInteger number, BigInteger numberOfIterations)
        {
            BigInteger s, d, i, a, x, j;

            d = number - 1;
            s = 0;
            while (d % 2 == 0)
            {
                s++;
                d /= 2;
            }

            for (i = 0; i < numberOfIterations; i++)
            {
                a = Random(2, number - 2);
                x = PowerModulo(a, d, number);

                if (x == 1 || x == number - 1)
                    continue;

                j = 1;
                while (j < s && x != number - 1)
                {
                    x = MultiplyModulo(x, x, number);
                    if (x == 1)
                        return false;
                    j++;
                }
                if (x != number - 1)
                    return false;
            }

            return true;
        }

        public static BigInteger MultiplyModulo(BigInteger a, BigInteger b, BigInteger n)
        {
            BigInteger w;

            w = 0;
            int howManyBits = HowManyBits(b);
            byte[] bits = ToBits(b, howManyBits);

            for (int i = bits.Length - 1; i >= 0; i--)
            {
                if (bits[i] == 1)
                    w = (w + a) % n;
                a = (a << 1) % n;
            }
            return w;
        }

        public static BigInteger Nwd(BigInteger x, BigInteger y)
        {
            BigInteger temp;
            BigInteger a = new BigInteger(x);
            BigInteger b = new BigInteger(y);

            while (b != 0)
            {
                temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public static BigInteger Random(BigInteger min, BigInteger max)
        {
            Random r = new Random();
            BigInteger difference = max - min;
            int length = r.Next(0, difference.Number.Length);
            string number = "";
            number += (char)(r.Next(9) + '1');

            for (int i = 1; i < length; i++)
            {
                number += (char)(r.Next(10) + '0');
            }

            BigInteger result = new BigInteger(number);

            return min + result;
        }

        public static BigInteger Power(BigInteger b1, BigInteger b2)
        {
            BigInteger power = new BigInteger();
            BigInteger two = new BigInteger("2", false);
            BigInteger zero = new BigInteger();

            //power.Number = Pow(b1.Number, b2.Number);
            power.Number = FastPow(b1.Number, b2.Number);
            power.Sign = (b2 % two) == zero ? false : b1.Sign;
            //power.Sign = b2 == two;

            return power;
        }

        public static BigInteger Absolute(BigInteger b)
        {
            return new BigInteger(b.Number, false);
        }

        #endregion

        #region properties

        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                StringBuilder sb = new StringBuilder(value);

                if (sb[0] == '-')
                {
                    sb = sb.Remove(0, 1);
                    Sign = true;
                }
                else
                {
                    Sign = false;
                }

                for (int i = 0; i < sb.Length; i++)
                {
                    if (sb[i] < '0' || sb[i] > '9')
                        throw new ArgumentException("Nie liczba");
                }

                while (sb[0] == '0' && sb.Length != 1)
                {
                    sb = sb.Remove(0, 1);
                }
                number = sb.ToString();
            }
        }

        public bool Sign
        {
            get
            {
                return sign;
            }
            set
            {
                sign = value;
            }
        }

        #endregion

        #region constructors

        public BigInteger()
        {
            Number = "0";
        }

        public BigInteger(long l)
        {
            Number = l.ToString();
        }

        public BigInteger(string n)
        {
            Number = n;
        }

        public BigInteger(string n, bool s)
        {
            Number = n;
            Sign = s;
        }

        public BigInteger(BigInteger b)
        {
            Number = b.Number;
            Sign = b.Sign;
        }

        #endregion

        #region overriden

        public override bool Equals(object obj)
        {
            BigInteger bi = obj as BigInteger;
            if (bi == null)
                return false;
            else
                return (Number == bi.Number) && (Sign == bi.Sign);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return (sign == false ? "" : "-") + number;
        }

        #endregion

        #region overloaded operators

        public static implicit operator BigInteger(int val)
        {
            return new BigInteger(val.ToString());
        }

        public static implicit operator BigInteger(long val)
        {
            return new BigInteger(val);
        }

        public static implicit operator BigInteger(string val)
        {
            return new BigInteger(val);
        }

        private static byte[] ToBits(BigInteger source, int howManyBits)
        {
            byte[] result = new byte[howManyBits];
            BigInteger temp = new BigInteger(source);
            int i = 0;

            while (temp != 0)
            {
                result[i++] = Convert.ToByte((temp % 2).ToString());
                temp /= 2;
            }

            Array.Reverse(result);

            return result;
        }

        public static BigInteger operator <<(BigInteger b1, int b2)
        {
            return b1 * Power(2, b2);
        }

        public static bool operator ==(BigInteger b1, BigInteger b2)
        {
            return (b1.Number == b2.Number) && (b1.Sign == b2.Sign);
        }

        public static bool operator !=(BigInteger b1, BigInteger b2)
        {
            return !(b1 == b2);
        }

        public static bool operator <(BigInteger b1, BigInteger b2)
        {
            if (b1.Sign == true && b2.Sign == false) // b1 ujemne, b2 dodatnie
                return true;
            if (b1.Sign == false && b2.Sign == true) // b1 dodatnie, b2 ujemne
                return false;
            if (b1.Sign == false && b2.Sign == false)// obie dodatnie
            {
                if (b1.Number.Length < b2.Number.Length) // jezeli b1 jest krotsze niz b2
                    return true;
                if (b1.Number.Length > b2.Number.Length) // jezeli b1 jest dluzsze niz b2
                    return false;
                if (b1.Number.CompareTo(b2.Number) < 0)
                    return true;
                if (b1.Number.CompareTo(b2.Number) >= 0)
                    return false;
            }
            if (b1.Sign == true && b2.Sign == true)// obie ujemne
            {
                if (b1.Number.Length < b2.Number.Length) // jezeli b1 jest krotsze niz b2
                    return false;
                if (b1.Number.Length > b2.Number.Length) // jezeli b1 jest dluzsze niz b2
                    return true;
                if (b1.Number.CompareTo(b2.Number) <= 0)
                    return false;
                if (b1.Number.CompareTo(b2.Number) > 0)
                    return true;
            }

            return false;
        }

        public static bool operator >(BigInteger b1, BigInteger b2)
        {
            return !(b1 < b2);
        }

        public static bool operator <=(BigInteger b1, BigInteger b2)
        {
            return ((b1 < b2) || (b1 == b2));
        }

        public static bool operator >=(BigInteger b1, BigInteger b2)
        {
            return ((b1 > b2) || (b1 == b2));
        }

        public static BigInteger operator ++(BigInteger b)
        {
            return b + new BigInteger("1");
        }

        public static BigInteger operator --(BigInteger b)
        {
            return b - new BigInteger("1");
        }

        public static BigInteger operator +(BigInteger b1, BigInteger b2)
        {
            BigInteger addition = new BigInteger();
            if (b1.Sign == b2.Sign) // obie ujemne albo obie dodatnie
            {
                addition.Number = Add(b1.Number, b2.Number);
                addition.Sign = b1.Sign;
            }
            else // rozne znaki
            {
                if (Absolute(b1) > Absolute(b2))
                {
                    addition.Number = Subtract(b1.Number, b2.Number);
                    addition.Sign = b1.Sign;
                }
                else
                {
                    addition.Number = Subtract(b2.Number, b1.Number);
                    addition.Sign = b2.Sign;
                }
            }
            if (addition.Number == "0") // -0 problem
            {
                addition.Sign = false;
            }

            return addition;
        }

        public static BigInteger operator -(BigInteger b1, BigInteger b2)
        {
            BigInteger bs2 = new BigInteger(b2);
            bs2.Sign = !bs2.Sign; // x - y = x + (-y)

            return b1 + bs2;
        }

        public static BigInteger operator *(BigInteger b1, BigInteger b2)
        {
            BigInteger multiply = new BigInteger();

            multiply.Number = MultiplyKaratsuba(b1.Number, b2.Number);
            multiply.Sign = b1.Sign != b2.Sign;

            if (multiply.Number == "0") // -0 problem
            {
                multiply.Sign = false;
            }

            return multiply;
        }

        public static BigInteger operator /(BigInteger b1, BigInteger b2)
        {
            BigInteger divide = new BigInteger();

            divide.Number = Divide(b1.Number, b2.Number).Item1;
            divide.Sign = b1.Sign != b2.Sign;

            if (divide.Number == "0") // -0 problem
            {
                divide.Sign = false;
            }

            return divide;
        }

        public static BigInteger operator %(BigInteger b1, BigInteger b2)
        {
            BigInteger reminder = new BigInteger();

            reminder.Number = Divide(b1.Number, b2.Number).Item2;
            reminder.Sign = b1.Sign != b2.Sign;

            if (reminder.Number == "0") // -0 problem
            {
                reminder.Sign = false;
            }

            return reminder;
        }

        #endregion

        #region private
        private static string Add(string number1, string number2)
        {
            StringBuilder result;
            char carry = '0';
            int lengthDifference = Math.Abs(number1.Length - number2.Length);

            if (number1.Length > number2.Length)
            {
                result = new StringBuilder(number1);
                number2 = number2.Insert(0, new string('0', lengthDifference));
            }
            else
            {
                result = new StringBuilder(number2);
                number1 = number1.Insert(0, new string('0', lengthDifference));
            }

            for (int i = number1.Length - 1; i >= 0; --i)
            {
                result[i] = (char)(((carry - '0') + (number1[i] - '0') + (number2[i] - '0')) + '0');

                if (i != 0)
                {
                    if (result[i] > '9')
                    {
                        result[i] = (char)(result[i] - 10);
                        carry = '1';
                    }
                    else
                        carry = '0';
                }
            }
            if (result[0] > '9')
            {
                result[0] = (char)(result[0] - 10);
                result.Insert(0, '1');
            }
            return result.ToString();
        }

        private static string Subtract(string number1, string number2)
        {
            StringBuilder result;
            StringBuilder sbnumber1;
            StringBuilder sbnumber2;
            int lengthDifference = Math.Abs(number1.Length - number2.Length);

            if (number1.Length > number2.Length)
            {
                result = new StringBuilder(number1);
                number2 = number2.Insert(0, new string('0', lengthDifference));
            }
            else
            {
                result = new StringBuilder(number2);
                number1 = number1.Insert(0, new string('0', lengthDifference));
            }

            sbnumber1 = new StringBuilder(number1);
            sbnumber2 = new StringBuilder(number2);

            for (int i = sbnumber1.Length - 1; i >= 0; --i)
            {
                if (sbnumber1[i] < sbnumber2[i])
                {
                    sbnumber1[i] = (char)(sbnumber1[i] + 10);
                    sbnumber1[i - 1]--;
                }
                result[i] = (char)(((sbnumber1[i] - '0') - (sbnumber2[i] - '0')) + '0');
            }

            while (result[0] == '0' && result.Length != 1)
                result.Remove(0, 1);

            return result.ToString();
        }

        private static string MultiplyKaratsuba(string number1, string number2)
        {
            if (Less(number1, "10") || Less(number2, "10"))
                return Multiply(number1, number2);

            int n = Math.Max(number1.Length, number2.Length);
            int m = (int)Math.Ceiling(n / 2.0);

            string n1 = number1.PadLeft(n, '0');
            string n2 = number2.PadLeft(n, '0');

            string high1 = RemoveFrontZeros(n1.Remove(n1.Length - m));
            string low1 = RemoveFrontZeros(n1.Substring(n1.Length - m));
            string high2 = RemoveFrontZeros(n2.Remove(n2.Length - m));
            string low2 = RemoveFrontZeros(n2.Substring(n2.Length - m));

            string X = MultiplyKaratsuba(high1, high2);
            string Y = MultiplyKaratsuba(low1, low2);
            string Z = Subtract(Subtract(MultiplyKaratsuba(Add(low1, high1), Add(low2, high2)), X), Y);

            return Add(Add(Multiply10(X, 2 * m), (Multiply10(Z, m))), Y);
        }

        private static string Multiply10(string x, int i)
        {
            return x.PadRight(x.Length + i, '0');
        }

        private static string RemoveFrontZeros(string v)
        {
            while (v[0] == '0' && v.Length != 1)
                v = v.Remove(0, 1);
            return v;
        }

        private static string Multiply(string number1, string number2)
        {
            if (number1.Length > number2.Length)
            {
                string temp = number1;
                number1 = number2;
                number2 = temp;
            }

            string result = "0";
            for (int i = number1.Length - 1; i >= 0; --i)
            {
                StringBuilder temp = new StringBuilder(number2);
                int currentDigit = number1[i] - '0';
                int carry = 0;

                for (int j = temp.Length - 1; j >= 0; --j)
                {
                    temp[j] = (char)(((temp[j] - '0') * currentDigit) + carry);

                    if (temp[j] > 9)
                    {
                        carry = (temp[j] / 10);
                        temp[j] = (char)(temp[j] - (carry * 10));
                    }
                    else
                        carry = 0;

                    temp[j] += '0'; // back to string mood
                }

                if (carry > 0)
                    temp.Insert(0, (char)(carry + '0'));

                temp.Append('0', (number1.Length - i - 1)); // as like mult by 10, 100, 1000, 10000 and so on

                result = Add(result, temp.ToString()); // O(n)
            }

            while (result[0] == '0' && result.Length != 1) // erase leading zeros
                result = result.Remove(0, 1);

            return result;
        }

        private static Tuple<string, string> Divide(string number1, string number2)
        {
            if (number1.Length < number2.Length)
            {
                return Tuple.Create("0", new string(number1.ToCharArray()));
            }
            if (number1.Length == number2.Length)
            {
                if (number1.CompareTo(number2) == 0)
                {
                    return Tuple.Create("1", "0");
                }
                if (number1.CompareTo(number2) == -1)
                {
                    return Tuple.Create("0", new string(number1.ToCharArray()));
                }
            }

            string result = "";
            int r;
            StringBuilder sb1 = new StringBuilder();

            foreach (char c in number1)
            {
                sb1.Append(c);
                while (sb1[0] == '0' && sb1.Length != 1)
                    sb1 = sb1.Remove(0, 1);
                if (Less(sb1.ToString(), number2))
                {
                    result += "0";
                    continue;
                }
                r = 0;
                while (!Less(sb1.ToString(), number2))
                {
                    sb1 = new StringBuilder(Subtract(sb1.ToString(), number2));
                    r++;
                }

                for (int i = 0; i < sb1.Length; i++)
                {
                    if (sb1[0] != '0')
                        break;
                    sb1 = sb1.Remove(0, 1);
                }

                result += r.ToString();
            }

            while (result[0] == '0' && result.Length != 1)
                result = result.Remove(0, 1);

            if (sb1.Length == 0)
                sb1.Append('0');

            return Tuple.Create(result, sb1.ToString());
        }

        private static Tuple<string, string> Divide(string n, long den)
        {
            long rem = 0;
            StringBuilder result = new StringBuilder() { Length = 10000 };

            for (int i = 0, len = n.Length; i < len; ++i)
            {
                rem = (rem * 10) + (n[i] - '0');
                result[i] = (char)(rem / den + '0');
                rem %= den;
            }
            result.Length = n.Length;

            while (result[0] == '0' && result.Length != 1)
            {
                result.Remove(0, 1);
            }

            if (result.Length == 0)
            {
                result = new StringBuilder("0");
            }

            return Tuple.Create(result.ToString(), rem.ToString());
        }

        private static string FastPow(string number1, string number2)
        {
            if (number2 == "0")
                return "1";
            if ("13579".Contains(number2[number2.Length - 1].ToString()))
                return Multiply(number1, Square(FastPow(number1, Divide(Subtract(number2, "1"), "2").Item1)));
            return Square(FastPow(number1, Divide(number2, "2").Item1));
        }

        private static string Square(string number)
        {
            return MultiplyKaratsuba(number, number);
        }

        private static string Pow(string number1, string number2)
        {
            if (number2 == "0")
            {
                return "1";
            }
            string result = new string(number1.ToCharArray());
            for (string i = "1"; Less(i, number2); i = Add(i, "1"))
            {
                result = Multiply(result, number1);
            }
            return result;
        }

        private static bool Less(string number1, string number2)
        {
            if (number1.Length < number2.Length)
            {
                return true;
            }
            if (number1.Length > number2.Length)
            {
                return false;
            }
            if (number1.CompareTo(number2) == -1)
            {
                return true;
            }
            return false;
        }

        private string number;
        private bool sign;
        #endregion
    }
}
