using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Planning.Bindings;
using NinjectAdapter;

namespace MetroWpf
{
    public class NinjectServiceLocatorExt :
        NinjectServiceLocator,
        ICustomTypeDescriptor
    {
        private readonly IKernel _kernel;

        /// <summary>
        ///   Initializes a new instance of the <see cref="NinjectServiceLocatorExt" /> class.
        /// </summary>
        /// <param name="kernel"> The kernel. </param>
        public NinjectServiceLocatorExt(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        ///   Gets the kernel.
        /// </summary>
        public IKernel Kernel
        {
            get { return _kernel; }
        }

        /// <summary>
        ///   Returns a collection of custom attributes for this instance of a component.
        /// </summary>
        /// <returns> An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object. </returns>
        public AttributeCollection GetAttributes()
        {
            return new AttributeCollection(null);
        }

        /// <summary>
        ///   Returns the class name of this instance of a component.
        /// </summary>
        /// <returns> The class name of the object, or null if the class does not have a name. </returns>
        public string GetClassName()
        {
            return null;
        }

        /// <summary>
        ///   Returns the name of this instance of a component.
        /// </summary>
        /// <returns> The name of the object, or null if the object does not have a name. </returns>
        public string GetComponentName()
        {
            return null;
        }

        /// <summary>
        ///   Returns a type converter for this instance of a component.
        /// </summary>
        /// <returns> A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or null if there is no <see
        ///    cref="T:System.ComponentModel.TypeConverter" /> for this object. </returns>
        public TypeConverter GetConverter()
        {
            return null;
        }

        /// <summary>
        ///   Returns the default event for this instance of a component.
        /// </summary>
        /// <returns> An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or null if this object does not have events. </returns>
        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        /// <summary>
        ///   Returns the default property for this instance of a component.
        /// </summary>
        /// <returns> A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or null if this object does not have properties. </returns>
        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        /// <summary>
        ///   Returns an editor of the specified type for this instance of a component.
        /// </summary>
        /// <returns> An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found. </returns>
        /// <param name="editorBaseType"> A <see cref="T:System.Type" /> that represents the editor for this object. </param>
        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        /// <summary>
        ///   Returns the events for this instance of a component.
        /// </summary>
        /// <returns> An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance. </returns>
        public EventDescriptorCollection GetEvents()
        {
            return new EventDescriptorCollection(null);
        }

        /// <summary>
        ///   Returns the events for this instance of a component using the specified attribute array as a filter.
        /// </summary>
        /// <returns> An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance. </returns>
        /// <param name="attributes"> An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return new EventDescriptorCollection(null);
        }

        /// <summary>
        ///   Returns the properties for this instance of a component.
        /// </summary>
        /// <returns> A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance. </returns>
        public PropertyDescriptorCollection GetProperties()
        {
            var allBindings = GetAllBindings();

            var result = new PropertyDescriptorCollection(allBindings
                                                              .Where(x => x.Value.Count == 1 && !x.Value.First().IsConditional)
                                                              .Select(x => new NinjectBindingPropertyDescriptor(_kernel, x.Key, x.Value.First()))
                                                              .ToArray());

            return result;
        }

        /// <summary>
        ///   Returns the properties for this instance of a component using the attribute array as a filter.
        /// </summary>
        /// <returns> A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance. </returns>
        /// <param name="attributes"> An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return ((ICustomTypeDescriptor)this).GetProperties(null);
        }

        /// <summary>
        ///   Returns an object that contains the property described by the specified property descriptor.
        /// </summary>
        /// <returns> An <see cref="T:System.Object" /> that represents the owner of the specified property. </returns>
        /// <param name="pd"> A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found. </param>
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        /// <summary>
        ///   Gets all bindings registered with the kernel
        /// </summary>
        /// <returns> </returns>
        private IEnumerable<KeyValuePair<Type, ICollection<IBinding>>> GetAllBindings()
        {
            var fi = typeof(KernelBase).GetField("bindings", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);
            return (Multimap<Type, IBinding>)fi.GetValue(_kernel);
        }

        public class NinjectBindingPropertyDescriptor : PropertyDescriptor
        {
            private readonly IBinding binding;
            private readonly IKernel kernel;

            /// <summary>
            ///   Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the specified name and attributes.
            /// </summary>
            /// <param name="kernel"> </param>
            /// <param name="name"> The name of the property. </param>
            /// <param name="binding"> Ninject binding </param>
            public NinjectBindingPropertyDescriptor(IKernel kernel, Type name, IBinding binding)
                : base(name.Name, null)
            {
                this.kernel = kernel;
                this.binding = binding;
            }

            /// <summary>
            ///   When overridden in a derived class, gets the type of the component this property is bound to.
            /// </summary>
            /// <returns> A <see cref="T:System.Type" /> that represents the type of component this property is bound to. When the <see
            ///    cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)" /> or <see
            ///    cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)" /> methods are invoked, the object specified might be an instance of this type. </returns>
            public override Type ComponentType
            {
                get { return typeof(NinjectServiceLocatorExt); }
            }

            /// <summary>
            ///   When overridden in a derived class, gets a value indicating whether this property is read-only.
            /// </summary>
            /// <returns> true if the property is read-only; otherwise, false. </returns>
            public override bool IsReadOnly
            {
                get { return true; }
            }

            /// <summary>
            ///   When overridden in a derived class, gets the type of the property.
            /// </summary>
            /// <returns> A <see cref="T:System.Type" /> that represents the type of the property. </returns>
            public override Type PropertyType
            {
                get { return binding.Service; }
            }

            /// <summary>
            ///   When overridden in a derived class, returns whether resetting an object changes its value.
            /// </summary>
            /// <returns> true if resetting the component changes its value; otherwise, false. </returns>
            /// <param name="component"> The component to test for reset capability. </param>
            public override bool CanResetValue(object component)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            ///   When overridden in a derived class, gets the current value of the property on a component.
            /// </summary>
            /// <returns> The value of a property for a given component. </returns>
            /// <param name="component"> The component with the property for which to retrieve the value. </param>
            public override object GetValue(object component)
            {
                var request = kernel.CreateRequest(binding.Service, null, binding.Parameters, false, true);
                var result = kernel.Resolve(request).FirstOrDefault();
                return result;
            }

            /// <summary>
            ///   When overridden in a derived class, resets the value for this property of the component to the default value.
            /// </summary>
            /// <param name="component"> The component with the property value that is to be reset to the default value. </param>
            public override void ResetValue(object component)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            ///   When overridden in a derived class, sets the value of the component to a different value.
            /// </summary>
            /// <param name="component"> The component with the property value that is to be set. </param>
            /// <param name="value"> The new value. </param>
            public override void SetValue(object component, object value)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            ///   When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.
            /// </summary>
            /// <returns> true if the property should be persisted; otherwise, false. </returns>
            /// <param name="component"> The component with the property to be examined for persistence. </param>
            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }
        }
    }
}
