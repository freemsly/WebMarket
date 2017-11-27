// <copyright company="Recorded Books Inc" file="LoaderElementSection.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System;
    using System.Configuration;

    public sealed class LoaderElementCollection //: ConfigurationElementCollection
    {
        //public LoaderElement this[int index]
        //{
        //    get { return base.BaseGet(index) as LoaderElement; }
        //    set
        //    {
        //        if (base.BaseGet(index) != null)
        //        {
        //            base.BaseRemoveAt(index);
        //        }
        //        this.BaseAdd(index, value);
        //    }
        //}

        //protected override ConfigurationElement CreateNewElement()
        //{
        //    return new LoaderElement();
        //}

        //protected override object GetElementKey(ConfigurationElement element)
        //{
        //    return (element as LoaderElement).Name;
        //}

        //public void Add(LoaderElement element)
        //{
        //    base.BaseAdd(element);
        //}

        //public LoaderElementCollection()
        //{
        //    LoaderElement element = (LoaderElement)CreateNewElement();
        //    base.BaseAdd(element);
        //}
    }
}
