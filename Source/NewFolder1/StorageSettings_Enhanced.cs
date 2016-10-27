﻿using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace HaulingHysteresis
{
    public class StorageSettings_Enhanced
    {
        public static void ExposeData(StorageSettings storage)
        {
            FieldInfo field = typeof(StorageSettings).GetField("priorityInt", BindingFlags.Instance | BindingFlags.NonPublic);
            StoragePriority storagePriority = (StoragePriority)field.GetValue(storage);
            Scribe_Values.LookValue<StoragePriority>(ref storagePriority, "priority", StoragePriority.Unstored, false);
            field.SetValue(storage, storagePriority);
            Scribe_Deep.LookDeep<ThingFilter>(ref storage.filter, "filter", new object[0]);
            StorageSettings_Hysteresis storageSettings_Hysteresis = StorageSettings_Mapping.Get(storage);
            Scribe_Deep.LookDeep<StorageSettings_Hysteresis>(ref storageSettings_Hysteresis, "hysteresis", new object[0]);
            bool flag = storageSettings_Hysteresis != null;
            if (flag)
            {
                StorageSettings_Mapping.Set(storage, storageSettings_Hysteresis);
            }
        }
    }
}
