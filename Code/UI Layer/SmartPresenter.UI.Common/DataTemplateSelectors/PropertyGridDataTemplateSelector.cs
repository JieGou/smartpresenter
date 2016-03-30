using SmartPresenter.UI.Common.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace SmartPresenter.UI.Common
{
    public class PropertyGridDataTemplateSelector : DataTemplateSelector
    {
        #region Available Data Templates

        public DataTemplate DefaultDataTemplate { get; set; }
        public DataTemplate StringDataTemplate { get; set; }
        public DataTemplate ReadOnlyStringDataTemplate { get; set; }
        public DataTemplate BooleanDataTemplate { get; set; }
        public DataTemplate BrushDataTemplate { get; set; }
        public DataTemplate ThicknessDataTemplate { get; set; }
        public DataTemplate CornerRadiusDataTemplate { get; set; }
        public DataTemplate TransformDataTemplate { get; set; }
        public DataTemplate ShadowDataTemplate { get; set; }
        public DataTemplate TextAlignmentDataTemplate { get; set; }
        public DataTemplate FontDataTemplate { get; set; }

        #endregion

        #region Methods

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            PropertyData propertyData = item as PropertyData;
            if (propertyData == null)
            {
                return base.SelectTemplate(item, container);
            }
            if (propertyData.Type.Equals(typeof(String)) && propertyData.IsReadOnly)
            {
                return ReadOnlyStringDataTemplate;
            }
            else if (propertyData.Type.Equals(typeof(String)) || propertyData.Type.Equals(typeof(int)) || propertyData.Type.Equals(typeof(float)) || propertyData.Type.Equals(typeof(double)))
            {
                return StringDataTemplate;
            }
            else if (propertyData.Type.Equals(typeof(Boolean)))
            {
                return BooleanDataTemplate;
            }
            else if (propertyData.Type.Equals(typeof(Brush)))
            {
                return BrushDataTemplate;
            }
            else if (propertyData.Type.Equals(typeof(Thickness)))
            {
                return ThicknessDataTemplate;
            }
            else if (propertyData.Type.Equals(typeof(CornerRadius)))
            {
                return CornerRadiusDataTemplate;
            }
            else if (propertyData.Type.Equals(typeof(Transform)))
            {
                return TransformDataTemplate;
            }
            else if (propertyData.Type.Equals(typeof(DropShadowEffect)))
            {
                return ShadowDataTemplate;
            }
            else if (propertyData.Type.Equals(typeof(TextAlignment)))
            {
                return TextAlignmentDataTemplate;
            }
            else if (propertyData.Type.Equals(typeof(SmartPresenter.BO.Common.Entities.Font)))
            {
                return FontDataTemplate;
            }
            return DefaultDataTemplate;
        }

        #endregion
    }
}
