using BaseSite.App.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSite.App.Services
{
    public class DisplayService : KeyedDataStore<string, Display>
    {

        protected override string OutputFile => "displays.json";

        #region Constructors
        public DisplayService() : base()
        { }
        #endregion

        public void ChangeKey(String oldKey, String newKey)
        {
            if (InitialStore.ContainsKey(oldKey))
            {
                if (InitialStore.ContainsKey(newKey))
                {
                    InitialStore[newKey] = InitialStore[oldKey];
                }
                else
                {
                    InitialStore.Add(newKey, InitialStore[oldKey]);
                }
                InitialStore.Remove(oldKey);
                Persist();
            }
        }

        public void Duplicate(String dupKey)
        {
            String newKey = "CopyOf" + dupKey;
            if (InitialStore.ContainsKey(dupKey) && !InitialStore.ContainsKey(newKey))
            {
                InitialStore[newKey] = InitialStore[dupKey];
                Persist();
                RaiseUpdated();
            }
        }
    }

    public class Display
    {
        #region Properties

        #region Static Default Value
        public static Display Default
        {
            get
            {
                return new Display()
                {
                    Logo = "https://upload.wikimedia.org/wikipedia/commons/7/74/Nothing_logo.svg",
                    TickerText = "Some Text Here",
                    Background = "",
                    CSS = "/* Add Custom CSS Here */",
                    ContentText = "Lorem Ipsum and so on...."
                };
            }
        }
        #endregion

        public String Description { get; set; }

        public String Logo { get; set; }

        public String TickerText { get; set; }

        public string Background { get; set; }

        public String CSS { get; set; }

        public String ContentText { get; set; }

        public String ContentHTML { get; set; }

        #endregion
    }


}
