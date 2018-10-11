namespace Infrastructure.Menu
{
    using System;

    public interface IActionMenuItem<TMenuItemContext> : IMenuItem<TMenuItemContext>
    {
        Action<TMenuItemContext> Action { get; set; }
    }
}