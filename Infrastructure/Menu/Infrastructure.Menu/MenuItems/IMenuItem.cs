namespace Infrastructure.Menu
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Prism.Commands;

    public interface IMenuItem<TMenuItemContext>: IMenuItem
    {
        TMenuItemContext MenuItemContext { get; set; }
    }

    public interface  IMenuItem
    {
        DelegateCommand MenuItemCommand { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        int OrderHint { get; set; }

        bool Enabled { get; set; }

        bool Visible { get; set; }

    }
}