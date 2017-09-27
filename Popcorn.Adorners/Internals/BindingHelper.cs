namespace Popcorn.Adorners.Internals
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Data;

    internal static class BindingHelper
    {
        private static readonly Dictionary<DependencyProperty, PropertyPath> PropertyPaths = new Dictionary<DependencyProperty, PropertyPath>();

        internal static BindingBuilder Bind(
            this DependencyObject target,
            DependencyProperty targetProperty)
        {
            return new BindingBuilder(target, targetProperty);
        }

        private static BindingExpression Bind(
            DependencyObject target,
            DependencyProperty targetProperty,
            object source,
            PropertyPath path)
        {
            var binding = new Binding
            {
                Path = path,
                Source = source,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            return (BindingExpression)BindingOperations.SetBinding(target, targetProperty, binding);
        }

        private static PropertyPath GetPath(DependencyProperty property)
        {
            if (PropertyPaths.TryGetValue(property, out PropertyPath path))
            {
                return path;
            }

            path = new PropertyPath(property);
            PropertyPaths[property] = path;
            return path;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static BindingExpression Bind(
            DependencyObject target,
            DependencyProperty targetProperty,
            object source,
            DependencyProperty sourceProperty)
        {
            return Bind(target, targetProperty, source, GetPath(sourceProperty));
        }

        internal struct BindingBuilder
        {
            private readonly DependencyObject target;
            private readonly DependencyProperty targetProperty;

            internal BindingBuilder(DependencyObject target, DependencyProperty targetProperty)
            {
                this.target = target;
                this.targetProperty = targetProperty;
            }

            internal BindingExpression OneWayTo(object source, DependencyProperty sourceProperty)
            {
                var sourcePath = GetPath(sourceProperty);
                return this.OneWayTo(source, sourcePath);
            }

            internal BindingExpression TwoWayTo(object source, DependencyProperty sourceProperty)
            {
                var sourcePath = GetPath(sourceProperty);
                var binding = new Binding
                {
                    Source = source,
                    Path = sourcePath,
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                return (BindingExpression)BindingOperations.SetBinding(this.target, this.targetProperty, binding);
            }

            internal BindingExpression OneWayTo(object source)
            {
                var binding = new Binding
                {
                    Source = source,
                    Mode = BindingMode.OneWay,
                };

                return (BindingExpression)BindingOperations.SetBinding(this.target, this.targetProperty, binding);
            }

            internal BindingExpression OneWayTo(object source, PropertyPath sourcePath)
            {
                var binding = new Binding
                {
                    Path = sourcePath,
                    Source = source,
                    Mode = BindingMode.OneWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                return (BindingExpression)BindingOperations.SetBinding(this.target, this.targetProperty, binding);
            }
        }
    }
}