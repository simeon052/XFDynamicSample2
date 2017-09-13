using System;
using System.Collections.Generic;
using System.Text;

namespace XFDynamicSample2.Models
{
    public class SampleClass
    {
        public enum TestEnum
        {
            Item1,
            Item2,
            Item3,
        }

        public int intProperty { get; set; }
        public void SetIntProperty(int value) { intProperty = value; } 

        public TestEnum enumProperty { get; set; }
        public void SetEnumProperty(TestEnum value) { enumProperty = value; }

        public string stringProperty { get; set; }
        public void SetStringProperty(string value){ stringProperty = value; }

    }
}
