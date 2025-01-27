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
using FiftyOne.Foundation.Mobile.Detection.Entities;
using System.IO;
using FiftyOne.Foundation.Mobile.Detection.Readers;

namespace FiftyOne.Foundation.Mobile.Detection.Factories
{
    internal abstract class BaseEntityFactory<T>
    {
        /// <summary>
        /// Creates a new instance of <see cref="BaseEntity"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose entity list the index or offset is contained
        /// within
        /// </param>
        /// <param name="index">
        /// The index of offset to the start of the entity within the data
        /// structure
        /// </param>
        /// <param name="reader">
        /// Binary reader positioned at the start of the entity
        /// </param>
        /// <returns>A new instance of the entity</returns>
        internal abstract T Create(DataSet dataSet, int index, Reader reader);

        /// <summary>
        /// Returns the length of the entity as stored in the data structure.
        /// </summary>
        /// <remarks>
        /// The method is implement on entities which are part of variable
        /// length lists.
        /// </remarks>
        /// <param name="entity">The Entity the length is required for</param>
        /// <returns>The length of the entity in bytes</returns>
        internal virtual int GetLength(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the length of the entity type as stored in the data
        /// structure.
        /// </summary>
        /// <remarks>
        /// The method is implement on entities which are part of fixed 
        /// length lists.
        /// </remarks>
        /// <returns>The length of the entity type in bytes</returns>
        internal virtual int GetLength()
        {
            throw new NotImplementedException();
        }
    }
        
    internal class ProfileOffsetFactory : BaseEntityFactory<ProfileOffset>
    {
        /// <summary>
        /// Creates a new instance of <see cref="ProfileOffset"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose profile offset list the offset is contained 
        /// ithin
        /// </param>
        /// <param name="index">
        /// The index to the start of the profile within the data structure
        /// </param>
        /// <param name="reader">
        /// Binary reader positioned at the start of the profile offset
        /// </param>
        /// <returns>A new instance of an profile offset</returns>
        internal override ProfileOffset Create(DataSet dataSet, int index, Reader reader)
        {
            return new ProfileOffset(dataSet, index, reader);
        }

        /// <summary>
        /// Returns the length of the <see cref="ProfileOffset"/> entity
        /// </summary>
        /// <returns>Length in bytes of the ProfileOffset</returns>
        internal override int GetLength()
        {
            return sizeof(Int32) * 2;
        }
    }

    internal class AsciiStringFactory : BaseEntityFactory<AsciiString>
    {
        /// <summary>
        /// Creates a new instance of <see cref="AsciiString"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose strings list the string is contained within
        /// </param>
        /// <param name="offset">
        /// The offset to the start of the string within the string data
        /// structure
        /// </param>
        /// <param name="reader">
        /// Binary reader positioned at the start of the AsciiString
        /// </param>
        /// <returns>A new instance of an <see cref="AsciiString"/></returns>
        internal override AsciiString Create(DataSet dataSet, int offset, Reader reader)
        {
            return new AsciiString(dataSet, offset, reader);
        }

        /// <summary>
        /// Returns the length of the
        /// <see cref="AsciiString"/> entity including
        /// the null terminator and length indicator.
        /// </summary>
        /// <param name="entity">Entity of type
        /// <see cref="AsciiString"/></param>
        /// <returns>Length in bytes of the AsciiString</returns>
        internal override int GetLength(AsciiString entity)
        {
            return entity.Value.Length + 3; ;
        }
    }

    internal class ComponentFactoryV31 : ComponentFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="ComponentV31"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose components list the component is contained
        /// within
        /// </param>
        /// <param name="index">Index of the entity within the list</param>
        /// <param name="reader">
        /// Reader connected to the source data structure and positioned to
        /// start reading
        /// </param>
        /// <returns>A new instance of an <see cref="ComponentV31"/></returns>
        internal override Component Create(DataSet dataSet, int index, Reader reader)
        {
            return new ComponentV31(dataSet, index, reader);
        }
    }

