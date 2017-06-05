// <copyright company="Recorded Books, Inc" file="Item.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

using System;
using System.Runtime.Serialization;

namespace WebMarket.Api.Model
{
    
    [KnownType(typeof(eMagazine))]
    [KnownType(typeof(Book))]
    [DataContract(Name = "item")]
    public class Item 
    {

    }
}
