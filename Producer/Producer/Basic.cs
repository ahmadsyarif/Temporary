using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
namespace Producer
{
    class Basic
    {
        [JsonProperty("name")]
        public string name { set; get; }
        [JsonProperty("value")]
        public int value { set; get; }

        public Basic(string Name, int Value)
        {
            this.name = Name;
            this.value = Value;
        }

        public override string ToString()
        {
            return this.name + " " + this.value;
        }
    }
}
