using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Menu.Controller
{
    /// <summary>
    /// </summary>
    public interface IMenuItemStateService
    {
        /// <summary>
        /// Checks if the menu item is required to be enabled
        /// </summary>
        /// <param name="menuItemName">the menu item name</param>
        /// <returns>true, false</returns>
        bool IsMenuItemRequiredToBeEnabled(string menuItemName);

        /// <summary>
        /// Checks if the menu item is required to be visible
        /// </summary>
        /// <param name="menuItemName">the menu item name</param>
        /// <returns>true, false</returns>
        bool IsMenuItemRequiredToBeVisbile(string menuItemName);
    }
}
