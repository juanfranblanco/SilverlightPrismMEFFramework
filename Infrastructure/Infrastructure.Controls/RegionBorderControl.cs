using System;
using System.Windows;
using System.Windows.Controls;

namespace Infrastructure.Controls
{
    /// <summary>
    /// A simple control that provides a border around a region
    /// in order to display the region's name in a header.
    /// </summary>
    public sealed class RegionBorderControl : ContentControl
    {
        #region Constants and Fields

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.Register(
            "RegionName",
            typeof(String),
            typeof(RegionBorderControl),
            new PropertyMetadata(string.Empty, OnRegionNameChanged));

        #endregion

        #region Constructors and Destructors

        public RegionBorderControl()
        {
            // Set default style key for generic template to be applied.
            this.DefaultStyleKey = typeof(RegionBorderControl);
        }

        #endregion

        #region Properties

        public string RegionName
        {
            get
            {
                return (string)this.GetValue(RegionNameProperty);
            }
            set
            {
                this.SetValue(RegionNameProperty, value);
            }
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region Methods

        private static void OnRegionNameChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var target = o as RegionBorderControl;
        }

        #endregion
    }
}