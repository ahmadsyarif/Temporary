using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathApps
{
    class person
    {
        private string _name;
        private int _age;
        public person(string name)
        {
            _name = name;
        }
        public void setName(string name)
        {
            _name = name;
        }
        public void setAge(int age)
        {
            _age = age;
        }

    }
}
