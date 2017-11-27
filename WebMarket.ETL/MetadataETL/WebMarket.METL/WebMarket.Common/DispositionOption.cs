// <copyright company="Recorded Books Inc" file="DispositionOption.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Common
{
    using System;

    public enum DispositionOption
    {
        Inactive = 0,
        Active = 1,
        ToAdd = 2,
        ToRemove = 3,
        ToModify = 4,
        NoChange = 5,
        ReLoad = 6,
        Orphan = 7,
        ReActivate = 8
    }
}
