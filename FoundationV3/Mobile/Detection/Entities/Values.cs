﻿/* *********************************************************************
 * This Source Code Form is copyright of 51Degrees Mobile Experts Limited. 
 * Copyright © 2015 51Degrees Mobile Experts Limited, 5 Charlotte Close,
 * Caversham, Reading, Berkshire, United Kingdom RG4 7BY
 * 
 * This Source Code Form is the subject of the following patent 
 * applications, owned by 51Degrees Mobile Experts Limited of 5 Charlotte
 * Close, Caversham, Reading, Berkshire, United Kingdom RG4 7BY: 
 * European Patent Application No. 13192291.6; and
 * United States Patent Application Nos. 14/085,223 and 14/085,301.
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0.
 * 
 * If a copy of the MPL was not distributed with this file, You can obtain
 * one at http://mozilla.org/MPL/2.0/.
 * 
 * This Source Code Form is “Incompatible With Secondary Licenses”, as
 * defined by the Mozilla Public License, v. 2.0.
 * ********************************************************************* */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiftyOne.Foundation.Mobile.Detection.Entities
{
    /// <summary>
    /// Encapsulates a list of one or more <see cref="Value"/> entities. 
    /// Provides methods to return boolean, double and string representations 
    /// of the values list.
    /// </summary>
    /// <para>
    /// The class contains helper methods to make consuming the data set easier.
    /// </para>
    /// <para>
    /// For more information see https://51degrees.com/Support/Documentation/Net
    /// </para>
    public class Values : IList<Value>
    {
        #region Static Fields

        /// <summary>
        /// Used to find values based on name.
        /// </summary>
        private static readonly SearchLists<Value, string> _valuesNameSearch = 
            new SearchLists<Value, string>();

        #endregion

        #region Fields

        /// <summary>
        /// The property the list of values relates to.
        /// </summary>
        private readonly Property _property;

        /// <summary>
        /// An array of values to expose.
        /// </summary>
        private readonly Value[] _values;

        #endregion

        #region Properties

        /// <summary>
        /// Returns true if any of the values in this list are the
        /// default ones for the property.
        /// </summary>
        public bool IsDefault
        {
            get
            {
                return this.Any(i => i.IsDefault);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new instance of the values list.
        /// </summary>
        /// <param name="property">
        /// Property the values list relates to.
        /// </param>
        /// <param name="values">
        /// An array of values to use with the list.
        /// </param>
        internal Values(Property property, Value[] values)
        {
            _property = property;
            _values = values;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the value associated with the name provided.
        /// </summary>
        /// <param name="valueName"></param>
        /// <returns></returns>
        public Value this[string valueName]
        {
            get
            {
                var index = _valuesNameSearch.BinarySearch(_values, valueName);
                return index >= 0 ? this[index] : null;
            }
        }

        /// <summary>
        /// The value represented as a boolean.
        /// </summary>
        /// <exception cref="MobileException">
        /// Thrown if the method is called for a property with multiple values.
        /// </exception>
        /// <returns>
        /// A boolean representation of the only item in the list.
        /// </returns>
        public bool ToBool()
        {
            if (_property.IsList)
                throw new MobileException("ToBool can only be used on non List properties");
            return Count > 0 ? this[0].ToBool() : false;
        }

        /// <summary>
        /// The value represented as a double.
        /// </summary>
        /// <exception cref="MobileException">
        /// Thrown if the method is called for a property with multiple values.
        /// </exception>
        /// <returns>
        /// A double representation of the only item in the list.
        /// </returns>
        public double ToDouble()
        {
            if (_property.IsList)
                throw new MobileException("ToDouble can only be used on non List properties");
            return Count > 0 ? this[0].ToDouble() : 0;
        }

        /// <summary>
        /// The value represented as a integer.
        /// </summary>
        /// <exception cref="MobileException">
        /// Thrown if the method is called for a property with multiple values.
        /// </exception>
        /// <returns>
        /// A integer representation of the only item in the list.
        /// </returns>
        public int ToInt()
        {
            if (_property.IsList)
                throw new MobileException("ToInt can only be used on non List properties");
            return Count > 0 ? this[0].ToInt() : 0;
        }

        /// <summary>
        /// Returns the values as a string array.
        /// </summary>
        /// <returns>
        /// Values as a string array.
        /// </returns>
        public string[] ToStringArray()
        {
            return this.Select(i => i.Name).ToArray();
        }

        /// <summary>
        /// The values represented as a string where multiple values are 
        /// seperated by colons.
        /// </summary>
        /// <returns>
        /// The values as a string.
        /// </returns>
        public override string ToString()
        {
            return String.Join(
                Constants.ValueSeperator,
                this.Select(i => i.ToString()).ToArray());
        }

        #endregion

        #region IList Interface Members

        /// <summary>
        /// Determines the index of a specific <see cref="Value"/> item in the Values.
        /// </summary>
        /// <param name="item">The value to locate in the list of values.</param>
        /// <returns>The index of value if found in the list; otherwise, -1.</returns>
        public int IndexOf(Value item)
        {
            for (var index = 0; index < _values.Length; index++)
            {
                if (_values[index].Equals(item))
                {
                    return index;
                }
            }
            return -1;
        }

        /// <summary>
        /// Not implemented as values list is readonly.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, Value item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented as values list is readonly.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="Value"/> at the specified index. Set is not implemented
        /// as the values list is readonly.
        /// </summary>
        /// <param name="index">The zero-based index of the value to get.</param>
        /// <returns>The value at the specified index.</returns>
        public Value this[int index]
        {
            get
            {
                return _values[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Not implemented as values list is readonly.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Value item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented as values list is readonly.
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the values list contains a specific <see cref="Value"/>.
        /// </summary>
        /// <param name="item">The value to locate in the values list.</param>
        /// <returns>true if the value is found in the values list; otherwise, false.</returns>
        public bool Contains(Value item)
        {
            return _values.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the values list to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the values copied from the values list. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Value[] array, int arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the values list.
        /// </summary>
        public int Count
        {
            get { return _values.Length; }
        }

        /// <summary>
        /// Always returns true as the values list is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Not implemented as values list is readonly.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Value item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the values list.
        /// </summary>
        /// <returns>An IEnumerator&lt;<see cref="Value"/>&gt; object that can be used to iterate through the collection.</returns>
        public IEnumerator<Value> GetEnumerator()
        {
            return _values.Select(i => i).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the values list.
        /// </summary>
        /// <returns>An IEnumerator&lt;<see cref="Value"/>&gt; object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        #endregion
    }
}
