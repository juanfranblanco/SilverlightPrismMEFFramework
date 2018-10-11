namespace Infrastructure.Menu
{
    using System;

    public interface IMenuItemViewOpener<TMenuItemContext>:IMenuItem<TMenuItemContext>
    {
        Type ViewType { get; set; }
        string TargetName { get; set; }
    }
}