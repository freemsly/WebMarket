using System;

namespace WebMarket.Common
{
    public class TypedItem
    {
        public TypedItem()
        {
        }

        public TypedItem(string key, object value)
        {
        }

        public TypedItem(string key, object value, OperatorTypeOption op)
        {
        }
        
        public string Key { get; set; }
        public string Domain { get; set; }
        public string Scope { get; set; }
        public DateTimeOffset Tds { get; set; }
        public string Text { get; set; }
        public OperatorTypeOption Operator { get; set; }
        public object Value { get; set; }
        public string Display { get; }

        public override string ToString()
        {
            return Text.ToString();
        }
    }

    [Flags]
    public enum OperatorTypeOption
    {
        None = 0,
        EqualTo = 1,
        NotEqualTo = 2,
        LessThan = 4,
        GreaterThan = 8,
        X = 16,
        And = 32,
        Or = 64
    }
}