    internal class ComponentFactoryV32 : ComponentFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="ComponentFactoryV32"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose components list the component is contained
        /// within
        /// </param>
        /// <param name="index">Index of the entity within the list</param>
        /// <param name="reader">
        /// Reader connected to the source data structure and positioned to
        /// start reading
        /// </param>
        /// <returns>A new instance of an
        /// <see cref="ComponentFactoryV32"/></returns>
        internal override Component Create(DataSet dataSet, int index, Reader reader)
        {
            return new ComponentV32(dataSet, index, reader);
        }
    }

    internal abstract class ComponentFactory : BaseEntityFactory<Component>
    {
        /// <summary>
        /// Returns the length of the <see cref="Component"/> entity
        /// </summary>
        /// <returns>Length in bytes of a Component</returns>
        internal override int GetLength()
        {
            return sizeof(int) * 2 + sizeof(byte);
        }
    }

    internal class MapFactory : BaseEntityFactory<Map>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Map"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose maps list the map is contained within
        /// </param>
        /// <param name="index">Index of the entity within the list</param>
        /// <param name="reader">
        /// Reader connected to the source data structure and positioned to
        /// start reading
        /// </param>
        /// <returns>A new instance of an <see cref="Map"/></returns>
        internal override Map Create(DataSet dataSet, int index, Reader reader)
        {
            return new Map(dataSet, index, reader);
        }

        /// <summary>
        /// Returns the length of the <see cref="Map"/> entity
        /// </summary>
        /// <returns>Length in bytes of a Map</returns>
        internal override int GetLength()
        {
            return sizeof(int);
        }
    }
    
    internal static class NodeFactoryShared
    {
        #region Constants

        /// <summary>
        /// The length of a node index in V3.1 format data.
        /// </summary>
        internal const int NodeIndexLengthV31 = sizeof(bool) + sizeof(int) + sizeof(int);

        /// <summary>
        /// The length of a node index in V3.2 format data.
        /// </summary>
        internal const int NodeIndexLengthV32 = sizeof(int) + sizeof(int);

        /// <summary>
        /// Used by the constructor to read the variable length list of child
        /// node indexes associated with the node in V3.1 format.
        /// </summary>
        /// <param name="dataSet">
        /// The data set the node is contained within
        /// </param>
        /// <param name="reader">
        /// Reader connected to the source data structure and positioned to start reading
        /// </param>
        /// <param name="offset">
        /// The offset in the data structure to the node
        /// </param>
        /// <param name="count">
        /// The number of node indexes that need to be read.
        /// </param>
        /// <returns>An array of child node indexes for the node</returns>
        internal static NodeIndex[] ReadNodeIndexesV31(DataSet dataSet, BinaryReader reader, int offset, int count)
        {
            var array = new NodeIndex[count];
            offset += sizeof(short);
            for (int i = 0; i < array.Length; i++)
            {
                var isString = reader.ReadBoolean();
                array[i] = new NodeIndex(
                    dataSet,
                    offset,
                    isString,
                    ReadValue(reader, isString),
                    reader.ReadInt32());
                offset += NodeFactoryShared.NodeIndexLengthV31;
            }
            return array;
        }

        /// <summary>
        /// Used by the constructor to read the variable length list of child
        /// node indexes associated with the node in V3.1 format.
        /// </summary>
        /// <param name="dataSet">
        /// The data set the node is contained within
        /// </param>
        /// <param name="reader">
        /// Reader connected to the source data structure and positioned to start reading
        /// </param>
        /// <param name="offset">
        /// The offset in the data structure to the node
        /// </param>
        /// <param name="count">
        /// The number of node indexes that need to be read.
        /// </param>
        /// <returns>An array of child node indexes for the node</returns>
        internal static NodeIndex[] ReadNodeIndexesV32(DataSet dataSet, BinaryReader reader, int offset, int count)
        {
            var array = new NodeIndex[count];
            offset += sizeof(short);
            for (int i = 0; i < array.Length; i++)
            {
                var index = reader.ReadInt32();
                var isString = index < 0;
                array[i] = new NodeIndex(
                    dataSet,
                    offset,
                    isString,
                    ReadValue(reader, isString),
                    Math.Abs(index));
                offset += NodeFactoryShared.NodeIndexLengthV32;
            }
            return array;
        }

        /// <summary>
        /// Reads the value and removes any zero characters if it's a string.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="isString">True if the value is a string in the strings list</param>
        /// <returns></returns>
        private static byte[] ReadValue(BinaryReader reader, bool isString)
        {
            var value = reader.ReadBytes(sizeof(int));
            if (isString == false)
            {
                int i;
                for (i = 0; i < value.Length; i++)
                    if (value[i] == 0) break;
                Array.Resize<byte>(ref value, i);
            }
            return value;
        }

        #endregion
    }

    internal abstract class NodeFactory : BaseEntityFactory<Node>
    {
        #region Constants

        /// <summary>
        /// The length of a numeric node index.
        /// </summary>
        internal const int NodeNumericIndexLength = sizeof(short) + sizeof(int);

        /// <summary>
        /// The basic length of a node for all supported versions.
        /// </summary>
        internal const int BaseLength = sizeof(short) + sizeof(short) + sizeof(int) + sizeof(int) +
                                       sizeof(short) + sizeof(short);

        #endregion

        protected abstract Node Construct(DataSet dataSet, int offset, Reader reader);

        /// <summary>
        /// Creates a new instance of <see cref="Node"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose node list the node is contained within
        /// </param>
        /// <param name="offset">
        /// The offset to the start of the node within the string data
        /// structure
        /// </param>
        /// <param name="reader">
        /// Binary reader positioned at the start of the Node
        /// </param>
        /// <returns>A new instance of a <see cref="Node"/></returns>
        internal override Node Create(DataSet dataSet, int offset, Reader reader)
        {
            return Construct(dataSet, offset, reader);
        }
    }

    internal class RootNodeFactory : BaseEntityFactory<Node>
    {
        /// <summary>
        /// An instance of <see cref="Node"/> based on the offset read
        /// from the data structure.
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose node list the node is contained within
        /// </param>
        /// <param name="index">
        /// The character position of the root node.
        /// </param>
        /// <param name="reader">
        /// Binary reader positioned at the start of the integer offset
        /// </param>
        /// <returns>An instance of <see cref="Node"/> which is a root node
        /// </returns>
        internal override Node Create(DataSet dataSet, int index, Reader reader)
        {
            return dataSet.Nodes[reader.ReadInt32()];
        }

        /// <summary>
        /// Returns the length of the root node offset
        /// </summary>
        /// <returns>Length in bytes of a root node offset</returns>
        internal override int GetLength()
        {
            return sizeof(int);
        }
    }

    internal abstract class ProfileFactory : BaseEntityFactory<Profile>
    {
        #region Constants

        /// <summary>
        /// The minimum length of a profile without any values.
        /// </summary>
        private const int MinLength = sizeof(byte) + sizeof(int) + sizeof(int) + sizeof(int);

        #endregion

        protected abstract Profile Construct(DataSet dataSet, int offset, Reader reader);

        /// <summary>
        /// Creates a new instance of <see cref="Profile"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose profile list the profile is contained within
        /// </param>
        /// <param name="offset">
        /// The offset to the start of the profile within the profile data
        /// structure
        /// </param>
        /// <param name="reader">
        /// Binary reader positioned at the start of the Profile
        /// </param>
        /// <returns>A new instance of an <see cref="Profile"/></returns>
        internal override Profile Create(DataSet dataSet, int offset, Reader reader)
        {
            return Construct(dataSet, offset, reader);
        }

        /// <summary>
        /// Returns the length of the <see cref="Profile"/> entity
        /// </summary>
        /// <param name="entity">Entity of type <see cref="Profile"/></param>
        /// <returns>Length in bytes of the Profile</returns>
        internal override int GetLength(Profile entity)
        {
            return MinLength +
                (entity.ValueIndexes.Length * sizeof(int)) +
                (entity.SignatureIndexes.Length * sizeof(int));
        }
    }

    internal class PropertyFactory : BaseEntityFactory<Property>
    {
        #region Constants

        /// <summary>
        /// The length of the property record.
        /// 
        /// 1. byte - the index of the component the property relates to.
        /// 2. byte - the order that the property should be displayed in compared to others.
        /// 3. bool - if the property is mandatory and must contain values.
        /// 4. bool - if the property contains a list of values.
        /// 5. bool - if the values should be shown in UIs.
        /// 6. bool - if the property is obsolete and will be removed in future versions.
        /// 7. bool - if the property should be shown in a list of properties.
        /// 8. byte - the strong type of the values.
        /// 9. int - the index of the default value, or -1 if one is not provided.
        /// 10. int - the offset to the string containing the name of the property.
        /// 11. int - the offset to the string containing the description of the property.
        /// 12. int - the offset to the string containing the category of the property.
        /// 13. int - the offset to the string containing the URL of the property.
        /// 14. int - the index of the first value in the list of values.
        /// 15. int - the index of the last value in the list of values.
        /// 16. int - the number of maps assigned to the property.
        /// 17. int - the first index of the map assigned to the property.
        /// </summary>
        internal const int RecordLength = (sizeof(int) * 9) + (sizeof(bool) * 5) + (sizeof(byte) * 3);

        #endregion

        /// <summary>
        /// Creates a new instance of <see cref="Property"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose properties list the property is contained within
        /// </param>
        /// <param name="offset">
        /// The offset to the start of the property within the string data
        /// structure
        /// </param>
        /// <param name="reader">
        /// Binary reader positioned at the start of the Property
        /// </param>
        /// <returns>A new instance of a <see cref="Property"/></returns>
        internal override Property Create(DataSet dataSet, int offset, Reader reader)
        {
            return new Property(dataSet, offset, reader);
        }

        internal override int GetLength()
        {
            return RecordLength;
        }
    }

    internal class ValueFactory : BaseEntityFactory<Value>
    {
        #region Constants

        /// <summary>
        /// The length of the values record.
        /// 
        /// 1. short - the index of the property the value relates to.
        /// 2. int - offset to the string containing the name of the property.
        /// 3. int - offset to the string containing the description of the value.
        /// 4. int - offset to the string containing a URL for the value.
        /// </summary>
        internal const int RecordLength = (sizeof(int) * 3) + sizeof(short);

        #endregion

        #region Overridden Methods

        /// <summary>
        /// Creates a new instance of <see cref="Value"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose values list the value is contained within
        /// </param>
        /// <param name="offset">
        /// The offset to the start of the value within the values data structure
        /// </param>
        /// <param name="reader">
        /// Binary reader positioned at the start of the Value
        /// </param>
        /// <returns>A new instance of a <see cref="Value"/></returns>
        internal override Value Create(DataSet dataSet, int offset, Reader reader)
        {
            return new Value(dataSet, offset, reader);
        }

        internal override int GetLength()
        {
            return RecordLength;
        }

        #endregion
    }

    internal class SignatureFactoryV31 : BaseEntityFactory<Signature>
    {
        #region Fields

        private readonly int _recordLength;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new instance of <see cref="SignatureFactoryV31"/>
        /// </summary>
        /// <param name="dataSet">The data set the factory will create signatures for</param>
        internal SignatureFactoryV31(DataSet dataSet)
        {
            _recordLength =
                (dataSet.SignatureProfilesCount * sizeof(int)) +
                (dataSet.SignatureNodesCount * sizeof(int));
        }

        #endregion

        #region Overridden Methods

        /// <summary>
        /// Creates a new instance of <see cref="SignatureV31"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose signature list the value is contained within
        /// </param>
        /// <param name="index">
        /// The index of the signature within the values data structure
        /// </param>
        /// <param name="reader">
        /// Binary reader positioned at the start of the signature
        /// </param>
        /// <returns>A new instance of a <see cref="Signature"/></returns>
        internal override Signature Create(DataSet dataSet, int index, Reader reader)
        {
            return new SignatureV31(dataSet, index, reader);
        }

        /// <summary>
        /// The length of the signature.
        /// </summary>
        /// <returns>Length of the signature in bytes</returns>
        internal override int GetLength()
        {
            return _recordLength;
        }

        #endregion
    }

    internal class SignatureFactoryV32 : BaseEntityFactory<Signature>
    {
        #region Constants

        /// <summary>
        /// The length of each signature record in the dataset.
        /// byte = count of nodes associated with the signature
        /// int = first index of the node offset in signaturesnodes
        /// int = rank of the signature
        /// byte = flags to indicate information about the signature
        /// </summary>
        private const int NODES_LENGTH = sizeof(byte) + sizeof(int) + sizeof(int) + sizeof(byte);

        #endregion

        #region Fields

        private readonly int _recordLength;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new instance of <see cref="SignatureFactoryV32"/>
        /// </summary>
        internal SignatureFactoryV32(DataSet dataSet)
        {
            _recordLength =
                (dataSet.SignatureProfilesCount * sizeof(int)) +
                NODES_LENGTH;
        }

        #endregion

        #region Overridden Methods

        /// <summary>
        /// Creates a new instance of <see cref="SignatureV32"/>
        /// </summary>
        /// <param name="dataSet">
        /// The data set whose signature list the value is contained within
        /// </param>
        /// <param name="index">
        /// The index of the signature within the values data structure
        /// </param>
        /// <param name="reader">
        /// Binary reader positioned at the start of the signature
        /// </param>
        /// <returns>A new instance of a <see cref="Signature"/></returns>
        internal override Signature Create(DataSet dataSet, int index, Reader reader)
        {
            return new SignatureV32(dataSet, index, reader);
        }

        /// <summary>
        /// The length of the signature.
        /// </summary>
        /// <returns>Length of the signature in bytes</returns>
        internal override int GetLength()
        {
            return _recordLength;
        }

        #endregion
    }
}
