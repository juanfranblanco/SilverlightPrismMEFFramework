namespace Infrastructure.Menu
{
    using System.Collections.Generic;

    public class OrderHintComparisonMenuItem<TMenuContext> : IComparer<IMenuItem<TMenuContext>>
    {
        public int Compare(IMenuItem<TMenuContext> x, IMenuItem<TMenuContext> y)
        {
            return x.OrderHint.CompareTo(y.OrderHint);
        }
    }
}